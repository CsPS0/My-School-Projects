"use client";
import React, { useEffect, useState } from 'react';
import Link from 'next/link';
import {
  Gamepad2,
  MonitorPlay,
  Bitcoin,
  Landmark,
  Music,
  ArrowRight,
  TrendingUp,
  TrendingDown
} from 'lucide-react';
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
interface DashboardData {
  trn_gaming?: any;
  steam_players?: Record<string, { player_count: number; result: number }>;
  youtube_stats?: Record<string, { name: string; subscriberCount: number; viewCount: number; videoCount: number; thumbnailUrl?: string }>;
  finance_markets?: {
    crypto: { 
      bitcoin: { usd: number; eur: number }; 
      ethereum: { usd: number; eur: number };
    };
    currency: { EUR_TO_HUF: number; EUR_TO_USD: number };
  };
  world_politics?: {
    turnoutPercentage: number;
    globalLeaders: { id: string }[];
  };
  global_music_Global?: MusicTrack[];
  lastfm_personal?: {
    username: string;
    recentTracks: {
      name: string;
      artist: string;
      nowPlaying: boolean;
      imageUrl?: string;
    }[];
  };
  personal_data?: {
    youtube?: Record<string, { name: string; subscriberCount: number; viewCount: number; videoCount: number; thumbnailUrl?: string }>;
    steam?: { username: string; avatarUrl: string };
  };
}
export default function DashboardClient() {
  const [data, setData] = useState<DashboardData | null>(null);
  const [hiddenWidgets, setHiddenWidgets] = useState<string[]>([]);
  const [dashboardStyle, setDashboardStyle] = useState<string>('grid');
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    Promise.all([
      fetch('/api/dashboard').then(res => res.json()),
      fetch('/api/auth/preferences').then(res => res.ok ? res.json() : {}) as Promise<any>
    ])
    .then(([dashboardData, prefData]) => {
      setData(dashboardData);
      if (prefData.dashboardStyle) {
        setDashboardStyle(prefData.dashboardStyle);
      }
      try {
        setHiddenWidgets(JSON.parse(prefData.hiddenWidgets || '[]'));
      } catch {
        setHiddenWidgets([]);
      }
      setLoading(false);
    })
    .catch((err) => {
      console.error(err);
      setLoading(false);
    });
  }, []);
  const formatNumber = (num: number) => {
    if (num >= 1000000000) return (num / 1000000000).toFixed(1) + 'B';
    if (num >= 1000000) return (num / 1000000).toFixed(1) + 'M';
    if (num >= 1000) return (num / 1000).toFixed(1) + 'K';
    return num.toString();
  };
  if (loading) {
    return (
      <div className="flex items-center justify-center min-h-[60vh]">
        <div className="w-12 h-12 border-4 border-transparent/20 border-t-accent rounded-full animate-spin"></div>
      </div>
    );
  }
  const btc = data?.finance_markets?.crypto?.bitcoin?.usd;
  const eth = data?.finance_markets?.crypto?.ethereum?.usd;
  const eurToHuf = data?.finance_markets?.currency?.EUR_TO_HUF;
  const steamPlayers = data?.steam_players?.['0']?.player_count;
  const topYoutuber = data?.youtube_stats ? Object.values(data.youtube_stats).sort((a, b) => b.subscriberCount - a.subscriberCount)[0] : null;
  const personalYoutuber = data?.personal_data?.youtube ? Object.values(data.personal_data.youtube)[0] : null;
  const personalMusic = data?.lastfm_personal;
  const topGlobalTrack = data?.global_music_Global?.[0];
  const globalLeadersCount = data?.world_politics?.globalLeaders?.length || 0;
  return (
    <div className={`w-full space-y-8 pb-20 ${dashboardStyle === 'compact' ? 'max-w-6xl mx-auto' : ''}`}>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 grid-flow-row-dense">
        {!hiddenWidgets.includes('crypto') && (
          <Link href="/crypto" className="lg:col-span-2 lg:row-span-1 md:col-span-2 group block bg-surface-card border-border-subtle border rounded-2xl p-6 hover:border-accent hover:shadow-xl hover:shadow-accent/5 transition-all relative overflow-hidden">
            <div className="absolute top-0 right-0 p-4 opacity-0 group-hover:opacity-100 transition-opacity translate-x-2 group-hover:translate-x-0">
              <ArrowRight className="text-accent" />
            </div>
            <div className="flex items-center gap-3 mb-6">
              <div className="w-10 h-10 rounded-full bg-accent/10 text-accent flex items-center justify-center">
                <Bitcoin size={24} />
              </div>
              <h2 className="text-xl font-bold text-primary">Crypto Markets</h2>
            </div>
            <div className="space-y-4">
              <div className="bg-surface-base p-4 rounded-xl border border-border-subtle flex justify-between items-center group-hover:border-accent/30 transition-colors">
                <span className="font-bold text-secondary">Bitcoin</span>
                <div className="text-right">
                  <span className="text-lg font-bold text-primary block">${btc?.toLocaleString() || '---'}</span>
                  <span className="text-xs text-accent flex items-center gap-1 justify-end"><TrendingUp size={12}/> +2.4%</span>
                </div>
              </div>
              <div className="bg-surface-base p-4 rounded-xl border border-border-subtle flex justify-between items-center group-hover:border-accent/30 transition-colors">
                <span className="font-bold text-secondary">Ethereum</span>
                <div className="text-right">
                  <span className="text-lg font-bold text-primary block">${eth?.toLocaleString() || '---'}</span>
                  <span className="text-xs text-accent flex items-center gap-1 justify-end"><TrendingDown size={12}/> -0.8%</span>
                </div>
              </div>
            </div>
          </Link>
        )}
        {!hiddenWidgets.includes('steam') && (
          <Link href="/gaming" className="lg:col-span-1 lg:row-span-1 md:col-span-1 group block bg-surface-card border-border-subtle border rounded-2xl p-6 hover:border-accent hover:shadow-xl hover:shadow-accent/5 transition-all relative overflow-hidden">
            <div className="absolute top-0 right-0 p-4 opacity-0 group-hover:opacity-100 transition-opacity translate-x-2 group-hover:translate-x-0">
              <ArrowRight className="text-accent" />
            </div>
            <div className="flex items-center gap-3 mb-6">
              <div className="w-10 h-10 rounded-full bg-accent/10 text-accent flex items-center justify-center">
                <Gamepad2 size={24} />
              </div>
              <h2 className="text-xl font-bold text-primary">Gaming Pulse</h2>
            </div>
            <div className="flex flex-col h-[calc(100%-4rem)] justify-center gap-4">
              <div className="text-center bg-surface-base py-6 rounded-xl border border-border-subtle group-hover:border-accent/30 transition-colors">
                <div className="text-4xl font-bold text-primary mb-1">{steamPlayers ? formatNumber(steamPlayers) : '---'}</div>
                <div className="text-xs text-secondary font-bold uppercase tracking-wider">Players Online</div>
              </div>
              <div className="text-center text-sm text-muted">
                Click to view detailed charts, top games, and your personal tracker stats.
              </div>
            </div>
          </Link>
        )}
        {!hiddenWidgets.includes('youtube') && (
          <Link href="/entertainment" className="lg:col-span-1 lg:row-span-2 md:col-span-1 group block bg-surface-card border-border-subtle border rounded-2xl p-6 hover:border-accent hover:shadow-xl hover:shadow-accent/5 transition-all relative overflow-hidden">
            <div className="absolute top-0 right-0 p-4 opacity-0 group-hover:opacity-100 transition-opacity translate-x-2 group-hover:translate-x-0">
              <ArrowRight className="text-accent" />
            </div>
            <div className="flex items-center gap-3 mb-6">
              <div className="w-10 h-10 rounded-full bg-accent/10 text-accent flex items-center justify-center">
                <MonitorPlay size={24} />
              </div>
              <h2 className="text-xl font-bold text-primary">YouTube Stats</h2>
            </div>
            {personalYoutuber ? (
              <div className="bg-surface-base border border-border-subtle p-4 rounded-xl text-center flex flex-col items-center gap-3 group-hover:border-accent/30 transition-colors">
                {personalYoutuber.thumbnailUrl && (<img src={personalYoutuber.thumbnailUrl} alt="Your Avatar" className="w-16 h-16 rounded-full border-2 border-border-subtle" />
                )}
                <div className="w-full mt-2">
                  <h3 className="font-bold text-xl text-primary mb-4">{personalYoutuber.name}</h3>
                  <div className="flex flex-col gap-2">
                    <div className="flex justify-between items-center bg-surface-card px-3 py-2 rounded-lg border border-border-subtle">
                      <span className="text-secondary text-xs font-bold uppercase">Subscribers</span>
                      <span className="text-accent-warm font-bold">{formatNumber(personalYoutuber.subscriberCount)}</span>
                    </div>
                    <div className="flex justify-between items-center bg-surface-card px-3 py-2 rounded-lg border border-border-subtle">
                      <span className="text-secondary text-xs font-bold uppercase">Total Views</span>
                      <span className="text-primary font-bold">{formatNumber(personalYoutuber.viewCount)}</span>
                    </div>
                    <div className="flex justify-between items-center bg-surface-card px-3 py-2 rounded-lg border border-border-subtle">
                      <span className="text-secondary text-xs font-bold uppercase">Videos</span>
                      <span className="text-primary font-bold">{formatNumber(personalYoutuber.videoCount)}</span>
                    </div>
                  </div>
                </div>
              </div>
            ) : topYoutuber ? (
              <div className="bg-surface-base border border-border-subtle p-4 rounded-xl text-center flex flex-col items-center gap-3 group-hover:border-accent/30 transition-colors">
                {topYoutuber.thumbnailUrl && (<img src={topYoutuber.thumbnailUrl} alt="Top Avatar" className="w-16 h-16 rounded-full border-2 border-border-subtle" />
                )}
                <div className="text-xs bg-accent/20 text-accent px-2 py-0.5 rounded font-bold uppercase">#1 Global</div>
                <div className="w-full mt-2">
                  <h3 className="font-bold text-xl text-primary mb-4">{topYoutuber.name}</h3>
                  <div className="flex flex-col gap-2">
                    <div className="flex justify-between items-center bg-surface-card px-3 py-2 rounded-lg border border-border-subtle">
                      <span className="text-secondary text-xs font-bold uppercase">Subscribers</span>
                      <span className="text-accent-warm font-bold">{formatNumber(topYoutuber.subscriberCount)}</span>
                    </div>
                    <div className="flex justify-between items-center bg-surface-card px-3 py-2 rounded-lg border border-border-subtle">
                      <span className="text-secondary text-xs font-bold uppercase">Total Views</span>
                      <span className="text-primary font-bold">{formatNumber(topYoutuber.viewCount)}</span>
                    </div>
                    <div className="flex justify-between items-center bg-surface-card px-3 py-2 rounded-lg border border-border-subtle">
                      <span className="text-secondary text-xs font-bold uppercase">Videos</span>
                      <span className="text-primary font-bold">{formatNumber(topYoutuber.videoCount)}</span>
                    </div>
                  </div>
                </div>
              </div>
            ) : (
               <div className="text-muted text-center py-6 text-sm">Loading YouTube...</div>
            )}
          </Link>
        )}
        {!hiddenWidgets.includes('music') && (
          <Link href="/music" className="lg:col-span-2 lg:row-span-1 md:col-span-2 group block bg-surface-card border-border-subtle border rounded-2xl p-6 hover:border-accent hover:shadow-xl hover:shadow-accent/5 transition-all relative overflow-hidden">
            <div className="absolute top-0 right-0 p-4 opacity-0 group-hover:opacity-100 transition-opacity translate-x-2 group-hover:translate-x-0">
              <ArrowRight className="text-accent" />
            </div>
            <div className="flex items-center gap-3 mb-6">
              <div className="w-10 h-10 rounded-full bg-accent/10 text-accent flex items-center justify-center">
                <Music size={24} />
              </div>
              <h2 className="text-xl font-bold text-primary">Music Hub</h2>
            </div>
            {personalMusic && personalMusic.recentTracks.length > 0 ? (
              <div className="bg-surface-base border border-border-subtle p-4 rounded-xl flex items-center gap-4 group-hover:border-accent/30 transition-colors">
                {personalMusic.recentTracks[0].imageUrl ? (<img src={personalMusic.recentTracks[0].imageUrl} alt="Album Art" className="w-14 h-14 rounded shadow-sm object-cover" />
                ) : (
                  <div className="w-14 h-14 rounded bg-surface-inset flex items-center justify-center"><Music size={20} className="text-muted"/></div>
                )}
                <div className="flex-1 min-w-0">
                  <div className="text-xs font-bold text-accent uppercase tracking-wider mb-1 flex items-center gap-1.5">
                    {personalMusic.recentTracks[0].nowPlaying ? <><span className="w-2 h-2 rounded-full bg-accent animate-pulse"></span> Now Playing</> : 'Last Played'}
                  </div>
                  <div className="font-bold text-primary truncate">{personalMusic.recentTracks[0].name}</div>
                  <div className="text-sm text-secondary truncate">{personalMusic.recentTracks[0].artist}</div>
                </div>
              </div>
            ) : topGlobalTrack ? (
              <div className="bg-surface-base border border-border-subtle p-4 rounded-xl flex items-center gap-4 group-hover:border-accent/30 transition-colors">
                {topGlobalTrack.imageUrl ? (<img src={topGlobalTrack.imageUrl} alt="Album Art" className="w-14 h-14 rounded shadow-sm object-cover" />
                ) : (
                  <div className="w-14 h-14 rounded bg-surface-inset flex items-center justify-center"><Music size={20} className="text-muted"/></div>
                )}
                <div className="flex-1 min-w-0">
                  <div className="text-xs font-bold text-accent uppercase tracking-wider mb-1">Global #1</div>
                  <div className="font-bold text-primary truncate">{topGlobalTrack.title}</div>
                  <div className="text-sm text-secondary truncate">{topGlobalTrack.artist}</div>
                </div>
              </div>
            ) : (
              <div className="text-muted text-center py-6 text-sm">Loading Music...</div>
            )}
          </Link>
        )}
        {!hiddenWidgets.includes('politics') && (
          <Link href="/politics" className="lg:col-span-1 lg:row-span-1 md:col-span-1 group block bg-surface-card border-border-subtle border rounded-2xl p-6 hover:border-accent hover:shadow-xl hover:shadow-accent/5 transition-all relative overflow-hidden">
            <div className="absolute top-0 right-0 p-4 opacity-0 group-hover:opacity-100 transition-opacity translate-x-2 group-hover:translate-x-0">
              <ArrowRight className="text-accent" />
            </div>
            <div className="flex items-center gap-3 mb-6">
              <div className="w-10 h-10 rounded-full bg-accent/10 text-accent flex items-center justify-center">
                <Landmark size={24} />
              </div>
              <h2 className="text-xl font-bold text-primary">World Politics</h2>
            </div>
            <div className="flex flex-col h-[calc(100%-4rem)] justify-center gap-4">
              <div className="text-center bg-surface-base py-6 rounded-xl border border-border-subtle group-hover:border-accent/30 transition-colors">
                <div className="text-4xl font-bold text-primary mb-1">{globalLeadersCount}</div>
                <div className="text-xs text-secondary font-bold uppercase tracking-wider">World Leaders Tracked</div>
              </div>
            </div>
          </Link>
        )}
        {!hiddenWidgets.includes('exchange') && (
          <Link href="/exchange" className="lg:col-span-1 lg:row-span-1 md:col-span-1 group block bg-surface-card border-border-subtle border rounded-2xl p-6 hover:border-accent hover:shadow-xl hover:shadow-accent/5 transition-all relative overflow-hidden">
            <div className="absolute top-0 right-0 p-4 opacity-0 group-hover:opacity-100 transition-opacity translate-x-2 group-hover:translate-x-0">
              <ArrowRight className="text-accent" />
            </div>
            <div className="flex items-center gap-3 mb-6">
              <div className="w-10 h-10 rounded-full bg-accent/10 text-accent flex items-center justify-center">
                <TrendingUp size={24} />
              </div>
              <h2 className="text-xl font-bold text-primary">Exchange Rates</h2>
            </div>
            <div className="flex flex-col h-[calc(100%-4rem)] justify-center gap-4">
              <div className="bg-surface-base p-4 rounded-xl border border-border-subtle flex justify-between items-center group-hover:border-accent/30 transition-colors">
                <span className="font-bold text-secondary text-lg">EUR / HUF</span>
                <span className="text-2xl font-bold text-primary">{eurToHuf ? eurToHuf.toFixed(2) : '---'}</span>
              </div>
              <div className="text-center text-sm text-muted">
                Click to view full currency exchange data and historical charts.
              </div>
            </div>
          </Link>
        )}
      </div>
    </div>
  );
}
