import { BaseDataSource } from '../core/DataSource';
 
export interface ExophaseGame {
  id: string;
  title: string;
  platform: string;
  playtimeStr: string;
  awardsEarned: number;
  awardsPossible: number;
  lastPlayed: number; 
  coverUrl: string;
}
export interface ExophaseData {
  username: string;
  games: ExophaseGame[];
}
export class ExophaseProvider extends BaseDataSource<ExophaseData> {
  id = 'exophase_stats';
  name = 'Exophase Profile Stats';
  category = 'Games';
  private username: string;
  constructor(urlOrUsername: string) {
    super();
    let parsedUsername = urlOrUsername.trim();
    const match = parsedUsername.match(/exophase\.com\/user\/([^/?]+)/);
    if (match) {
      parsedUsername = match[1];
    }
    this.username = parsedUsername;
  }
  async fetchData(): Promise<ExophaseData> {
    try {
      this.isMock = true;
      const games: ExophaseGame[] = [
        {
          id: '1',
          title: 'Suit For Hire',
          platform: 'Steam',
          playtimeStr: '5h 28m',
          awardsEarned: 40,
          awardsPossible: 40,
          lastPlayed: 1780617600, 
          coverUrl: 'https://cdn.cloudflare.steamstatic.com/steam/apps/1612420/header.jpg'
        },
        {
          id: '2',
          title: 'Shadow Warrior 3: Definitive Edition',
          platform: 'Steam',
          playtimeStr: '7h 49m',
          awardsEarned: 38,
          awardsPossible: 38,
          lastPlayed: 1780617600, 
          coverUrl: 'https://m.exophase.com/steam/games/m/16d6b8.png?08b266ce53a558800d3c10d1e43abb60'
        },
        {
          id: '3',
          title: 'Clash Royale',
          platform: 'Google Play',
          playtimeStr: '67h 41m',
          awardsEarned: 21,
          awardsPossible: 30,
          lastPlayed: 1780617600, 
          coverUrl: 'https://m.exophase.com/android/games/m/0je847.png?9d46342de113f079cd72c61cb571e064'
        }
      ];
      return {
        username: this.username || 'CsPS',
        games
      };
    } catch (error) {
      return this.handleFetchError(error);
    }
  }
  validateResponse(data: unknown): boolean {
    if (typeof data !== 'object' || data === null) return false;
    return 'games' in data;
  }
}
