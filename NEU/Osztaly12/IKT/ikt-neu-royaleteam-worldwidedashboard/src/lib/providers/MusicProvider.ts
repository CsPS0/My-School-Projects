import { BaseDataSource } from '../core/DataSource';
export interface MusicTrack {
  rank: number;
  title: string;
  artist: string;
  spotifyStreams: number;
  soundcloudStreams: number;
  totalStreams: number;
  imageUrl?: string;
  releaseDate?: string;
}
export class MusicProvider extends BaseDataSource<MusicTrack[]> {
  id: string;
  name: string;
  category = 'Entertainment';
  private country: string;
  constructor(country: string = 'Global') {
    super();
    this.country = country;
    this.id = `global_music_${country.replace(/\s+/g, '_')}`;
    this.name = country === 'Global' ? 'Global Top 10 Music' : `Top 10 Music in ${country}`;
  }
  async fetchData(): Promise<MusicTrack[]> {
    const fallbackTracks = [
      { title: 'Blinding Lights', artist: 'The Weeknd', spotify: 4100000000, soundcloud: 250000000, imageUrl: 'https://lastfm.freetls.fastly.net/i/u/174s/2a96cbd8b46e442fc41c2b86b821562f.png', releaseDate: '29 Nov 2019' },
      { title: 'Shape of You', artist: 'Ed Sheeran', spotify: 3800000000, soundcloud: 190000000 },
      { title: 'Someone You Loved', artist: 'Lewis Capaldi', spotify: 3300000000, soundcloud: 140000000 },
      { title: 'Sunflower', artist: 'Post Malone & Swae Lee', spotify: 3200000000, soundcloud: 310000000 },
      { title: 'Starboy', artist: 'The Weeknd', spotify: 3100000000, soundcloud: 280000000 },
      { title: 'As It Was', artist: 'Harry Styles', spotify: 3000000000, soundcloud: 110000000 },
      { title: 'Dance Monkey', artist: 'Tones And I', spotify: 3000000000, soundcloud: 180000000 },
      { title: 'One Dance', artist: 'Drake', spotify: 2900000000, soundcloud: 400000000 },
      { title: 'STAY', artist: 'The Kid LAROI & Justin Bieber', spotify: 2800000000, soundcloud: 150000000 },
      { title: 'rockstar', artist: 'Post Malone', spotify: 2800000000, soundcloud: 350000000 },
    ];
    try {
      const apiKey = process.env.LASTFM_API_KEY;
      if (!apiKey) throw new Error('No API Key');
      let url = '';
      if (this.country === 'Global') {
        url = `http://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key=${apiKey}&format=json&limit=10`;
      } else {
        let mappedCountry = this.country;
        if (mappedCountry === 'USA') mappedCountry = 'United States';
        if (mappedCountry === 'UK') mappedCountry = 'United Kingdom';
        url = `http://ws.audioscrobbler.com/2.0/?method=geo.gettoptracks&country=${encodeURIComponent(mappedCountry)}&api_key=${apiKey}&format=json&limit=10`;
      }
      const res = await fetch(url, {
        next: { revalidate: 3600 } 
      });
      if (!res.ok) throw new Error('Last.fm API failed');
      const data = await res.json();
      const tracks = data.tracks?.track || [];
      if (tracks.length === 0) throw new Error('No tracks found');
      const tracksData = tracks.map((t: any, index: number) => ({
        rank: index + 1,
        title: t.name,
        artist: t.artist?.name || 'Unknown Artist',
        spotifyStreams: 0,
        soundcloudStreams: 0,
        totalStreams: parseInt(t.playcount || t.listeners, 10) || 0,
        imageUrl: t.image?.find((img: any) => img.size === 'large')?.['#text'] || t.image?.[0]?.['#text'],
        releaseDate: undefined,
      }));
      const enrichedTracks = await Promise.all(tracksData.map(async (t: any) => {
        try {
          const infoRes = await fetch(`http://ws.audioscrobbler.com/2.0/?method=track.getInfo&artist=${encodeURIComponent(t.artist)}&track=${encodeURIComponent(t.title)}&api_key=${apiKey}&format=json`);
          if (infoRes.ok) {
            const infoData = await infoRes.json();
            const realImg = infoData.track?.album?.image?.find((img: any) => img.size === 'extralarge' || img.size === 'large')?.['#text'];
            if (realImg && !realImg.includes('2a96cbd8b46e442fc41c2b86b821562f')) {
              t.imageUrl = realImg;
            }
            if (infoData.track?.wiki?.published) {
              const pub = infoData.track.wiki.published;
              t.releaseDate = pub.split(',')[0].trim();
            }
          }
        } catch {
        }
        return t;
      }));
      return enrichedTracks;
    } catch (error) {
      console.warn(`Last.fm ${this.country} fetch failed. Using fallback.`, error);
      const processedTracks: MusicTrack[] = fallbackTracks.map((t) => ({
        rank: 0, 
        title: t.title,
        artist: t.artist,
        spotifyStreams: t.spotify,
        soundcloudStreams: t.soundcloud,
        totalStreams: t.spotify + t.soundcloud,
      })).sort((a, b) => b.totalStreams - a.totalStreams);
      processedTracks.forEach((track, index) => {
        track.rank = index + 1;
      });
      return processedTracks;
    }
  }
  validateResponse(data: unknown): boolean {
    if (!Array.isArray(data)) return false;
    if (data.length === 0) return false;
    const first = data[0] as Record<string, unknown>;
    return (
      'title' in first &&
      'artist' in first &&
      'totalStreams' in first
    );
  }
}
