import { BaseDataSource } from '../core/DataSource';
export interface GameStats {
  gameId: string;
  gameName: string;
  platform: string;
  username: string;
  level: number;
  rank: {
    name: string;
    mmr: number | string;
    iconUrl?: string;
  };
  primaryStats: {
    label: string;
    value: string | number;
  }[];
  timePlayedSeconds?: number;
}
export class TRNProvider extends BaseDataSource<Record<string, GameStats> | null> {
  id = 'trn_gaming';
  name = 'Tracker Network';
  category = 'Gaming';
  private urls: Record<string, string | null>;
  constructor(urls: Record<string, string | null>) {
    super();
    this.urls = urls;
  }
  async fetchData(): Promise<Record<string, GameStats> | null> {
    const results: Record<string, GameStats> = {};
    let hasAnyUrl = false;
    for (const [gameId, url] of Object.entries(this.urls)) {
      if (!url || url.trim() === '') continue;
      hasAnyUrl = true;
      let username = 'Unknown';
      let platform = 'unknown';
      try {
        if (gameId === 'fortnite') {
          const match = url.match(/profile\/([^/]+)\/([^/?]+)/);
          if (match) { platform = match[1]; username = match[2]; }
        } else if (gameId === 'lol') {
          const match = url.match(/profile\/([^/]+)\/([^/]+)\/([^/?]+)/);
          if (match) { platform = match[1]; username = decodeURIComponent(match[3]); }
        } else {
          const match = url.match(/profile\/([^/]+)\/([^/?]+)/);
          if (match) { platform = match[1]; username = match[2]; }
        }
        username = decodeURIComponent(username);
      } catch {
        console.warn(`Failed to parse URL for ${gameId}: ${url}`);
      }
      results[gameId] = this.getMockDataForGame(gameId, username, platform);
    }
    

    if (!hasAnyUrl) {
      const defaultGames = ['r6', 'rl', 'lol', 'bf2042', 'fortnite'];
      const defaultName = 'RoyaleGamer';
      for (const game of defaultGames) {
        results[game] = this.getMockDataForGame(game, defaultName, 'pc');
      }
    }
    
    this.isMock = true; 
    return Object.keys(results).length > 0 ? results : null;
  }
  private getMockDataForGame(gameId: string, username: string, platform: string): GameStats {
    const seed = username.length * 13 + 42;
    if (gameId === 'r6') {
      return {
        gameId: 'r6',
        gameName: 'Rainbow Six Siege',
        platform,
        username,
        level: 120 + seed,
        rank: { name: 'Platinum II', mmr: 3450 },
        primaryStats: [
          { label: 'Kills', value: (4210 + seed * 10).toLocaleString() },
          { label: 'K/D Ratio', value: (1.08 + (seed % 20) / 100).toFixed(2) },
          { label: 'Win %', value: `${(51.6 + (seed % 10)).toFixed(1)}%` }
        ]
      };
    }
    if (gameId === 'rl') {
      return {
        gameId: 'rl',
        gameName: 'Rocket League',
        platform,
        username,
        level: 85 + (seed % 50),
        rank: { name: 'Champion I', mmr: 1045 },
        primaryStats: [
          { label: 'Goals', value: (3450 + seed * 5).toLocaleString() },
          { label: 'Saves', value: (1840 + seed * 2).toLocaleString() },
          { label: 'Win %', value: `${(54.2 + (seed % 5)).toFixed(1)}%` }
        ]
      };
    }
    if (gameId === 'lol') {
      return {
        gameId: 'lol',
        gameName: 'League of Legends',
        platform,
        username,
        level: 340 + seed,
        rank: { name: 'Diamond IV', mmr: '75 LP' },
        primaryStats: [
          { label: 'KDA', value: (2.84 + (seed % 10) / 10).toFixed(2) },
          { label: 'CS/Min', value: (6.8 + (seed % 5) / 10).toFixed(1) },
          { label: 'Win %', value: `${(52.1 + (seed % 6)).toFixed(1)}%` }
        ]
      };
    }
    if (gameId === 'bf2042') {
      return {
        gameId: 'bf2042',
        gameName: 'Battlefield 2042',
        platform,
        username,
        level: 98 + (seed % 20),
        rank: { name: 'S014', mmr: 0 },
        primaryStats: [
          { label: 'Kills', value: (8402 + seed * 20).toLocaleString() },
          { label: 'K/D Ratio', value: (1.45 + (seed % 15) / 100).toFixed(2) },
          { label: 'Headshots', value: (1205 + seed * 3).toLocaleString() }
        ]
      };
    }
    if (gameId === 'fortnite') {
      return {
        gameId: 'fortnite',
        gameName: 'Fortnite',
        platform,
        username,
        level: 215 + (seed % 100),
        rank: { name: 'Elite', mmr: '45%' },
        primaryStats: [
          { label: 'Wins', value: (142 + seed % 50).toLocaleString() },
          { label: 'K/D Ratio', value: (2.15 + (seed % 10) / 10).toFixed(2) },
          { label: 'Win %', value: `${(8.4 + (seed % 4)).toFixed(1)}%` }
        ]
      };
    }
    return {
      gameId,
      gameName: 'Unknown Game',
      platform,
      username,
      level: 1,
      rank: { name: 'Unranked', mmr: 0 },
      primaryStats: []
    };
  }
  validateResponse(_data: unknown): boolean {
    return true; 
  }
}
