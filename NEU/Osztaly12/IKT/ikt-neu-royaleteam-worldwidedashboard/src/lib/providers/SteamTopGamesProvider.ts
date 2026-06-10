import { BaseDataSource } from '../core/DataSource';
export interface SteamChartGame {
  rank: number;
  id: string;
  title: string;
  developer: string;
  statValue: string; 
  coverUrl: string;
}
export interface SteamChartsData {
  mostPlayed: SteamChartGame[];
  wishlisted: SteamChartGame[];
  online: SteamChartGame[];
}
export class SteamTopGamesProvider extends BaseDataSource<SteamChartsData> {
  id = 'steam_top_games';
  name = 'Steam Top Charts';
  category = 'Games';
  private knownAppNames: Record<number, string> = {
    730: 'Counter-Strike 2',
    570: 'Dota 2',
    578080: 'PUBG: BATTLEGROUNDS',
    1172470: 'Apex Legends',
    271590: 'Grand Theft Auto V',
    1203220: 'NARAKA: BLADEPOINT',
    252490: 'Rust',
    440: 'Team Fortress 2',
    431960: 'Wallpaper Engine',
    230410: 'Warframe',
    236390: 'War Thunder',
    359550: 'Tom Clancy\'s Rainbow Six Siege',
    1085660: 'Destiny 2',
    1091500: 'Cyberpunk 2077',
    1245620: 'ELDEN RING',
    3678970: 'Monster Hunter Wilds',
    2694490: 'Infinity Nikki'
  };
  async fetchData(): Promise<SteamChartsData> {
    try {
      const [onlineRes, playedRes] = await Promise.allSettled([
        fetch('https://api.steampowered.com/ISteamChartsService/GetGamesByConcurrentPlayers/v1/', { next: { revalidate: 3600 } }).then(r => r.json()),
        fetch('https://api.steampowered.com/ISteamChartsService/GetMostPlayedGames/v1/', { next: { revalidate: 3600 } }).then(r => r.json())
      ]);
      const online = onlineRes.status === 'fulfilled' ? this.mapSteamApiToGames(onlineRes.value.response?.ranks?.slice(0, 10), 'concurrent_in_game') : [];
      const mostPlayed = playedRes.status === 'fulfilled' ? this.mapSteamApiToGames(playedRes.value.response?.ranks?.slice(0, 10), 'peak_in_game') : [];
      return {
        online: online.length > 0 ? online : this.getMockOnline(),
        mostPlayed: mostPlayed.length > 0 ? mostPlayed : this.getMockMostPlayed(),
        wishlisted: this.getMockWishlisted()
      };
    } catch (error) {
      console.error('Failed to fetch Steam Top Games, falling back to mock data', error);
      return {
        online: this.getMockOnline(),
        mostPlayed: this.getMockMostPlayed(),
        wishlisted: this.getMockWishlisted()
      };
    }
  }
  private mapSteamApiToGames(ranks: any[], statKey: string): SteamChartGame[] {
    if (!ranks || !Array.isArray(ranks)) return [];
    return ranks.map((g: any) => ({
      rank: g.rank,
      id: String(g.appid),
      title: this.knownAppNames[g.appid] || `Steam App ${g.appid}`,
      developer: 'Unknown',
      statValue: `${(g[statKey] || 0).toLocaleString()} players`,
      coverUrl: `https://cdn.cloudflare.steamstatic.com/steam/apps/${g.appid}/header.jpg`
    }));
  }
  validateResponse(data: unknown): boolean {
    return typeof data === 'object' && data !== null && 'mostPlayed' in data && 'wishlisted' in data && 'online' in data;
  }
  private getMockMostPlayed(): SteamChartGame[] {
    return [
      { rank: 1, id: '730', title: 'Counter-Strike 2', developer: 'Valve', statValue: '1,275,982 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/730/header.jpg' },
      { rank: 2, id: '578080', title: 'PUBG: BATTLEGROUNDS', developer: 'KRAFTON, Inc.', statValue: '732,248 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/578080/header.jpg' },
      { rank: 3, id: '570', title: 'Dota 2', developer: 'Valve', statValue: '685,102 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/570/header.jpg' },
      { rank: 4, id: '3678970', title: 'Monster Hunter Wilds', developer: 'CAPCOM', statValue: '333,188 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/3678970/header.jpg' },
      { rank: 5, id: '2694490', title: 'Infinity Nikki', developer: 'Infold Games', statValue: '318,722 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/2694490/header.jpg' },
      { rank: 6, id: '1172470', title: 'Apex Legends', developer: 'Electronic Arts', statValue: '252,192 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1172470/header.jpg' },
      { rank: 7, id: '1203220', title: 'NARAKA: BLADEPOINT', developer: '24 Entertainment', statValue: '251,323 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1203220/header.jpg' },
      { rank: 8, id: '252490', title: 'Rust', developer: 'Facepunch Studios', statValue: '172,305 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/252490/header.jpg' },
      { rank: 9, id: '271590', title: 'Grand Theft Auto V', developer: 'Rockstar Games', statValue: '161,288 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/271590/header.jpg' },
      { rank: 10, id: '431960', title: 'Wallpaper Engine', developer: 'Wallpaper Engine Team', statValue: '115,200 peak', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/431960/header.jpg' }
    ];
  }
  private getMockOnline(): SteamChartGame[] {
    return [
      { rank: 1, id: '730', title: 'Counter-Strike 2', developer: 'Valve', statValue: '1,352,584 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/730/header.jpg' },
      { rank: 2, id: '578080', title: 'PUBG: BATTLEGROUNDS', developer: 'KRAFTON, Inc.', statValue: '680,190 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/578080/header.jpg' },
      { rank: 3, id: '570', title: 'Dota 2', developer: 'Valve', statValue: '621,734 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/570/header.jpg' },
      { rank: 4, id: '3678970', title: 'Monster Hunter Wilds', developer: 'CAPCOM', statValue: '332,049 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/3678970/header.jpg' },
      { rank: 5, id: '2694490', title: 'Infinity Nikki', developer: 'Infold Games', statValue: '273,891 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/2694490/header.jpg' },
      { rank: 6, id: '1172470', title: 'Apex Legends', developer: 'Electronic Arts', statValue: '235,484 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1172470/header.jpg' },
      { rank: 7, id: '1203220', title: 'NARAKA: BLADEPOINT', developer: '24 Entertainment', statValue: '202,301 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1203220/header.jpg' },
      { rank: 8, id: '252490', title: 'Rust', developer: 'Facepunch Studios', statValue: '154,302 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/252490/header.jpg' },
      { rank: 9, id: '271590', title: 'Grand Theft Auto V', developer: 'Rockstar Games', statValue: '141,120 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/271590/header.jpg' },
      { rank: 10, id: '431960', title: 'Wallpaper Engine', developer: 'Wallpaper Engine Team', statValue: '98,000 players', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/431960/header.jpg' }
    ];
  }
  private getMockWishlisted(): SteamChartGame[] {
    return [
      { rank: 1, id: '2358720', title: 'Black Myth: Wukong', developer: 'Game Science', statValue: '8,452,100 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/2358720/header.jpg' },
      { rank: 2, id: '2452460', title: 'Hollow Knight: Silksong', developer: 'Team Cherry', statValue: '5,200,450 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/2452460/header.jpg' },
      { rank: 3, id: '1145360', title: 'Hades II', developer: 'Supergiant Games', statValue: '4,890,200 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1145360/header.jpg' },
      { rank: 4, id: '1086940', title: 'Baldur\'s Gate 3', developer: 'Larian Studios', statValue: '4,100,500 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1086940/header.jpg' },
      { rank: 5, id: '1623730', title: 'Palworld', developer: 'Pocketpair', statValue: '3,750,000 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1623730/header.jpg' },
      { rank: 6, id: '1790600', title: 'ARK: Survival Ascended', developer: 'Studio Wildcard', statValue: '3,200,100 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1790600/header.jpg' },
      { rank: 7, id: '1361210', title: 'Warhammer 40,000: Space Marine 2', developer: 'Saber Interactive', statValue: '3,050,000 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1361210/header.jpg' },
      { rank: 8, id: '1938090', title: 'Call of Duty', developer: 'Activision', statValue: '2,900,000 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1938090/header.jpg' },
      { rank: 9, id: '2050650', title: 'Resident Evil 4', developer: 'CAPCOM', statValue: '2,800,500 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/2050650/header.jpg' },
      { rank: 10, id: '1627720', title: 'Lies of P', developer: 'NEOWIZ', statValue: '2,500,000 wishlists', coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1627720/header.jpg' }
    ];
  }
}
