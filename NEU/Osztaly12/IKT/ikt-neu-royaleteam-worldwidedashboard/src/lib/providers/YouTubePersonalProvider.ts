import { BaseDataSource } from '../core/DataSource';
export type YouTubePersonalData = {
  name: string;
  subscriberCount: number;
  viewCount: number;
  videoCount: number;
  thumbnailUrl?: string;
  latestVideo?: {
    id: string;
    title: string;
    publishedAt: string;
    viewCount: number;
    likeCount: number;
    commentCount: number;
    thumbnailUrl: string;
  };
};
export class YouTubePersonalProvider extends BaseDataSource<YouTubePersonalData> {
  id = 'youtube_personal';
  name = 'YouTube Personal Stats';
  category = 'Video';
  private handleOrId: string;
  constructor(handleOrId: string) {
    super();
    this.handleOrId = handleOrId;
    this.id = `youtube_personal_${handleOrId}`;
  }
  async fetchData(): Promise<YouTubePersonalData> {
    const apiKey = process.env.YOUTUBE_API_KEY;
    if (!apiKey || apiKey === 'your_youtube_api_key_here') {
      return {
        name: this.handleOrId,
        subscriberCount: 1500,
        viewCount: 125000,
        videoCount: 42,
        thumbnailUrl: 'https://via.placeholder.com/150',
        latestVideo: {
          id: 'dQw4w9WgXcQ',
          title: 'Never Gonna Give You Up',
          publishedAt: '2009-10-25T06:57:33Z',
          viewCount: 1500000000,
          likeCount: 18000000,
          commentCount: 450000,
          thumbnailUrl: 'https://i.ytimg.com/vi/dQw4w9WgXcQ/maxresdefault.jpg'
        }
      };
    }
    let url = '';
    if (this.handleOrId.startsWith('UC')) {
      url = `https://www.googleapis.com/youtube/v3/channels?part=statistics,snippet,contentDetails&id=${this.handleOrId}&key=${apiKey}`;
    } else {
      url = `https://www.googleapis.com/youtube/v3/channels?part=statistics,snippet,contentDetails&forHandle=${this.handleOrId}&key=${apiKey}`;
    }
    const channelRes = await fetch(url);
    if (!channelRes.ok) throw new Error(`HTTP Error: ${channelRes.status}`);
    const channelData = await channelRes.json();
    if (!channelData.items || channelData.items.length === 0) {
      throw new Error('Channel not found');
    }
    const item = channelData.items[0];
    const stats = item.statistics;
    const snippet = item.snippet;
    const contentDetails = item.contentDetails;
    const uploadsPlaylistId = contentDetails?.relatedPlaylists?.uploads;
    let latestVideo = undefined;
    if (uploadsPlaylistId) {
      const playlistRes = await fetch(
        `https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&playlistId=${uploadsPlaylistId}&maxResults=1&key=${apiKey}`
      );
      if (playlistRes.ok) {
        const playlistData = await playlistRes.json();
        if (playlistData.items && playlistData.items.length > 0) {
          const videoSnippet = playlistData.items[0].snippet;
          const videoId = videoSnippet.resourceId.videoId;
          const videoRes = await fetch(
            `https://www.googleapis.com/youtube/v3/videos?part=statistics&id=${videoId}&key=${apiKey}`
          );
          if (videoRes.ok) {
            const videoData = await videoRes.json();
            if (videoData.items && videoData.items.length > 0) {
              const videoStats = videoData.items[0].statistics;
              latestVideo = {
                id: videoId,
                title: videoSnippet.title,
                publishedAt: videoSnippet.publishedAt,
                viewCount: parseInt(videoStats.viewCount || '0', 10),
                likeCount: parseInt(videoStats.likeCount || '0', 10),
                commentCount: parseInt(videoStats.commentCount || '0', 10),
                thumbnailUrl: videoSnippet.thumbnails?.maxres?.url || videoSnippet.thumbnails?.high?.url || videoSnippet.thumbnails?.default?.url
              };
            }
          }
        }
      }
    }
    return {
      name: snippet?.title || this.handleOrId,
      subscriberCount: parseInt(stats.subscriberCount || '0', 10),
      viewCount: parseInt(stats.viewCount || '0', 10),
      videoCount: parseInt(stats.videoCount || '0', 10),
      thumbnailUrl: snippet?.thumbnails?.default?.url,
      latestVideo
    };
  }
  validateResponse(data: any): boolean {
    return data && typeof data.subscriberCount === 'number';
  }
}
