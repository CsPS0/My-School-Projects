"use client";
import React, { useEffect, useState } from 'react';
import { WidgetCard } from '../components/WidgetCard';
import { Music, HelpCircle } from 'lucide-react';
import { MusicTrack } from '../DashboardClient';
export default function MusicPage() {
  const [music, setMusic] = useState<MusicTrack[] | null>(null);
  const [personal, setPersonal] = useState<any>(null);
  const [artist, setArtist] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [country, setCountry] = useState('Global');

  useEffect(() => {
    setLoading(true);
    fetch('/api/dashboard?music_country=' + encodeURIComponent(country), { cache: 'no-store' })
      .then((res) => res.json())
      .then((data) => {
        setMusic(data[`global_music_${country.replace(/\s+/g, '_')}`]);
        if (data['lastfm_personal']) {
          setPersonal(data['lastfm_personal']);
        }
        if (data['personal_data'] && data['personal_data'].artist) {
          setArtist(data['personal_data'].artist);
        }
        setLoading(false);
      })
      .catch((err) => {
        console.error(err);
        setLoading(false);
      });
  }, [country]);
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
    <div className="w-full grid grid-cols-1 xl:grid-cols-2 gap-6 items-start">
      <WidgetCard id="favorite-artist" title={artist ? `Your Favourite Artist: ${artist.name}` : "Favorite Artist"} icon={Music} delay={50} className="w-full xl:col-span-2">
        {artist ? (
          <div className="grid grid-cols-1 md:grid-cols-3 gap-6 p-4">
            <div className="flex flex-col items-center justify-center py-8 bg-surface-inset border border-border-subtle rounded-xl h-full shadow-inner">
              <div className="text-4xl font-black text-primary tracking-tight text-center px-4 leading-tight">{artist.name}</div>
              <div className="text-accent-warm font-bold text-2xl mt-4">{formatNumber(artist.listeners)} <span className="text-sm font-medium text-secondary">Listeners</span></div>
              <div className="text-muted text-sm mt-1.5 font-medium">{formatNumber(artist.playcount)} total plays</div>
            </div>
            
            <div className="md:col-span-2 flex flex-col justify-center">
              <h4 className="text-sm font-bold text-secondary uppercase tracking-wider mb-3 px-1">Top Tracks</h4>
              <div className="flex flex-col gap-2.5">
                {artist.topTracks.map((track: any, idx: number) => (
                  <div key={idx} className="flex items-center gap-4 p-3 rounded-xl bg-surface-inset border border-border-subtle hover:bg-surface-card-hover transition-colors">
                    <div className="w-6 text-center font-bold text-lg text-muted">{idx + 1}</div>
                    <div className="flex-1 min-w-0">
                      <div className="text-base font-bold text-primary truncate">{track.name}</div>
                    </div>
                    <div className="flex flex-col items-end shrink-0 pl-2">
                      <div className="font-bold text-accent-warm text-lg">{formatNumber(track.playcount)}</div>
                      <div className="text-[10px] uppercase font-bold tracking-wider text-muted mt-0.5">plays</div>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          </div>
        ) : (
          <div className="flex flex-col items-center justify-center min-h-[200px] text-muted py-10 gap-3 text-sm">
            <HelpCircle size={40} className="text-border-strong mb-2" />
            <p>No Favorite Artist configured</p>
            <p>Set a Last.fm Artist URL in Settings</p>
          </div>
        )}
      </WidgetCard>

      <WidgetCard id="personal-music" title={personal ? `Listening Activity: ${personal.username}` : "Listening Activity"} icon={Music} delay={100} className="w-full" theme="accent">
        {personal ? (
          <div className="flex flex-col gap-3 p-4">
            {personal.recentTracks.map((track: any, idx: number) => (
              <div key={idx} className={`flex items-center gap-4 p-3 rounded-xl border transition-colors ${track.nowPlaying ? 'bg-accent/10 border-accent' : 'bg-surface-inset border-border-subtle hover:bg-surface-card-hover'}`}>
                {track.imageUrl ? (
                  <img src={track.imageUrl} alt={track.album} className={`w-12 h-12 rounded-md shadow-sm object-cover shrink-0 ${track.nowPlaying ? 'animate-pulse' : ''}`} />
                ) : (
                  <div className="w-12 h-12 rounded-md bg-surface-base flex items-center justify-center shadow-sm shrink-0">
                    <Music size={20} className="text-muted" />
                  </div>
                )}
                <div className="flex-1 min-w-0 ml-1">
                  <div className="text-lg font-bold text-primary mb-0.5 truncate">{track.name}</div>
                  <div className="text-sm text-secondary truncate">{track.artist}</div>
                  <div className="text-[11px] text-muted truncate mt-0.5">{track.album}</div>
                </div>
                <div className="flex flex-col items-end shrink-0 pl-2">
                  <div className={`text-xs font-semibold ${track.nowPlaying ? 'text-accent-warm flex items-center gap-1.5' : 'text-muted'}`}>
                    {track.nowPlaying && <span className="w-2 h-2 rounded-full bg-accent-warm animate-pulse"></span>}
                    {track.date}
                  </div>
                </div>
              </div>
            ))}
          </div>
        ) : (
          <div className="flex flex-col items-center justify-center min-h-[300px] text-muted py-12 gap-3 text-sm">
            <p>Link your Last.fm profile in Settings</p>
          </div>
        )}
      </WidgetCard>

      <WidgetCard id="music" title={country === 'Global' ? 'Global Top 10 Music' : `Top 10 Music in ${country}`} icon={Music} delay={200} className="w-full">
        <div className="flex flex-wrap gap-2 px-4 pt-4 pb-2 border-b border-border-subtle mb-2">
          {['Global', 'USA', 'UK', 'France', 'Hungary', 'Romania'].map(c => (
            <button
              key={c}
              onClick={() => setCountry(c)}
              className={`px-3 py-1.5 rounded-lg text-sm font-medium transition-colors ${country === c ? 'bg-accent text-white shadow-md' : 'bg-surface-inset text-secondary hover:text-primary hover:bg-surface-card-hover border border-border-subtle'}`}
            >
              {c}
            </button>
          ))}
        </div>
        {music ? (
          <div className="flex flex-col gap-4 p-4">
            {music.map((track) => (
              <div key={track.rank} className="flex items-center gap-4 bg-surface-inset border border-border-subtle p-3 rounded-xl hover:bg-surface-card-hover transition-colors">
                <div className="w-8 shrink-0 text-center font-bold text-2xl text-muted">
                  {track.rank}
                </div>
                {track.imageUrl ? (
                  <img src={track.imageUrl} alt={track.title} className="w-12 h-12 rounded-md object-cover shadow-sm shrink-0 bg-surface-base" />
                ) : (
                  <div className="w-12 h-12 rounded-md bg-surface-base flex items-center justify-center shadow-sm shrink-0">
                    <Music size={20} className="text-muted" />
                  </div>
                )}
                <div className="flex-1 min-w-0 ml-1">
                  <div className="text-lg font-bold text-primary mb-0.5 truncate">{track.title}</div>
                  <div className="text-sm text-secondary truncate">{track.artist}</div>
                  {track.releaseDate && (
                    <div className="text-[11px] text-muted truncate mt-0.5">Released: {track.releaseDate}</div>
                  )}
                </div>
                <div className="flex flex-col items-end shrink-0">
                  <div className="font-bold text-xl text-accent-warm">{formatNumber(track.totalStreams)}</div>
                  <div className="text-xs text-secondary mt-1 flex items-center justify-end gap-1 group relative">
                    Playcount
                    <HelpCircle size={12} className="text-muted cursor-help" />
                    <div className="absolute bottom-full right-0 mb-2 w-48 p-2 bg-surface-card border border-border-default rounded-lg text-xs text-secondary shadow-xl opacity-0 group-hover:opacity-100 transition-opacity pointer-events-none z-10 text-center">
                      All-time plays since release. Rank is based on this week&apos;s streams.
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        ) : (
          <div className="text-muted text-center py-12">Data unavailable</div>
        )}
      </WidgetCard>
    </div>
  );
}
