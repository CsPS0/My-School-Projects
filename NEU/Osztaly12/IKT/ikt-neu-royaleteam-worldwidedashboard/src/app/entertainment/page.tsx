"use client";
import React, { useEffect, useState } from 'react';
import { WidgetCard } from '../components/WidgetCard';
import { MonitorPlay, ExternalLink } from 'lucide-react';
export default function YouTubePage() {
  const [youtube, setYoutube] = useState<any>(null);
  const [personalYoutube, setPersonalYoutube] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    fetch('/api/dashboard')
      .then((res) => res.json())
      .then((data) => {
        setYoutube(data['youtube_stats']);
        if (data['personal_data']?.youtube) {
          setPersonalYoutube(data['personal_data'].youtube);
        }
        setLoading(false);
      })
      .catch(console.error);
  }, []);
  const formatNumber = (num: number) => {
    if (num >= 1000000000) return (num / 1000000000).toFixed(1) + 'B';
    if (num >= 1000000) return (num / 1000000).toFixed(1) + 'M';
    if (num >= 1000) return (num / 1000).toFixed(1) + 'K';
    return num.toString();
  };
  if (loading) {
    return (
      <div className="flex items-center justify-center min-h-[40vh]">
        <div className="w-12 h-12 border-4 border-transparent/20 border-t-accent rounded-full animate-spin"></div>
      </div>
    );
  }
  return (
    <div className="w-full flex flex-col xl:flex-row gap-6">
      <div className="flex-1">
        <WidgetCard id="youtube" title="Top 10 YouTubers" icon={MonitorPlay} delay={100} className="w-full h-full">
          {youtube ? (() => {
            const sortedYoutubers = Object.values(youtube).sort((a: any, b: any) => b.subscriberCount - a.subscriberCount);
            return (
              <div className="flex flex-col gap-3 p-2">
                {sortedYoutubers.map((channel: any, index: number) => (
                  <div key={channel.name} className="flex items-center justify-between p-3 rounded-lg bg-surface-inset border border-border-subtle hover:border-accent/50 transition-colors">
                    <div className="flex items-center gap-4">
                      <div className={`w-8 h-8 rounded-full flex items-center justify-center font-bold text-sm ${index === 0 ? 'bg-yellow-500/20 text-yellow-500' : index === 1 ? 'bg-gray-400/20 text-gray-400' : index === 2 ? 'bg-amber-700/20 text-amber-700' : 'bg-surface-base text-muted'}`}>
                        #{index + 1}
                      </div>
                      {channel.thumbnailUrl && (
                        <img src={channel.thumbnailUrl} alt={channel.name} className="w-10 h-10 rounded-full object-cover border-2 border-border-subtle" />
                      )}
                      <div className="font-bold text-primary">{channel.name}</div>
                    </div>
                    <div className="flex items-center gap-6">
                      <div className="text-right">
                        <div className="font-bold text-accent-warm">{formatNumber(channel.subscriberCount)}</div>
                        <div className="text-xs text-secondary">Subscribers</div>
                      </div>
                      {channel.id && (
                        <a 
                          href={`https://youtube.com/channel/${channel.id}`} 
                          target="_blank" 
                          rel="noopener noreferrer"
                          className="p-2 bg-surface-base hover:bg-accent/10 text-secondary hover:text-accent rounded-full transition-colors group"
                          title={`Visit ${channel.name} on YouTube`}
                        >
                          <ExternalLink size={16} className="group-hover:-rotate-12 transition-transform" />
                        </a>
                      )}
                    </div>
                  </div>
                ))}
              </div>
            );
          })() : (
            <div className="text-muted text-center py-12">Data unavailable</div>
          )}
        </WidgetCard>
      </div>
      <div className="xl:w-1/3">
        <WidgetCard id="personal-youtube" title="Your Channel" icon={MonitorPlay} delay={200} className="w-full h-full" theme="light">
          {personalYoutube && Object.values(personalYoutube).length > 0 ? (() => {
            const channel: any = Object.values(personalYoutube)[0];
            return (
              <div className="flex flex-col gap-6 p-4">
                <div className="text-center flex flex-col items-center gap-3">
                  {channel.thumbnailUrl && (
                    <img src={channel.thumbnailUrl} alt={channel.name} className="w-20 h-20 rounded-full object-cover border-4 border-border-subtle shadow-lg" />
                  )}
                  <h3 className="text-2xl font-bold text-primary">{channel.name}</h3>
                </div>
                <div className="flex flex-col gap-4">
                  <div className="bg-surface-inset border border-border-subtle p-4 rounded-xl text-center">
                    <div className="text-3xl font-bold text-primary mb-1">{formatNumber(channel.subscriberCount)}</div>
                    <div className="text-xs text-secondary uppercase tracking-wider font-medium">Subscribers</div>
                  </div>
                  <div className="bg-surface-inset border border-border-subtle p-4 rounded-xl text-center">
                    <div className="text-3xl font-bold text-primary mb-1">{formatNumber(channel.viewCount)}</div>
                    <div className="text-xs text-secondary uppercase tracking-wider font-medium">Total Views</div>
                  </div>
                  <div className="bg-surface-inset border border-border-subtle p-4 rounded-xl text-center">
                    <div className="text-3xl font-bold text-primary mb-1">{channel.videoCount.toLocaleString()}</div>
                    <div className="text-xs text-secondary uppercase tracking-wider font-medium">Videos</div>
                  </div>
                </div>
                {channel.latestVideo && (
                  <div className="mt-4 pt-6 border-t border-border-subtle">
                    <div className="flex items-center gap-2 mb-4">
                      <div className="w-2 h-2 rounded-full bg-accent animate-pulse"></div>
                      <h4 className="text-sm font-bold text-secondary uppercase tracking-wider">Your Latest Upload</h4>
                    </div>
                    <div className="bg-surface-inset border border-border-subtle rounded-xl overflow-hidden hover:border-accent/50 transition-colors">
                      <div className="relative aspect-video w-full bg-surface-base">
                        <img src={channel.latestVideo.thumbnailUrl} alt={channel.latestVideo.title} className="w-full h-full object-cover" />
                        <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-transparent to-transparent flex flex-col justify-end p-4">
                          <h5 className="text-white font-bold text-sm line-clamp-2">{channel.latestVideo.title}</h5>
                          <div className="text-xs text-white/70 mt-1">
                            {new Date(channel.latestVideo.publishedAt).toLocaleDateString()}
                          </div>
                        </div>
                      </div>
                      <div className="grid grid-cols-3 divide-x divide-border-subtle border-t border-border-subtle">
                        <div className="p-3 text-center">
                          <div className="text-primary font-bold text-sm">{formatNumber(channel.latestVideo.viewCount)}</div>
                          <div className="text-[10px] text-secondary uppercase mt-0.5">Views</div>
                        </div>
                        <div className="p-3 text-center">
                          <div className="text-primary font-bold text-sm">{formatNumber(channel.latestVideo.likeCount)}</div>
                          <div className="text-[10px] text-secondary uppercase mt-0.5">Likes</div>
                        </div>
                        <div className="p-3 text-center">
                          <div className="text-primary font-bold text-sm">{formatNumber(channel.latestVideo.commentCount)}</div>
                          <div className="text-[10px] text-secondary uppercase mt-0.5">Comments</div>
                        </div>
                      </div>
                    </div>
                  </div>
                )}
              </div>
            );
          })() : (
            <div className="flex flex-col items-center justify-center h-full text-muted py-12 gap-3 text-sm">
              <p>Link your YouTube channel in Settings</p>
            </div>
          )}
        </WidgetCard>
      </div>
    </div>
  );
}
