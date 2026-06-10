import { BaseDataSource } from '../core/DataSource';

export interface ArtistTrack {
  name: string;
  playcount: number;
  listeners: number;
}

export interface ArtistData {
  name: string;
  listeners: number;
  playcount: number;
  topTracks: ArtistTrack[];
}

export class LastFmArtistProvider extends BaseDataSource<ArtistData | null> {
  id = 'lastfm_artist';
  name = 'Last.fm Artist Info';
  category = 'Music';

  private urlOrName: string;

  constructor(urlOrName: string) {
    super();
    this.urlOrName = urlOrName;
  }

  async fetchData(): Promise<ArtistData | null> {
    const apiKey = process.env.LASTFM_API_KEY;
    if (!apiKey) throw new Error('No LASTFM_API_KEY provided');

    let artistName = this.urlOrName.trim();
    const match = artistName.match(/last\.fm\/music\/([^/?]+)/);
    if (match) {
      artistName = decodeURIComponent(match[1]).replace(/\+/g, ' ');
    }

    if (!artistName) return null;

    const encodedArtist = encodeURIComponent(artistName);

    const infoRes = await fetch(
      `http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=${encodedArtist}&api_key=${apiKey}&format=json`
    );
    const infoData = await infoRes.json();

    if (!infoData.artist) return null;

    const tracksRes = await fetch(
      `http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=${encodedArtist}&api_key=${apiKey}&format=json&limit=5`
    );
    const tracksData = await tracksRes.json();

    const topTracks: ArtistTrack[] = [];
    if (tracksData.toptracks && Array.isArray(tracksData.toptracks.track)) {
      for (const t of tracksData.toptracks.track) {
        topTracks.push({
          name: t.name,
          playcount: parseInt(t.playcount, 10) || 0,
          listeners: parseInt(t.listeners, 10) || 0,
        });
      }
    }

    return {
      name: infoData.artist.name,
      listeners: parseInt(infoData.artist.stats.listeners, 10) || 0,
      playcount: parseInt(infoData.artist.stats.playcount, 10) || 0,
      topTracks,
    };
  }

  validateResponse(data: unknown): boolean {
    return !!data && typeof (data as any).name === 'string';
  }
}
