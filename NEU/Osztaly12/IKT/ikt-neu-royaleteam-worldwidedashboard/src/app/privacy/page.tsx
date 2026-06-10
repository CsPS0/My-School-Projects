import React from 'react';
import Link from 'next/link';
import { ArrowLeft, Shield, Database, Webhook, Settings2 } from 'lucide-react';

export default function PrivacyPage() {
  return (
    <div className="min-h-screen p-4 sm:p-8 lg:p-12 max-w-5xl mx-auto space-y-10">
      <div className="flex justify-between items-end mb-8">
        <div>
          <div className="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-accent/10 border border-accent/20 text-accent text-xs font-bold uppercase tracking-wider mb-4">
            <Shield size={14} /> Data & Privacy
          </div>
          <h1 className="text-4xl font-bold text-primary flex items-center gap-3">
            Privacy & Data Usage Policy
          </h1>
          <p className="text-secondary mt-3 max-w-2xl text-lg">
            A complete breakdown of how this dashboard handles your data, which APIs it connects to, and which statistics are purely simulated for demonstration purposes.
          </p>
        </div>
        <Link 
          href="/overview" 
          className="hidden sm:flex items-center gap-2 text-sm font-semibold text-secondary hover:text-primary transition-colors bg-surface-inset border border-border-default hover:border-border-subtle rounded-lg px-4 py-2"
        >
          <ArrowLeft size={16} /> Back
        </Link>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
        
        <div className="bg-surface-card border border-border-default rounded-2xl p-6 sm:p-8 shadow-xl">
          <div className="w-12 h-12 rounded-xl bg-accent/10 text-accent flex items-center justify-center mb-6">
            <Webhook size={24} />
          </div>
          <h2 className="text-2xl font-bold text-primary mb-4">Live API Integrations</h2>
          <p className="text-secondary mb-6 leading-relaxed">
            These sections connect directly to live external APIs to fetch real-time statistics based on the identifiers you provide.
          </p>
          <ul className="space-y-4 text-sm text-secondary">
            <li className="flex gap-3">
              <span className="text-primary font-bold min-w-[100px]">Steam</span>
              <span>Uses the official Steam Web API to fetch player counts, game details, and your personal gaming statistics.</span>
            </li>
            <li className="flex gap-3">
              <span className="text-primary font-bold min-w-[100px]">YouTube</span>
              <span>Uses the YouTube Data API v3 to fetch channel subscriber counts, video views, and real-time social metrics.</span>
            </li>
            <li className="flex gap-3">
              <span className="text-primary font-bold min-w-[100px]">Last.fm</span>
              <span>Uses the Last.fm API to fetch live music scrobbles, top artists, and recent listening history.</span>
            </li>
            <li className="flex gap-3">
              <span className="text-primary font-bold min-w-[100px]">Crypto</span>
              <span>Uses the CoinGecko API to fetch live cryptocurrency market data, prices, and exchange rates.</span>
            </li>
            <li className="flex gap-3">
              <span className="text-primary font-bold min-w-[100px]">Exophase</span>
              <span>Scrapes public Exophase gaming profiles to aggregate cross-platform gaming achievements.</span>
            </li>
          </ul>
        </div>

        <div className="bg-surface-card border border-border-default rounded-2xl p-6 sm:p-8 shadow-xl relative overflow-hidden">
          <div className="absolute top-0 right-0 p-6 opacity-5 pointer-events-none">
            <Database size={160} />
          </div>
          <div className="relative z-10">
            <div className="w-12 h-12 rounded-xl bg-accent-warm/10 text-accent-warm flex items-center justify-center mb-6">
              <Database size={24} />
            </div>
            <h2 className="text-2xl font-bold text-primary mb-4">Simulated Data</h2>
            <p className="text-secondary mb-6 leading-relaxed">
              Certain sections of this dashboard use strictly simulated or hardcoded mock data for demonstration purposes, ensuring no real personal financial or political polling data is handled.
            </p>
            <ul className="space-y-4 text-sm text-secondary">
              <li className="flex gap-3">
                <span className="text-primary font-bold min-w-[100px]">Politics</span>
                <span>The Global Parliaments, World Leaders Directory, and Hungarian Elections data (e.g., Tisza Kormány) are fictionalized 2026 scenarios built for this school project.</span>
              </li>
              <li className="flex gap-3">
                <span className="text-primary font-bold min-w-[100px]">Finance</span>
                <span>The traditional stock market tracking and banking metrics are mocked using randomized procedural data sets.</span>
              </li>
            </ul>
          </div>
        </div>

      </div>

      <div className="bg-surface-card border border-border-default rounded-2xl p-6 sm:p-8 shadow-xl mt-8">
        <div className="w-12 h-12 rounded-xl bg-accent/10 text-accent flex items-center justify-center mb-6">
          <Settings2 size={24} />
        </div>
        <h2 className="text-2xl font-bold text-primary mb-4">Required Settings</h2>
        <p className="text-secondary mb-6 leading-relaxed">
          To enable the live integrations for your personal account, you must navigate to the <Link href="/settings" className="text-accent hover:underline">Settings</Link> page and provide the following public identifiers. We do NOT ask for passwords or private authentication tokens.
        </p>
        
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
          <div className="bg-surface-inset border border-border-subtle p-4 rounded-xl">
            <h3 className="font-bold text-primary mb-1">Steam</h3>
            <p className="text-xs text-secondary">Your 17-digit Steam64 ID (e.g., 765611980...)</p>
          </div>
          <div className="bg-surface-inset border border-border-subtle p-4 rounded-xl">
            <h3 className="font-bold text-primary mb-1">YouTube</h3>
            <p className="text-xs text-secondary">Your channel handle or ID (e.g., @royaleteam)</p>
          </div>
          <div className="bg-surface-inset border border-border-subtle p-4 rounded-xl">
            <h3 className="font-bold text-primary mb-1">Last.fm</h3>
            <p className="text-xs text-secondary">Your Last.fm username (e.g., royalmusic)</p>
          </div>
          <div className="bg-surface-inset border border-border-subtle p-4 rounded-xl">
            <h3 className="font-bold text-primary mb-1">Exophase</h3>
            <p className="text-xs text-secondary">Your full Exophase profile URL</p>
          </div>
        </div>
      </div>

    </div>
  );
}
