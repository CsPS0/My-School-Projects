import { BaseDataSource } from '../core/DataSource';
export interface RecentTrack {
  name: string;
  artist: string;
  album: string;
  nowPlaying: boolean;
  imageUrl?: string;
  date?: string;
}
export interface LastFmPersonalData {
  username: string;
  recentTracks: RecentTrack[];
}
export class LastFmPersonalProvider extends BaseDataSource<LastFmPersonalData | null> {
  id = 'lastfm_personal';
  name = 'Last.fm Now Playing';
  category = 'Entertainment';
  private lastFmUsername: string;
  constructor(lastFmUsername: string) {
    super();
    this.lastFmUsername = lastFmUsername;
  }
  async fetchData(): Promise<LastFmPersonalData | null> {
    if (!this.lastFmUsername) return null;
    const apiKey = process.env.LASTFM_API_KEY;
    if (!apiKey) {
      console.warn('LASTFM_API_KEY is not set. Returning mock personal data.');
      return this.getMockData();
    }
    try {
      const res = await fetch(`http://ws.audioscrobbler.com/2.0/?method=user.getrecenttracks&user=${this.lastFmUsername}&api_key=${apiKey}&format=json&limit=10`, {
        next: { revalidate: 60 } 
      });
      if (!res.ok) throw new Error(`Last.fm API failed with status ${res.status}`);
      const data = await res.json();
      if (!data.recenttracks || !data.recenttracks.track) {
         throw new Error('Invalid Last.fm response format');
      }
      const tracks = data.recenttracks.track;
      const recentTracks: RecentTrack[] = tracks.map((t: any) => ({
        name: t.name,
        artist: t.artist?.['#text'] || t.artist?.name || 'Unknown Artist',
        album: t.album?.['#text'] || 'Unknown Album',
        nowPlaying: t['@attr']?.nowplaying === 'true',
        imageUrl: t.image?.find((img: any) => img.size === 'large')?.['#text'] || t.image?.[0]?.['#text'],
        date: t.date?.['#text'] || 'Now',
      }));
      return {
        username: this.lastFmUsername,
        recentTracks,
      };
    } catch (error) {
      console.error('Failed to fetch Last.fm personal data:', error);
      return this.getMockData();
    }
  }
  private getMockData(): LastFmPersonalData {
    return {
      username: this.lastFmUsername,
      recentTracks: [
        {
          name: 'Take My Breath',
          artist: 'The Weeknd',
          album: 'Dawn FM',
          nowPlaying: true,
          imageUrl: 'https://lastfm.freetls.fastly.net/i/u/174s/2a96cbd8b46e442fc41c2b86b821562f.png',
          date: 'Now'
        },
        {
          name: 'Levitating',
          artist: 'Dua Lipa',
          album: 'Future Nostalgia',
          nowPlaying: false,
          imageUrl: 'https://lastfm.freetls.fastly.net/i/u/174s/878604d7c0411d33d9eb74bde1b3cc3d.png',
          date: '10 mins ago'
        }
      ]
    };
  }
  validateResponse(data: unknown): boolean {
    if (data === null) return true;
    if (typeof data !== 'object') return false;
    const d = data as Record<string, unknown>;
    return 'recentTracks' in d && Array.isArray(d.recentTracks);
  }
}
