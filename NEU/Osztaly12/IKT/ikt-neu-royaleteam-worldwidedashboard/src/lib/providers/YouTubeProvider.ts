import { BaseDataSource } from '../core/DataSource';
export interface ChannelInfo {
  name: string;
  id: string;
}
export type YouTubeData = Record<string, {
  name: string;
  id: string;
  subscriberCount: number;
  viewCount: number;
  videoCount: number;
  thumbnailUrl?: string;
}>;
export class YouTubeProvider extends BaseDataSource<YouTubeData> {
  id = 'youtube_stats';
  name = 'YouTube Channel Stats';
  category = 'Video';
  private channels: ChannelInfo[];
  constructor(channels: ChannelInfo[]) {
    super();
    this.channels = channels;
  }
  async fetchData(): Promise<YouTubeData> {
    try {
      const apiKey = process.env.YOUTUBE_API_KEY;
      if (!apiKey || apiKey === 'your_youtube_api_key_here') {
        return this.channels.reduce((acc, ch) => ({
          ...acc,
          [ch.name]: {
            name: ch.name,
            id: ch.id,
            subscriberCount: 260000000,
            viewCount: 45000000000,
            videoCount: 800,
            thumbnailUrl: 'https://via.placeholder.com/150'
          }
        }), {} as YouTubeData);
      }
      const ids = this.channels.map(c => c.id).join(',');
      const response = await fetch(
        `https://www.googleapis.com/youtube/v3/channels?part=statistics,snippet&id=${ids}&key=${apiKey}`
      );
      if (!response.ok) {
        throw new Error(`HTTP Error: ${response.status}`);
      }
      const data = await response.json();
      const results: YouTubeData = {};
      data.items?.forEach((item: any) => {
        const id = item.id as string;
        const stats = item.statistics as Record<string, string>;
        const snippet = item.snippet;
        const chInfo = this.channels.find(c => c.id === id);
        if (chInfo && stats) {
          results[chInfo.name] = {
            name: chInfo.name,
            id: chInfo.id,
            subscriberCount: parseInt(stats.subscriberCount, 10),
            viewCount: parseInt(stats.viewCount, 10),
            videoCount: parseInt(stats.videoCount, 10),
            thumbnailUrl: snippet?.thumbnails?.default?.url
          };
        }
      });
      if (Object.keys(results).length === 0) {
        throw new Error('No statistics found for the channels.');
      }
      return results;
    } catch (error) {
      return this.handleFetchError(error);
    }
  }
  validateResponse(data: unknown): boolean {
    if (typeof data !== 'object' || data === null) return false;
    return Object.keys(data).length > 0;
  }
}
