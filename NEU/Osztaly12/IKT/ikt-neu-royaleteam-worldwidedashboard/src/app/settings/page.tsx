"use client";
import { useState, useEffect, useRef } from 'react';
import {
  Save,
  User as UserIcon,
  Lock,
  Gamepad2,
  MonitorPlay,
  Music,
  LayoutGrid,
  AlertCircle,
  CheckCircle2,
  HelpCircle,
  Link2,
  Camera,
  Bitcoin,
  ArrowLeftRight,
  Landmark,
  ChevronDown,
} from 'lucide-react';
export default function SettingsPage() {
  const [user, setUser] = useState<any>(null);
  const [saving, setSaving] = useState(false);
  const [message, setMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);
  const [originalPassword, setOriginalPassword] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [avatarUrl, setAvatarUrl] = useState<string | null>(null);
  const [avatarDirty, setAvatarDirty] = useState(false);
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [steamId, setSteamId] = useState('');
  const [steamApiKey, setSteamApiKey] = useState('');
  const [exophaseUsername, setExophaseUsername] = useState('');
  const [youtubeHandle, setYoutubeHandle] = useState('');
  const [favoriteArtist, setFavoriteArtist] = useState('');
  const [lastFmUsername, setLastFmUsername] = useState('');
  const [trackerUrlR6, setTrackerUrlR6] = useState('');
  const [trackerUrlRL, setTrackerUrlRL] = useState('');
  const [trackerUrlLoL, setTrackerUrlLoL] = useState('');
  const [trackerUrlBF6, setTrackerUrlBF6] = useState('');
  const [trackerUrlFortnite, setTrackerUrlFortnite] = useState('');
  const [dashboardStyle, setDashboardStyle] = useState('grid');
  const [hiddenWidgets, setHiddenWidgets] = useState<string[]>([]);
  const widgets = [
    { id: 'steam', label: 'Steam Player Count' },
    { id: 'youtube', label: 'YouTube Analytics' },
    { id: 'music', label: 'Global Top 10 Music' },
    { id: 'crypto', label: 'Crypto Markets' },
    { id: 'exchange', label: 'Exchange Rates' },
    { id: 'politics', label: 'Election 2026' },
  ];
  useEffect(() => {
    fetch('/api/auth/me')
      .then((res) => res.json())
      .then((data) => {
        if (data.user) {
          setUser(data.user);
          setAvatarUrl(data.user.avatarUrl || null);
          setSteamId(data.user.steamId || '');
          setSteamApiKey(data.user.steamApiKey || '');
          setExophaseUsername(data.user.exophaseUsername || '');
          setYoutubeHandle(data.user.youtubeHandle || '');
          setFavoriteArtist(data.user.favoriteArtist || '');
          setLastFmUsername(data.user.lastFmUsername || '');
          setTrackerUrlR6(data.user.trackerUrlR6 || '');
          setTrackerUrlRL(data.user.trackerUrlRL || '');
          setTrackerUrlLoL(data.user.trackerUrlLoL || '');
          setTrackerUrlBF6(data.user.trackerUrlBF6 || '');
          setTrackerUrlFortnite(data.user.trackerUrlFortnite || '');
        }
      })
      .catch(() => {});
    fetch('/api/auth/preferences')
      .then((res) => {
        if (res.ok) return res.json();
        throw new Error('Not logged in');
      })
      .then((data) => {
        setDashboardStyle(data.dashboardStyle || 'grid');
        try {
          setHiddenWidgets(JSON.parse(data.hiddenWidgets || '[]'));
        } catch {
          setHiddenWidgets([]);
        }
      })
      .catch(() => {});
  }, []);
  const handleSave = async () => {
    setMessage(null);
    if (password && password !== confirmPassword) {
      setMessage({ type: 'error', text: 'Passwords do not match.' });
      return;
    }
    setSaving(true);
    try {
      const promises = [];

      const accountPayload: Record<string, string> = {};
      if (password) {
        if (!originalPassword) {
          setMessage({ type: 'error', text: 'Please enter your original password.' });
          setSaving(false);
          return;
        }
        accountPayload.originalPassword = originalPassword;
        accountPayload.password = password;
      }
      if (avatarDirty) accountPayload.avatarUrl = avatarUrl || '';
      
      if (Object.keys(accountPayload).length > 0) {
        promises.push(
          fetch('/api/auth/me', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(accountPayload),
          }).then(async res => {
            if (res.ok) {
              setOriginalPassword('');
              setPassword('');
              setConfirmPassword('');
              setAvatarDirty(false);
            } else {
              const d = await res.json();
              throw new Error(d.error || 'Failed to update account.');
            }
          })
        );
      }

      promises.push(
        fetch('/api/auth/me', {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ 
            steamId, steamApiKey, exophaseUsername, youtubeHandle, 
            favoriteArtist, lastFmUsername, trackerUrlR6, trackerUrlRL, 
            trackerUrlLoL, trackerUrlBF6, trackerUrlFortnite
          }),
        }).then(res => { if (!res.ok) throw new Error('Failed to save integrations.'); })
      );

      promises.push(
        fetch('/api/auth/preferences', {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            dashboardStyle,
            hiddenWidgets: JSON.stringify(hiddenWidgets),
          }),
        }).then(res => { if (!res.ok) throw new Error('Failed to save preferences.'); })
      );

      await Promise.all(promises);
      setMessage({ type: 'success', text: 'All settings saved successfully.' });
    } catch (e: any) {
      setMessage({ type: 'error', text: e.message || 'An unexpected error occurred.' });
    } finally {
      setSaving(false);
      setTimeout(() => setMessage(null), 3000);
    }
  };
  const toggleWidget = (id: string) => {
    setHiddenWidgets((prev) =>
      prev.includes(id) ? prev.filter((w) => w !== id) : [...prev, id]
    );
  };
  if (!user) {
    return (
      <div className="flex items-center justify-center min-h-[40vh]">
        <div className="w-10 h-10 border-[3px] border-border-subtle border-t-accent rounded-full animate-spin" />
      </div>
    );
  }
  return (
    <div className="w-full max-w-7xl mx-auto">
      {message && (
        <div
          className={`flex items-center gap-2.5 p-3 rounded-lg mb-6 text-sm font-medium border ${
            message.type === 'success'
              ? 'bg-success/10 border-success/20 text-success'
              : 'bg-danger/10 border-danger/20 text-danger'
          }`}
        >
          {message.type === 'success' ? <CheckCircle2 size={16} /> : <AlertCircle size={16} />}
          {message.text}
        </div>
      )}
      
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        
        <div className="glass-card p-6 h-fit space-y-5">
          <h2 className="text-lg font-bold text-primary mb-4 flex items-center gap-2"><Lock size={18} className="text-accent" /> Account Settings</h2>
          <div className="flex items-center gap-5">
              <button
                type="button"
                onClick={() => fileInputRef.current?.click()}
                className="relative w-20 h-20 rounded-full shrink-0 bg-surface-inset border-2 border-dashed border-border-subtle hover:border-accent transition-colors flex items-center justify-center group overflow-hidden"
              >
                {avatarUrl ? (
                  <img src={avatarUrl} alt="Avatar" className="w-full h-full object-cover rounded-full" />
                ) : (
                  <UserIcon size={28} className="text-muted" />
                )}
                <div className="absolute inset-0 bg-black/50 opacity-0 group-hover:opacity-100 transition-opacity flex items-center justify-center rounded-full">
                  <Camera size={18} className="text-primary" />
                </div>
              </button>
              <input
                ref={fileInputRef}
                type="file"
                accept="image/png,image/jpeg,image/webp"
                className="hidden"
                onChange={(e) => {
                  const file = e.target.files?.[0];
                  if (!file) return;
                  if (file.size > 512 * 1024) {
                    setMessage({ type: 'error', text: 'Image must be under 512 KB.' });
                    return;
                  }
                  const reader = new FileReader();
                  reader.onload = () => {
                    setAvatarUrl(reader.result as string);
                    setAvatarDirty(true);
                  };
                  reader.readAsDataURL(file);
                }}
              />
              <div className="min-w-0">
                <div className="text-sm font-medium text-primary">{user.username}</div>
                <button
                  type="button"
                  onClick={() => fileInputRef.current?.click()}
                  className="text-xs text-accent hover:underline mt-0.5"
                >
                  {avatarUrl ? 'Change photo' : 'Upload photo'}
                </button>
                {avatarUrl && (
                  <button
                    type="button"
                    onClick={() => { setAvatarUrl(null); setAvatarDirty(true); }}
                    className="text-xs text-accent hover:underline mt-0.5 ml-3"
                  >
                    Remove
                  </button>
                )}
              </div>
            </div>
            <div className="space-y-4">
              <div className="space-y-1.5">
                <label className="text-xs font-medium text-secondary uppercase tracking-wider">Original Password</label>
                <input
                  type="password"
                  value={originalPassword}
                  onChange={(e) => setOriginalPassword(e.target.value)}
                  placeholder="Current password"
                  className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2.5 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors"
                />
              </div>
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                <div className="space-y-1.5">
                  <label className="text-xs font-medium text-secondary uppercase tracking-wider">New Password</label>
                  <input
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    placeholder="Enter new password"
                    className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2.5 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors"
                  />
                </div>
                <div className="space-y-1.5">
                  <label className="text-xs font-medium text-secondary uppercase tracking-wider">Confirm Password</label>
                  <input
                    type="password"
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                    placeholder="Confirm password"
                    className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2.5 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors"
                  />
                </div>
              </div>
            </div>
        </div>

        <div className="glass-card p-6 h-fit space-y-5">
          <h2 className="text-lg font-bold text-primary mb-1 flex items-center gap-2"><Link2 size={18} className="text-accent" /> Integrations</h2>
          <div className="space-y-3">
            <p className="text-sm text-secondary mb-4">Click a category below to configure its API keys and profile URLs.</p>
            
            <details className="group border border-border-subtle rounded-xl bg-surface-inset overflow-hidden [&_summary::-webkit-details-marker]:hidden">
              <summary className="cursor-pointer font-bold text-primary p-3 select-none flex items-center justify-between hover:bg-surface-card-hover transition-colors">
                <div className="flex items-center gap-2.5"><Gamepad2 size={16} className="text-accent" /> Gaming</div>
                <ChevronDown size={16} className="text-muted group-open:rotate-180 transition-transform" />
              </summary>
              <div className="p-4 space-y-5 border-t border-border-subtle bg-surface-base">
                <div className="space-y-1.5">
                  <label className="text-xs font-medium text-secondary uppercase tracking-wider flex items-center gap-1.5">
                    <Gamepad2 size={13} /> Steam Profile URL
                  </label>
                  <input type="url" value={steamId} onChange={(e) => setSteamId(e.target.value)} placeholder="https://steamcommunity.com/id/yourusername" className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors" />
                </div>
                <div className="space-y-1.5">
                  <label className="text-xs font-medium text-secondary uppercase tracking-wider flex items-center gap-1.5">
                    <Lock size={13} /> Steam Web API Key
                    <div className="group/tooltip relative flex items-center">
                      <HelpCircle size={13} className="text-muted hover:text-accent cursor-help transition-colors" />
                      <div className="absolute bottom-full left-1/2 -translate-x-1/2 mb-2 opacity-0 pointer-events-none group-hover/tooltip:opacity-100 group-hover/tooltip:pointer-events-auto transition-opacity duration-300 delay-[1000ms] group-hover/tooltip:delay-0 w-64 p-3 bg-surface-card border border-border-default rounded-lg shadow-xl text-xs text-primary z-50 text-center normal-case tracking-normal">
                        Steam requires a personal Web API key to securely fetch your exact game library and playtimes. You can get yours for free at{' '}
                        <a href="https://steamcommunity.com/dev/apikey" target="_blank" rel="noopener noreferrer" className="text-accent hover:underline font-bold">
                          steamcommunity.com/dev/apikey
                        </a>.
                        <span className="text-secondary mt-1.5 block">When asked for a Domain Name, you can simply enter <strong className="text-primary">localhost</strong>.</span>
                        <div className="absolute top-full left-0 w-full h-4 bg-transparent" />
                      </div>
                    </div>
                  </label>
                  <input type="password" value={steamApiKey} onChange={(e) => setSteamApiKey(e.target.value)} placeholder="Optional. Required for exact game count" className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors" />
                </div>
                <div className="space-y-1.5">
                  <label className="text-xs font-medium text-secondary uppercase tracking-wider flex items-center gap-1.5">
                    <Gamepad2 size={13} /> Exophase Profile URL
                  </label>
                  <input type="url" value={exophaseUsername} onChange={(e) => setExophaseUsername(e.target.value)} placeholder="https://www.exophase.com/user/yourusername/" className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors" />
                </div>
                
                <h3 className="text-primary font-bold tracking-wide mt-6 border-t border-border-subtle pt-4 text-sm">TRN TRACKER.GG URLS</h3>
                <div className="grid grid-cols-1 gap-4">
                  <div className="space-y-1.5">
                    <label className="text-xs font-medium text-secondary uppercase tracking-wider">R6 Siege</label>
                    <input type="url" value={trackerUrlR6} onChange={(e) => setTrackerUrlR6(e.target.value)} placeholder="https://tracker.gg/r6siege/profile/..." className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary outline-none focus:border-accent" />
                  </div>
                  <div className="space-y-1.5">
                    <label className="text-xs font-medium text-secondary uppercase tracking-wider">Rocket League</label>
                    <input type="url" value={trackerUrlRL} onChange={(e) => setTrackerUrlRL(e.target.value)} placeholder="https://rocketleague.tracker.network/..." className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary outline-none focus:border-accent" />
                  </div>
                  <div className="space-y-1.5">
                    <label className="text-xs font-medium text-secondary uppercase tracking-wider">League of Legends</label>
                    <input type="url" value={trackerUrlLoL} onChange={(e) => setTrackerUrlLoL(e.target.value)} placeholder="https://tracker.gg/lol/profile/..." className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary outline-none focus:border-accent" />
                  </div>
                  <div className="space-y-1.5">
                    <label className="text-xs font-medium text-secondary uppercase tracking-wider">Battlefield 6</label>
                    <input type="url" value={trackerUrlBF6} onChange={(e) => setTrackerUrlBF6(e.target.value)} placeholder="https://tracker.gg/bf2042/profile/..." className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary outline-none focus:border-accent" />
                  </div>
                  <div className="space-y-1.5">
                    <label className="text-xs font-medium text-secondary uppercase tracking-wider">Fortnite</label>
                    <input type="url" value={trackerUrlFortnite} onChange={(e) => setTrackerUrlFortnite(e.target.value)} placeholder="https://fortnitetracker.com/profile/..." className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary outline-none focus:border-accent" />
                  </div>
                </div>
              </div>
            </details>

            <details className="group border border-border-subtle rounded-xl bg-surface-inset overflow-hidden [&_summary::-webkit-details-marker]:hidden">
              <summary className="cursor-pointer font-bold text-primary p-3 select-none flex items-center justify-between hover:bg-surface-card-hover transition-colors">
                <div className="flex items-center gap-2.5"><MonitorPlay size={16} className="text-accent" /> Entertainment</div>
                <ChevronDown size={16} className="text-muted group-open:rotate-180 transition-transform" />
              </summary>
              <div className="p-4 space-y-4 border-t border-border-subtle bg-surface-base">
                <div className="space-y-1.5">
                  <label className="text-xs font-medium text-secondary uppercase tracking-wider">YouTube Channel URL</label>
                  <input type="url" value={youtubeHandle} onChange={(e) => setYoutubeHandle(e.target.value)} placeholder="https://www.youtube.com/@yourchannel" className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors" />
                </div>
              </div>
            </details>

            <details className="group border border-border-subtle rounded-xl bg-surface-inset overflow-hidden [&_summary::-webkit-details-marker]:hidden">
              <summary className="cursor-pointer font-bold text-primary p-3 select-none flex items-center justify-between hover:bg-surface-card-hover transition-colors">
                <div className="flex items-center gap-2.5"><Music size={16} className="text-accent" /> Music</div>
                <ChevronDown size={16} className="text-muted group-open:rotate-180 transition-transform" />
              </summary>
              <div className="p-4 space-y-4 border-t border-border-subtle bg-surface-base">
                <div className="space-y-1.5">
                  <label className="text-xs font-medium text-secondary uppercase tracking-wider">Last.fm Profile URL</label>
                  <input type="url" value={lastFmUsername} onChange={(e) => setLastFmUsername(e.target.value)} placeholder="https://www.last.fm/user/yourusername" className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors" />
                </div>
                <div className="space-y-1.5">
                  <label className="text-xs font-medium text-secondary uppercase tracking-wider">Favorite Artist URL (Last.fm)</label>
                  <input type="url" value={favoriteArtist} onChange={(e) => setFavoriteArtist(e.target.value)} placeholder="https://www.last.fm/music/Artist+Name" className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors" />
                </div>
              </div>
            </details>

            <details className="group border border-border-subtle rounded-xl bg-surface-inset overflow-hidden [&_summary::-webkit-details-marker]:hidden">
              <summary className="cursor-pointer font-bold text-primary p-3 select-none flex items-center justify-between hover:bg-surface-card-hover transition-colors">
                <div className="flex items-center gap-2.5"><Bitcoin size={16} className="text-accent" /> Crypto</div>
                <ChevronDown size={16} className="text-muted group-open:rotate-180 transition-transform" />
              </summary>
              <div className="p-4 border-t border-border-subtle bg-surface-base text-sm text-muted text-center italic py-6">
                No external configuration required for Crypto yet.
              </div>
            </details>

            <details className="group border border-border-subtle rounded-xl bg-surface-inset overflow-hidden [&_summary::-webkit-details-marker]:hidden">
              <summary className="cursor-pointer font-bold text-primary p-3 select-none flex items-center justify-between hover:bg-surface-card-hover transition-colors">
                <div className="flex items-center gap-2.5"><ArrowLeftRight size={16} className="text-accent" /> Exchange</div>
                <ChevronDown size={16} className="text-muted group-open:rotate-180 transition-transform" />
              </summary>
              <div className="p-4 border-t border-border-subtle bg-surface-base text-sm text-muted text-center italic py-6">
                No external configuration required for Exchange yet.
              </div>
            </details>

            <details className="group border border-border-subtle rounded-xl bg-surface-inset overflow-hidden [&_summary::-webkit-details-marker]:hidden">
              <summary className="cursor-pointer font-bold text-primary p-3 select-none flex items-center justify-between hover:bg-surface-card-hover transition-colors">
                <div className="flex items-center gap-2.5"><Landmark size={16} className="text-accent" /> Politics</div>
                <ChevronDown size={16} className="text-muted group-open:rotate-180 transition-transform" />
              </summary>
              <div className="p-4 border-t border-border-subtle bg-surface-base text-sm text-muted text-center italic py-6">
                No external configuration required for Politics yet.
              </div>
            </details>

          </div>
        </div>

        <div className="glass-card p-6 h-fit space-y-6">
          <h2 className="text-lg font-bold text-primary mb-4 flex items-center gap-2"><LayoutGrid size={18} className="text-accent" /> Dashboard Preferences</h2>
          <div className="space-y-6">
            <div className="mb-6">
              <label className="text-xs font-medium text-secondary uppercase tracking-wider mb-3 block">Dashboard Layout Options</label>
              <label className="flex items-center gap-3 bg-surface-inset px-3 py-2.5 rounded-lg cursor-pointer hover:bg-surface-card-hover transition-colors w-fit border border-border-subtle">
                <input
                  type="checkbox"
                  checked={dashboardStyle === 'compact'}
                  onChange={(e) => setDashboardStyle(e.target.checked ? 'compact' : 'grid')}
                  className="w-4 h-4 accent-accent rounded cursor-pointer"
                />
                <span className={`text-sm font-semibold ${dashboardStyle === 'compact' ? 'text-primary' : 'text-secondary'}`}>Enable Compact Cards</span>
              </label>
              <p className="text-xs text-muted mt-2">When disabled, cards stretch to fill the entire screen width.</p>
            </div>
            <div>
              <label className="text-xs font-medium text-secondary uppercase tracking-wider mb-3 block">Visible Highlight Cards (Overview)</label>
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-2">
                {widgets.map((w) => {
                  const visible = !hiddenWidgets.includes(w.id);
                  return (
                    <label
                      key={w.id}
                      className="flex items-center gap-3 bg-surface-inset px-3 py-2.5 rounded-lg cursor-pointer hover:bg-surface-card-hover transition-colors"
                    >
                      <input
                        type="checkbox"
                        checked={visible}
                        onChange={() => toggleWidget(w.id)}
                        className="w-4 h-4 accent-accent rounded"
                      />
                      <span className={`text-sm ${visible ? 'text-primary' : 'text-secondary'}`}>{w.label}</span>
                    </label>
                  );
                })}
              </div>
            </div>
          </div>
        </div>
      </div>
      
      <div className="flex justify-end mt-6 sticky bottom-4 z-10">
        <button
          onClick={handleSave}
          disabled={saving}
          className="flex items-center gap-2 bg-accent hover:bg-accent/90 text-surface-base text-sm font-semibold py-2.5 px-5 rounded-lg transition-colors disabled:opacity-50"
        >
          {saving ? (
            <div className="w-4 h-4 border-2 border-surface-base/20 border-t-surface-base rounded-full animate-spin" />
          ) : (
            <Save size={15} />
          )}
          Save
        </button>
      </div>
    </div>
  );
}
