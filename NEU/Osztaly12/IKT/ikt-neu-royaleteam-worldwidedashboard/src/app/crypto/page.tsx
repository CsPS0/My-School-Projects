"use client";
import React, { useEffect, useState } from 'react';
import { WidgetCard } from '../components/WidgetCard';
import { Bitcoin } from 'lucide-react';
import CryptoChart from '../components/CryptoChart';
import HawkIcon from '../components/HawkIcon';
export default function CryptoPage() {
  const [finance, setFinance] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    fetch('/api/dashboard')
      .then((res) => res.json())
      .then((data) => {
        setFinance(data['finance_markets']);
        setLoading(false);
      })
      .catch(console.error);
  }, []);
  if (loading) {
    return (
      <div className="flex items-center justify-center min-h-[40vh]">
        <div className="w-12 h-12 border-4 border-transparent/20 border-t-accent rounded-full animate-spin"></div>
      </div>
    );
  }
  const cryptoCoins = finance?.crypto ? [
    { key: 'bitcoin', name: 'Bitcoin', symbol: 'BTC', icon: '₿', data: finance.crypto.bitcoin },
    { key: 'ethereum', name: 'Ethereum', symbol: 'ETH', icon: 'Ξ', data: finance.crypto.ethereum },
    { key: 'solana', name: 'Solana', symbol: 'SOL', icon: '◎', data: finance.crypto.solana },
    { key: 'binancecoin', name: 'Binance Coin', symbol: 'BNB', icon: 'BNB', data: finance.crypto.binancecoin },
    { key: 'dogecoin', name: 'Dogecoin', symbol: 'DOGE', icon: 'Ð', data: finance.crypto.dogecoin },
    { key: 'hawkTuah', name: 'Hawk Tuah', symbol: 'HAWK', icon: <HawkIcon width={32} height={32} />, data: finance.crypto.hawkTuah },
  ] : [];
  return (
    <div className="w-full space-y-6">
      <WidgetCard id="crypto-chart" title="Crypto Markets Overview" icon={Bitcoin} delay={100} className="w-full">
        <div className="p-4">
          <CryptoChart height={350} />
        </div>
      </WidgetCard>
      <WidgetCard id="crypto-grid" title="Live Prices" icon={Bitcoin} delay={200} className="w-full">
        {finance ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 p-6">
            {cryptoCoins.map((coin) => (
              <div key={coin.key} className="flex flex-col gap-3 bg-surface-inset border border-border-subtle p-6 rounded-2xl hover:bg-surface-card-hover transition-colors shadow-sm">
                <div className="flex items-center gap-4 mb-2">
                  <div className="w-14 h-14 rounded-full bg-accent-warm/15 flex items-center justify-center text-accent-warm text-2xl font-bold">{coin.icon}</div>
                  <div>
                    <h3 className="text-xl font-bold text-primary">{coin.name}</h3>
                    <span className="text-secondary font-medium">{coin.symbol}</span>
                  </div>
                </div>
                <div className="text-3xl font-bold text-primary">${coin.data?.usd?.toLocaleString(undefined, { minimumFractionDigits: coin.data.usd < 1 ? 4 : 2, maximumFractionDigits: coin.data.usd < 1 ? 4 : 2 })}</div>
                <div className="text-lg text-secondary">€{coin.data?.eur?.toLocaleString(undefined, { minimumFractionDigits: coin.data.eur < 1 ? 4 : 2, maximumFractionDigits: coin.data.eur < 1 ? 4 : 2 })}</div>
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
