"use client";
import React, { useEffect, useState } from 'react';
import { WidgetCard } from '../components/WidgetCard';
import { Gamepad2, Trophy, Clock, User } from 'lucide-react';
export default function SteamPage() {
  const [steamPlayers, setSteamPlayers] = useState<Record<string, { player_count: number }>>({});
  const [selectedApp, setSelectedApp] = useState<string>('0');
  const [exophaseGames, setExophaseGames] = useState<any[]>([]);

  const [steamCharts, setSteamCharts] = useState<any | null>(null);
  const [activeTab, setActiveTab] = useState<'online' | 'mostPlayed' | 'wishlisted'>('online');
  const [trn, setTrn] = useState<any | null>(null);
  const [selectedTrnGame, setSelectedTrnGame] = useState<string>('');
  const [steamPersonal, setSteamPersonal] = useState<any | null>(null);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    fetch('/api/dashboard')
      .then((res) => res.json())
      .then((data) => {
        setSteamPlayers(data['steam_players'] || {});
        if (data['exophase_stats']) {
          setExophaseGames(data['exophase_stats'].games || []);
        }
        if (data['trn_gaming']) {
          setTrn(data['trn_gaming']);
          const games = Object.keys(data['trn_gaming']);
          if (games.length > 0) {
            setSelectedTrnGame(games[0]);
          }
        }
        if (data['steam_top_games']) {
          setSteamCharts(data['steam_top_games']);
        }
        if (data['steam_personal']) {
          setSteamPersonal(data['steam_personal']);
        }
        setLoading(false);
      })
      .catch(() => setLoading(false));
  }, []);
  if (loading) {
    return (
      <div className="flex items-center justify-center min-h-[40vh]">
        <div className="w-10 h-10 border-[3px] border-border-subtle border-t-accent rounded-full animate-spin" />
      </div>
    );
  }
  return (
    <div className="w-full grid grid-cols-1 lg:grid-cols-2 gap-6 pb-20">
      <div className="grid gap-6 h-full" style={{ gridTemplateRows: 'auto auto auto 1fr' }}>
      <WidgetCard id="steam-profile" title="Your Steam Profile" icon={User} delay={50}>
        {steamPersonal ? (
          <div className="flex items-center gap-6 p-2">
            <div className="w-20 h-20 rounded-xl overflow-hidden bg-surface-inset shrink-0 border border-border-subtle">
              <img src={steamPersonal.avatarUrl} alt={steamPersonal.username} className="w-full h-full object-cover" />
            </div>
            <div className="flex-1">
              <h3 className="text-xl font-bold text-primary mb-1">{steamPersonal.username}</h3>
              <div className="flex items-center gap-4 text-sm text-secondary flex-wrap mt-2">
                {steamPersonal.gameCount !== null && (
                  <div className="flex items-center gap-1.5">
                    <Gamepad2 size={14} className="text-accent" />
                    <span className="font-semibold text-primary">{steamPersonal.gameCount}</span> Games
                  </div>
                )}
                {steamPersonal.itemsOwned !== null && (
                  <div className="flex items-center gap-1.5">
                    <Gamepad2 size={14} className="text-accent" />
                    <span className="font-semibold text-primary">{steamPersonal.itemsOwned}</span> Items
                  </div>
                )}
                {steamPersonal.hoursPlayed !== null && (
                  <div className="flex items-center gap-1.5">
                    <Clock size={14} className="text-accent" />
                    <span className="font-semibold text-primary">{steamPersonal.hoursPlayed.toLocaleString()}</span> Hrs
                  </div>
                )}
                {steamPersonal.profileValue !== null && (
                  <div className="flex items-center gap-1.5">
                    <Trophy size={14} className="text-yellow-500" />
                    <span className="font-semibold text-primary">{steamPersonal.profileValue.toLocaleString()}€</span> Value
                  </div>
                )}
              </div>
            </div>
          </div>
        ) : (
          <div className="flex items-center justify-center min-h-[100px] text-muted text-sm">
            Link your Steam Profile in Settings
          </div>
        )}
      </WidgetCard>
      <WidgetCard id="steam-detail" title="Steam Stats" icon={Gamepad2} delay={100} className="relative">
        <div className="absolute top-6 right-6">
          <select 
            className="bg-surface-base border border-border-subtle rounded px-3 py-1.5 text-sm font-semibold text-primary focus:outline-none focus:border-accent transition-colors cursor-pointer"
            value={selectedApp}
            onChange={(e) => setSelectedApp(e.target.value)}
          >
            <option value="0">Global (All Steam Users)</option>
            <option value="730">Counter-Strike 2</option>
            <option value="570">Dota 2</option>
            <option value="359550">Rainbow Six Siege</option>
            <option value="386940">Ultimate Chicken Horse</option>
          </select>
        </div>
        {steamPlayers[selectedApp] ? (
          <div className="text-center py-6">
            <div className="text-6xl font-bold text-primary mb-2">{steamPlayers[selectedApp].player_count.toLocaleString()}</div>
            <div className="text-sm text-secondary">players online right now</div>
          </div>
        ) : (
          <div className="text-muted text-center py-8 text-sm">Data unavailable</div>
        )}
      </WidgetCard>
      <WidgetCard id="exophase-recent" title="Last Played Games" icon={Gamepad2} delay={200}>
        {exophaseGames.length > 0 ? (
          <div className="flex flex-col">
            {exophaseGames.map((game, i) => (
              <div 
                key={game.id}
                className="flex items-center gap-4 p-4 border-b border-border-subtle last:border-0 hover:bg-surface-inset transition-colors"
                style={{ animationDelay: `${i * 50}ms` }}
              >
                <div className="w-28 h-16 shrink-0 rounded-lg overflow-hidden bg-surface-inset shadow-sm">
                  <img src={game.coverUrl} alt={game.title} className="w-full h-full object-cover" />
                </div>
                <div className="flex-1 min-w-0">
                  <h4 className="text-sm font-bold text-primary truncate">{game.title}</h4>
                  <p className="text-xs text-accent mt-0.5">{game.platform}</p>
                  <div className="flex items-center gap-4 mt-2 flex-wrap">
                    <div className="flex items-center gap-1.5 text-xs text-secondary">
                      <Clock size={12} />
                      {game.playtimeStr}
                    </div>
                    {game.awardsPossible > 0 && (
                      <div className="flex items-center gap-1.5 text-xs text-secondary flex-1 max-w-[200px]">
                        <Trophy size={12} className={game.awardsEarned === game.awardsPossible ? "text-yellow-500" : ""} />
                        <div className="flex-1 h-1.5 bg-surface-inset rounded-full overflow-hidden">
                          <div 
                            className="h-full bg-accent rounded-full"
                            style={{ width: `${(game.awardsEarned / game.awardsPossible) * 100}%` }}
                          />
                        </div>
                        <span className="shrink-0">{game.awardsEarned}/{game.awardsPossible}</span>
                      </div>
                    )}
                    {game.lastPlayed > 0 && (
                      <div className="text-xs text-muted ml-auto">
                        Last played: {new Date(game.lastPlayed * 1000).toLocaleDateString()}
                      </div>
                    )}
                  </div>
                </div>
              </div>
            ))}
          </div>
        ) : (
          <div className="flex flex-col items-center justify-center min-h-[200px] text-muted py-8 text-sm">
            <p>Link your Exophase profile in Settings</p>
          </div>
        )}
      </WidgetCard>
      <WidgetCard id="trn" title="Stats Tracker" icon={Gamepad2} delay={150} theme="dark" className="flex-1 relative">
        {trn && Object.keys(trn).length > 0 ? (
          <>
            {Object.keys(trn).length > 1 && (
              <div className="absolute top-6 right-6">
                <select 
                  className="bg-surface-base border border-border-subtle rounded px-3 py-1.5 text-sm font-semibold text-primary focus:outline-none focus:border-accent transition-colors cursor-pointer"
                  value={selectedTrnGame}
                  onChange={(e) => setSelectedTrnGame(e.target.value)}
                >
                  {Object.values(trn).map((game: any) => (
                    <option key={game.gameId} value={game.gameId}>{game.gameName}</option>
                  ))}
                </select>
              </div>
            )}
            {trn[selectedTrnGame] && (
              <div className="p-4 flex flex-col justify-between gap-4 h-full mt-4">
                <div className="flex items-center gap-4 bg-surface-inset p-3 rounded-xl border border-border-subtle">
                  {trn[selectedTrnGame].rank.iconUrl && (
                    <img src={trn[selectedTrnGame].rank.iconUrl} alt={trn[selectedTrnGame].rank.name} className="w-12 h-12" />
                  )}
                  <div className="min-w-0 flex-1 flex items-center gap-3">
                    <div className="w-10 h-10 rounded-full bg-surface-base border border-border-subtle flex items-center justify-center shrink-0">
                      <User size={20} className="text-secondary" />
                    </div>
                    <div className="min-w-0 flex-1">
                      <h3 className="font-bold text-lg text-primary truncate">{trn[selectedTrnGame].username}</h3>
                      <p className="text-sm text-secondary font-medium truncate">{trn[selectedTrnGame].rank.name}{trn[selectedTrnGame].rank.mmr !== 0 ? ` • ${trn[selectedTrnGame].rank.mmr}` : ''}</p>
                    </div>
                  </div>
                </div>
                <div className="grid grid-cols-3 gap-3">
                  {trn[selectedTrnGame].primaryStats.map((stat: any, index: number) => (
                    <div key={index} className="bg-surface-inset p-3 rounded-xl border border-border-subtle text-center flex flex-col justify-center min-h-[70px]">
                      <p className="text-xs text-secondary mb-1">{stat.label}</p>
                      <p className={`font-bold ${index === 2 ? 'text-accent-warm' : 'text-primary'} text-lg sm:text-xl truncate`}>{stat.value}</p>
                    </div>
                  ))}
                </div>
              </div>
            )}
          </>
        ) : (
          <div className="flex flex-col items-center justify-center min-h-[200px] text-muted py-8 text-sm">
            <p>Link your Tracker.gg profiles in Settings</p>
          </div>
        )}
      </WidgetCard>
      </div>
      <div className="flex flex-col gap-6">
      {steamCharts && (
        <WidgetCard id="steam-charts" title="Top 10 Global Charts" icon={Trophy} delay={300}>
          <div className="flex gap-2 p-1 mb-4 bg-surface-inset rounded-lg overflow-x-auto no-scrollbar">
            <button 
              onClick={() => setActiveTab('online')}
              className={`flex-1 min-w-[120px] py-2 px-3 text-xs font-semibold rounded-md transition-all ${activeTab === 'online' ? 'bg-accent text-white shadow-md' : 'text-secondary hover:text-primary'}`}
            >
              Online Now
            </button>
            <button 
              onClick={() => setActiveTab('mostPlayed')}
              className={`flex-1 min-w-[120px] py-2 px-3 text-xs font-semibold rounded-md transition-all ${activeTab === 'mostPlayed' ? 'bg-accent text-white shadow-md' : 'text-secondary hover:text-primary'}`}
            >
              Most Played
            </button>
            <button 
              onClick={() => setActiveTab('wishlisted')}
              className={`flex-1 min-w-[120px] py-2 px-3 text-xs font-semibold rounded-md transition-all ${activeTab === 'wishlisted' ? 'bg-accent text-white shadow-md' : 'text-secondary hover:text-primary'}`}
            >
              Top Wishlisted
            </button>
          </div>
          <div className="flex flex-col relative min-h-[400px]">
            {steamCharts[activeTab]?.map((game: any, i: number) => (
              <div 
                key={`${activeTab}-${game.id}`}
                className="flex items-center gap-4 p-4 border-b border-border-subtle last:border-0 hover:bg-surface-inset transition-colors animate-slideUp"
                style={{ animationDelay: `${i * 30}ms` }}
              >
                <div className="w-8 flex-shrink-0 text-center font-bold text-accent text-lg">
                  #{game.rank}
                </div>
                <div className="w-28 h-16 shrink-0 rounded-lg overflow-hidden bg-surface-inset shadow-sm hidden sm:block">
                  <img src={game.coverUrl} alt={game.title} className="w-full h-full object-cover" />
                </div>
                <div className="flex-1 min-w-0">
                  <h4 className="text-sm font-bold text-primary truncate">{game.title}</h4>
                  <div className="flex items-center gap-2 mt-2">
                    <span className="text-xs font-semibold text-accent bg-accent/10 px-2 py-0.5 rounded-full">
                      {game.statValue}
                    </span>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </WidgetCard>
      )}
      </div>
    </div>
  );
}
