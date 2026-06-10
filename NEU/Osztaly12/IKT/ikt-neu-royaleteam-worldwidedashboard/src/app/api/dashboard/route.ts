import { NextResponse } from 'next/server';
import { DashboardManager } from '@/lib/core/DashboardManager';
import { SteamProvider } from '@/lib/providers/SteamProvider';
import { YouTubeProvider } from '@/lib/providers/YouTubeProvider';
import { FinanceProvider } from '@/lib/providers/FinanceProvider';
import { PoliticsProvider } from '@/lib/providers/PoliticsProvider';
import { MusicProvider } from '@/lib/providers/MusicProvider';
import { TRNProvider } from '@/lib/providers/TRNProvider';
import { LastFmPersonalProvider } from '@/lib/providers/LastFmPersonalProvider';
import { SteamPersonalProvider } from '@/lib/providers/SteamPersonalProvider';
import { ExophaseProvider } from '@/lib/providers/ExophaseProvider';
import { SteamTopGamesProvider } from '@/lib/providers/SteamTopGamesProvider';
import { YouTubePersonalProvider } from '@/lib/providers/YouTubePersonalProvider';
import { LastFmArtistProvider } from '@/lib/providers/LastFmArtistProvider';
export async function GET(req: Request) {
  try {
    const url = new URL(req.url);
    const musicCountry = url.searchParams.get('music_country') || 'Global';
    const manager = new DashboardManager();
    manager.registerProvider(new SteamTopGamesProvider());
    manager.registerProvider(new SteamProvider([0, 730, 570, 359550, 386940])); 
    manager.registerProvider(new YouTubeProvider([
      { name: 'MrBeast', id: 'UCX6OQ3DkcsbYNE6H8uQQuVA' },
      { name: 'T-Series', id: 'UCq-Fj5jknLsUf-MWSy4_brA' },
      { name: 'Cocomelon - Nursery Rhymes', id: 'UCbCmjCuTUZos6Inko4u57UQ' },
      { name: 'SET India', id: 'UCpEhnqL0y41EpW2TvWAHD7Q' },
      { name: '✿ Kids Diana Show', id: 'UCk8GzjMOrta8yxDcKfylJYw' },
      { name: 'PewDiePie', id: 'UC-lHJZR3Gqxm24_Vd_AJ5Yw' },
      { name: 'Like Nastya', id: 'UCJplp5SjeGSdVdwsfb9Q7lQ' },
      { name: 'Vlad and Niki', id: 'UCvlE5gTbOvjiolFlEm-c_Ow' },
      { name: 'Zee Music Company', id: 'UCFFbwnve3yF62-tVXkTyHqg' },
      { name: 'WWE', id: 'UCJ5v_MCY6GNUBTO8-D3XoAg' }
    ]));
    manager.registerProvider(new FinanceProvider());
    manager.registerProvider(new PoliticsProvider());
    manager.registerProvider(new MusicProvider(musicCountry));
    const data = await manager.fetchAllData();
    const { cookies } = await import('next/headers');
    const cookieStore = await cookies();
    const token = cookieStore.get('auth_token')?.value;
    if (token) {
      const { verifyToken } = await import('@/lib/utils/auth');
      const { prisma } = await import('@/lib/utils/prisma');
      const payload = await verifyToken(token);
      if (payload && payload.id) {
        const user = await prisma.user.findUnique({
          where: { id: payload.id as string }
        });
        if (user) {
          const personalData: any = {};
          if (user.youtubeHandle) {
            try {
              let ytId = user.youtubeHandle;
              const handleMatch = ytId.match(/youtube\.com\/@([^/?]+)/);
              const channelMatch = ytId.match(/youtube\.com\/channel\/(UC[^/?]+)/);
              if (channelMatch) {
                ytId = channelMatch[1];
              } else if (handleMatch) {
                ytId = handleMatch[1];
              }
              const personalYt = new YouTubePersonalProvider(ytId);
              const ytData = await personalYt.fetchData();
              if (ytData) {
                personalData.youtube = { [ytData.name]: ytData };
              }
            } catch (e) {
              console.error('Failed to fetch personal YouTube', e);
            }
          }
          if (user.steamId) {
            try {
              const steamPersonal = new SteamPersonalProvider(user.steamId);
              const steamData = await steamPersonal.fetchData();
              if (steamData) {
                personalData.steam = steamData;
              }
            } catch (e) {
              console.error('Failed to fetch Steam Personal Data', e);
            }
          }
          if (user.exophaseUsername) {
            try {
              const exophase = new ExophaseProvider(user.exophaseUsername);
              const exoData = await exophase.fetchData();
              if (exoData) {
                data['exophase_stats'] = exoData;
              }
            } catch (e) {
              console.error('Failed to fetch Exophase Data', e);
            }
          }
          if (user.favoriteArtist) {
            try {
              const artistProvider = new LastFmArtistProvider(user.favoriteArtist);
              const artistData = await artistProvider.fetchData();
              if (artistData) {
                personalData.artist = artistData;
              }
            } catch (e) {
              console.error('Failed to fetch Last.fm Artist Data', e);
            }
          }
          if (user.trackerUrlR6 || user.trackerUrlRL || user.trackerUrlLoL || user.trackerUrlBF6 || user.trackerUrlFortnite) {
            try {
              const trnUrls = {
                r6: user.trackerUrlR6,
                rl: user.trackerUrlRL,
                lol: user.trackerUrlLoL,
                bf2042: user.trackerUrlBF6,
                fortnite: user.trackerUrlFortnite
              };
              const trn = new TRNProvider(trnUrls);
              const trnData = await trn.fetchData();
              if (trnData) {
                data['trn_gaming'] = trnData;
              }
            } catch (e) {
              console.error('Failed to fetch TRN Data', e);
            }
          }
          if (user.steamId) {
            try {
              const steamPersonal = new SteamPersonalProvider(user.steamId, user.steamApiKey);
              const steamPersonalData = await steamPersonal.fetchData();
              if (steamPersonalData) {
                data['steam_personal'] = steamPersonalData;
              }
            } catch (e) {
              console.error('Failed to fetch Steam Personal Data', e);
            }
          }
          let lastFmUsername = user.lastFmUsername;
          if (!lastFmUsername) {
            try {
              const rawUser: any = await prisma.$queryRaw`SELECT lastFmUsername FROM User WHERE id = ${payload.id}`;
              if (rawUser && rawUser.length > 0) {
                lastFmUsername = rawUser[0].lastFmUsername;
              }
            } catch (e) {
              console.error('Raw query failed', e);
            }
          }
          if (lastFmUsername) {
            try {
              let parsedUsername = lastFmUsername;
              const match = lastFmUsername.match(/last\.fm\/user\/([^/?]+)/);
              if (match) {
                parsedUsername = match[1];
              }
              const lastFm = new LastFmPersonalProvider(parsedUsername);
              const lastFmData = await lastFm.fetchData();
              if (lastFmData) {
                data['lastfm_personal'] = lastFmData;
              }
            } catch (e) {
              console.error('Failed to fetch Last.fm Data', e);
            }
          }
          if (Object.keys(personalData).length > 0) {
            data['personal_data'] = personalData;
          }
        }
      }
    }
    return NextResponse.json(data);
  } catch (error) {
    console.error('Error fetching dashboard data:', error);
    return NextResponse.json({ error: 'Failed to fetch dashboard data' }, { status: 500 });
  }
}
