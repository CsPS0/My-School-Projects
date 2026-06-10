import { BaseDataSource } from '../core/DataSource';
export interface SteamPersonalData {
  username: string;
  avatarUrl: string;
  itemsOwned: number | null;
  hoursPlayed: number | null;
  gameCount: number | null;
  profileValue: number | null;
}
export class SteamPersonalProvider extends BaseDataSource<SteamPersonalData> {
  id = 'steam_personal';
  name = 'Steam Personal Profile';
  category = 'Games';
  private profileUrl: string;
  private apiKey: string | null;
  constructor(profileUrl: string, apiKey?: string | null) {
    super();
    this.profileUrl = profileUrl;
    this.apiKey = apiKey || null;
  }
  async fetchData(): Promise<SteamPersonalData> {
    try {
      let url = this.profileUrl.replace(/\/$/, '');
      if (!url.includes('steamcommunity.com')) {
        url = `https://steamcommunity.com/id/${url}`;
      }
      const xmlRes = await fetch(`${url}/?xml=1`, { next: { revalidate: 3600 } });
      if (!xmlRes.ok) throw new Error('Failed to fetch Steam XML profile');
      const xml = await xmlRes.text();
      const steamId64Match = xml.match(/<steamID64>(\d+)<\/steamID64>/);
      const usernameMatch = xml.match(/<steamID><!\[CDATA\[(.*?)\]\]><\/steamID>/);
      const avatarMatch = xml.match(/<avatarFull><!\[CDATA\[(.*?)\]\]><\/avatarFull>/);
      const steamId64 = steamId64Match ? steamId64Match[1] : null;
      const username = usernameMatch ? usernameMatch[1] : 'Unknown';
      const avatarUrl = avatarMatch ? avatarMatch[1] : '';
      if (!steamId64) throw new Error('Could not resolve Steam64 ID');
      let itemsOwned: number | null = null;
      try {
        const invRes = await fetch(`https://steamcommunity.com/inventory/${steamId64}/730/2?l=english&count=5000`, { next: { revalidate: 3600 } });
        if (invRes.ok) {
          const invData = await invRes.json();
          if (invData && invData.assets) {
            itemsOwned = invData.assets.length;
          }
        }
      } catch (err) {
        console.warn('Failed to fetch Steam inventory (rate limit or private)', err);
      }
      let hoursPlayed: number | null = null;
      let gameCount: number | null = null;
      if (this.apiKey && steamId64) {
        try {
          const apiRes = await fetch(`http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=${this.apiKey}&steamid=${steamId64}&format=json`, { next: { revalidate: 3600 } });
          if (apiRes.ok) {
            const data = await apiRes.json();
            if (data.response && data.response.games) {
              gameCount = data.response.game_count;
              let totalMinutes = 0;
              for (const game of data.response.games) {
                totalMinutes += game.playtime_forever;
              }
              hoursPlayed = Math.round(totalMinutes / 60);
              this.isMock = false;
            }
          }
        } catch (err) {
          console.warn('Failed to fetch Steam games via API', err);
        }
      }
      if (hoursPlayed === null) {
        try {
          const gamesRes = await fetch(`${url}/games?tab=all&xml=1`, { next: { revalidate: 3600 } });
          if (gamesRes.ok) {
            const gamesXml = await gamesRes.text();
            const gameMatches = [...gamesXml.matchAll(/<game>/g)];
            if (gameMatches.length > 0) {
              gameCount = gameMatches.length;
              const hoursMatches = [...gamesXml.matchAll(/<hoursOnRecord>([\d.]+)<\/hoursOnRecord>/g)];
              let totalHours = 0;
              for (const match of hoursMatches) {
                totalHours += parseFloat(match[1]);
              }
              hoursPlayed = Math.round(totalHours);
              this.isMock = false;
            }
          }
        } catch (err) {
          console.warn('Failed to fetch Steam games XML', err);
        }
      }
      let profileValue: number | null = null;
      if (hoursPlayed === null || hoursPlayed === 0) {
         this.isMock = true;
         gameCount = 182;
         hoursPlayed = 1834;
         itemsOwned = 204;
         profileValue = 649;
      } else {
         profileValue = (gameCount || 0) * 14.50; 
      }
      return {
        username,
        avatarUrl,
        itemsOwned,
        hoursPlayed,
        gameCount,
        profileValue: Math.round(profileValue)
      };
    } catch (error) {
      return this.handleFetchError(error);
    }
  }
  validateResponse(data: unknown): boolean {
    if (typeof data !== 'object' || data === null) return false;
    return 'username' in data;
  }
}
