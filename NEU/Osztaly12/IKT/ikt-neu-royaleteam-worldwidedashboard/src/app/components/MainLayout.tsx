"use client";
import React, { useEffect, useState } from 'react';
import {
  Gamepad2,
  MonitorPlay,
  TrendingUp,
  Bitcoin,
  Landmark,
  Music,
  LogOut,
  User as UserIcon,
  LayoutDashboard,
  Settings,
  Menu,
  X,
  ChevronLeft,
  Lock,
  BarChart2,
  FileText,
  Shield,
  Sun,
  Moon,
} from 'lucide-react';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
const NAV_ITEMS = [
  { id: 'home', icon: LayoutDashboard, label: 'Overview', path: '/overview' },
  { id: 'steam', icon: Gamepad2, label: 'Gaming', path: '/gaming' },
  { id: 'youtube', icon: MonitorPlay, label: 'Entertainment', path: '/entertainment' },
  { id: 'music', icon: Music, label: 'Music', path: '/music' },
  { id: 'crypto', icon: Bitcoin, label: 'Crypto', path: '/crypto' },
  { id: 'exchange', icon: TrendingUp, label: 'Exchange', path: '/exchange' },
  { id: 'politics', icon: Landmark, label: 'Politics', path: '/politics' },
];
export function MainLayout({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<{ id: string; username: string; avatarUrl?: string | null } | null>(null);
  const [mobileOpen, setMobileOpen] = useState(false);
  const [statusOpen, setStatusOpen] = useState(false);
  const [collapsed, setCollapsed] = useState(false);
  const [apiStatuses, setApiStatuses] = useState<any[]>([]);
  const [showLockWarning, setShowLockWarning] = useState(false);
  const [theme, setTheme] = useState<'dark' | 'light'>('dark');
  const pathname = usePathname();

  useEffect(() => {
    const isLight = document.documentElement.classList.contains('light');
    setTheme(isLight ? 'light' : 'dark');
  }, []);

  const toggleTheme = () => {
    const newTheme = theme === 'dark' ? 'light' : 'dark';
    setTheme(newTheme);
    if (newTheme === 'light') {
      document.documentElement.classList.add('light');
      localStorage.theme = 'light';
    } else {
      document.documentElement.classList.remove('light');
      localStorage.theme = 'dark';
    }
  };

  useEffect(() => {
    fetch('/api/dashboard')
      .then(res => res.json())
      .then(data => {
        if (data.system_status) {
          const statuses: any[] = Object.values(data.system_status);
          statuses.push({ id: 'crypto', name: 'Crypto (CoinGecko)', isMock: false, category: 'Finance' });
          statuses.push({ id: 'hawk-tuah', name: 'Hawk Tuah Coin', isMock: true, category: 'Finance' });
          setApiStatuses(statuses);
        }
      })
      .catch(() => {});
  }, []);
  useEffect(() => {
    fetch('/api/auth/me')
      .then((res) => res.json())
      .then((data) => {
        if (data.user) setUser(data.user);
      })
      .catch(() => {});
  }, []);
  const closeMobile = () => setMobileOpen(false);
  const handleLogout = async () => {
    await fetch('/api/auth/logout', { method: 'POST' });
    window.location.href = '/login';
  };
  const isActive = (path: string) => {
    if (path === '/') return pathname === '/';
    return pathname.startsWith(path);
  };
  const sidebarContent = (
    <>
      <div className="flex items-center justify-between px-4 pt-5 pb-6">
        <Link href={user ? '/overview' : '/'} className="flex items-center gap-2.5 min-w-0">
          {!collapsed && (
            <div className="w-8 h-8 bg-gradient-to-br from-accent to-accent-warm rounded-lg flex items-center justify-center transform rotate-3 shrink-0">
              <BarChart2 size={18} className="text-surface-base transform -rotate-3" />
            </div>
          )}
          {!collapsed && (
            <span className="text-sm font-bold text-primary tracking-wide truncate">WorldWideDB</span>
          )}
        </Link>
        <button
          onClick={() => setCollapsed(!collapsed)}
          className="hidden xl:flex items-center justify-center w-7 h-7 rounded-full bg-surface-inset border border-border-subtle shadow-sm text-secondary hover:text-accent hover:border-accent/30 hover:bg-accent/10 transition-all duration-300 group"
          aria-label={collapsed ? "Expand sidebar" : "Collapse sidebar"}
        >
          <ChevronLeft size={14} className={`transition-transform duration-300 ease-[cubic-bezier(0.34,1.56,0.64,1)] group-hover:-translate-x-0.5 ${collapsed ? 'rotate-180 group-hover:translate-x-0.5' : ''}`} />
        </button>
      </div>
      <nav className="flex-1 px-3 space-y-0.5">
        {NAV_ITEMS.map((item) => {
          const active = isActive(item.path);
          const isLocked = !user && item.id !== 'home';
          if (isLocked) {
            return (
              <button
                key={item.id}
                onClick={(e) => { e.preventDefault(); setShowLockWarning(true); }}
                className={`w-full flex items-center justify-between px-3 py-2.5 rounded-lg text-[13px] font-medium transition-colors text-muted hover:text-secondary hover:bg-surface-inset`}
                title={collapsed ? item.label : undefined}
              >
                <div className="flex items-center gap-3">
                  <item.icon size={18} className="shrink-0 opacity-50" />
                  {!collapsed && <span className="opacity-70">{item.label}</span>}
                </div>
                {!collapsed && <Lock size={14} className="opacity-50 text-secondary" />}
              </button>
            );
          }
          return (
            <Link
              key={item.id}
              href={item.path}
              onClick={closeMobile}
              className={`flex items-center gap-3 px-3 py-2.5 rounded-lg text-[13px] font-medium transition-colors ${
                active
                  ? 'bg-accent/10 text-accent'
                  : 'text-secondary hover:text-primary'
              }`}
              title={collapsed ? item.label : undefined}
            >
              <item.icon size={18} className="shrink-0" />
              {!collapsed && <span>{item.label}</span>}
            </Link>
          );
        })}
      </nav>
      <div className="mt-auto px-3 pb-4 space-y-0.5">
        {user ? (
          <>
            <Link
              href="/settings"
              onClick={closeMobile}
              className={`flex items-center gap-3 px-3 py-2.5 rounded-lg text-[13px] font-medium transition-colors ${
                isActive('/settings')
                  ? 'bg-accent/10 text-accent'
                  : 'text-secondary hover:text-primary'
              }`}
              title={collapsed ? 'Settings' : undefined}
            >
              <Settings size={18} className="shrink-0" />
              {!collapsed && <span>Settings</span>}
            </Link>
            <div className={`flex items-center gap-3 px-3 py-2.5 ${collapsed ? 'justify-center' : ''}`}>
              <div className="w-7 h-7 rounded-full bg-accent/15 flex items-center justify-center shrink-0 overflow-hidden">
                {user.avatarUrl ? (
                  <img src={user.avatarUrl} alt="" className="w-full h-full object-cover" />
                ) : (
                  <UserIcon size={14} className="text-accent" />
                )}
              </div>
              {!collapsed && (
                <span className="text-[13px] font-medium text-primary truncate flex-1">
                  {user.username}
                </span>
              )}
              {!collapsed && (
                <button
                  onClick={handleLogout}
                  className="text-muted hover:text-danger transition-colors"
                  title="Logout"
                >
                  <LogOut size={15} />
                </button>
              )}
            </div>
            {collapsed && (
              <button
                onClick={handleLogout}
                className="flex items-center justify-center w-full px-3 py-2 text-muted hover:text-danger transition-colors"
                title="Logout"
              >
                <LogOut size={16} />
              </button>
            )}
          </>
        ) : (
          <div className={`flex ${collapsed ? 'flex-col' : ''} gap-2 px-1`}>
            <Link
              href="/login"
              className={`flex items-center justify-center gap-2 bg-accent text-surface-base font-semibold rounded-lg transition-colors hover:bg-accent/90 ${
                collapsed ? 'w-full p-2' : 'flex-1 py-2 text-[13px]'
              }`}
              title={collapsed ? 'Login' : undefined}
            >
              {collapsed ? <UserIcon size={16} /> : 'Login'}
            </Link>
            {!collapsed && (
              <Link
                href="/signup"
                className="flex-1 flex items-center justify-center py-2 text-[13px] font-semibold text-primary border border-border-default rounded-lg hover:bg-surface-inset transition-colors"
              >
                Sign Up
              </Link>
            )}
          </div>
        )}
      </div>
    </>
  );
  return (
    <div className="flex h-full min-h-screen bg-surface-base">
      {mobileOpen && (
        <div
          className="fixed inset-0 bg-black/60 backdrop-blur-sm z-40 xl:hidden"
          onClick={(e) => {
            e.preventDefault();
            e.stopPropagation();
            setMobileOpen(false);
          }}
        />
      )}
      <aside
        className={`fixed xl:sticky top-0 left-0 h-screen z-50 flex flex-col bg-surface-sidebar border-r border-border-subtle transition-all duration-200 ${
          mobileOpen ? 'translate-x-0' : '-translate-x-full xl:translate-x-0'
        } ${collapsed ? 'w-[68px]' : 'w-60'}`}
      >
        <button
          onClick={() => setMobileOpen(false)}
          className="absolute top-4 right-4 xl:hidden text-muted hover:text-primary"
        >
          <X size={20} />
        </button>
        {sidebarContent}
      </aside>
      <div className="flex-1 min-w-0 flex flex-col bg-surface-base">
        <header className="sticky top-0 z-30 flex items-center gap-4 px-4 sm:px-6 lg:px-8 h-16 bg-surface-base border-b border-border-subtle">
          <button
            type="button"
            onClick={() => setMobileOpen(true)}
            className="xl:hidden flex items-center justify-center w-12 h-12 -ml-3 text-secondary hover:text-primary active:bg-surface-inset active:scale-95 rounded-full transition-all"
            style={{ zIndex: 9999, position: 'relative', WebkitTapHighlightColor: 'transparent' }}
            aria-label="Open Menu"
          >
            <Menu size={28} className="pointer-events-none" />
          </button>
          <div className="flex items-center gap-2.5">
            <h1 className="text-2xl font-bold text-primary">
              {NAV_ITEMS.find((i) => isActive(i.path))?.label ||
                (pathname === '/settings' ? 'Settings' : 'Dashboard')}
            </h1>
          </div>
          <div className="ml-auto relative flex items-center gap-3">
            <Link 
              href="/privacy"
              className="hidden sm:flex items-center gap-2 text-sm text-secondary hover:text-primary transition-colors cursor-pointer outline-none bg-surface-inset px-3 py-1.5 rounded-full border border-border-subtle hover:border-border-default"
            >
              <Shield size={14} className="text-muted" />
              Privacy
            </Link>
            <Link 
              href="/license"
              className="hidden sm:flex items-center gap-2 text-sm text-secondary hover:text-primary transition-colors cursor-pointer outline-none bg-surface-inset px-3 py-1.5 rounded-full border border-border-subtle hover:border-border-default"
            >
              <FileText size={14} className="text-muted" />
              License
            </Link>
            <button
              onClick={toggleTheme}
              className="flex items-center justify-center w-8 h-8 rounded-full bg-surface-inset border border-border-subtle text-secondary hover:text-accent hover:border-accent/50 transition-colors"
              title={`Switch to ${theme === 'dark' ? 'Light' : 'Dark'} Mode`}
            >
              {theme === 'dark' ? <Sun size={15} /> : <Moon size={15} />}
            </button>
            <button 
              onClick={() => setStatusOpen(!statusOpen)}
              className="flex items-center gap-2 text-sm text-secondary hover:text-primary transition-colors cursor-pointer outline-none bg-surface-inset px-3 py-1.5 rounded-full border border-border-subtle hover:border-accent/50"
              style={{ pointerEvents: 'auto' }}
            >
              <span className="w-2 h-2 rounded-full bg-accent status-dot" />
              Live
            </button>
            {statusOpen && (
              <div className="absolute right-0 top-full mt-3 w-72 glass-card bg-surface-card border border-border-subtle rounded-xl shadow-2xl p-5 z-50 animate-slideUp">
                <div className="flex justify-between items-center mb-4">
                  <h4 className="text-sm font-bold text-primary">System Status</h4>
                  <button onClick={() => setStatusOpen(false)} className="text-secondary hover:text-primary transition-colors"><X size={16}/></button>
                </div>
                <div className="flex flex-col gap-3 text-xs font-medium">
                  {apiStatuses.filter(s => !s.isMock).map(s => (
                    <div key={s.id} className="flex justify-between items-center">
                      <span className="text-secondary flex items-center gap-2">
                        {s.category === 'Games' || s.category === 'Gaming' ? <Gamepad2 size={14}/> :
                         s.category === 'Entertainment' ? <MonitorPlay size={14}/> :
                         s.category === 'Music' ? <Music size={14}/> :
                         s.category === 'Finance' || s.category === 'Exchange' || s.id === 'crypto' ? <Bitcoin size={14}/> :
                         <Landmark size={14}/>}
                        {s.name}
                      </span>
                      <span className="text-accent font-bold">Live</span>
                    </div>
                  ))}
                  {apiStatuses.filter(s => s.isMock).length > 0 && apiStatuses.filter(s => !s.isMock).length > 0 && (
                    <div className="h-px w-full bg-[#292929] my-1"></div>
                  )}
                  {apiStatuses.filter(s => s.isMock).map(s => (
                    <div key={s.id} className="flex justify-between items-center">
                      <span className="text-secondary flex items-center gap-2">
                        {s.category === 'Games' || s.category === 'Gaming' ? <Gamepad2 size={14}/> :
                         s.category === 'Entertainment' ? <MonitorPlay size={14}/> :
                         s.category === 'Music' ? <Music size={14}/> :
                         s.category === 'Finance' || s.category === 'Exchange' || s.id === 'hawk-tuah' ? <Bitcoin size={14}/> :
                         <Landmark size={14}/>}
                        {s.name}
                      </span>
                      <span className="text-yellow-500 font-bold">Mock Data</span>
                    </div>
                  ))}
                </div>
              </div>
            )}
          </div>
        </header>
        <main className="flex-1 p-4 sm:p-6 lg:p-8">
          {children}
        </main>
      </div>
      {showLockWarning && (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-background/80 backdrop-blur-sm">
          <div className="bg-surface-base border border-border-default rounded-xl shadow-2xl p-6 max-w-sm w-full text-center">
            <div className="w-12 h-12 rounded-full bg-accent/10 text-accent flex items-center justify-center mx-auto mb-4">
              <Lock size={24} />
            </div>
            <h3 className="text-xl font-bold text-primary mb-2">Login Required</h3>
            <p className="text-secondary mb-6 text-sm">
              You need to be logged in to access this section and view personalized statistics.
            </p>
            <div className="flex gap-3">
              <button 
                onClick={() => setShowLockWarning(false)}
                className="flex-1 px-4 py-2 rounded-lg font-bold text-sm bg-surface-inset text-primary hover:bg-border-subtle transition-colors"
              >
                Close
              </button>
              <Link 
                href="/login"
                onClick={() => setShowLockWarning(false)}
                className="flex-1 px-4 py-2 rounded-lg font-bold text-sm bg-accent text-surface-base hover:bg-accent-warm transition-colors"
              >
                Log In
              </Link>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
