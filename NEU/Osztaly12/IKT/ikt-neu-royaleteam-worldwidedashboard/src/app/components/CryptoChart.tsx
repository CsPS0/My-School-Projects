"use client";
import React, { useState, useEffect } from 'react';
import { ResponsiveContainer, LineChart, Line, XAxis, YAxis, Tooltip, CartesianGrid } from 'recharts';
import { TrendingUp, TrendingDown } from 'lucide-react';
const TIMEFRAMES = [
  { label: '1D', fullLabel: '1 Day', days: 1 },
  { label: '1W', fullLabel: '1 Week', days: 7 },
  { label: '1M', fullLabel: '1 Month', days: 30 },
  { label: '6M', fullLabel: '6 Months', days: 180 },
  { label: '1Y', fullLabel: '1 Year', days: 365 },
  { label: '5Y', fullLabel: '5 Years', days: 1825 },
  { label: 'Since Release', fullLabel: 'Since Release', days: 7300 },
];
const COIN_RELEASE_DATES: Record<string, string> = {
  'bitcoin': '2009-01-03',
  'ethereum': '2015-07-30',
  'solana': '2020-03-16',
  'binancecoin': '2017-06-26',
  'dogecoin': '2013-12-06',
  'hawk-tuah': '2024-12-04'
};
const COINS = [
  { id: 'bitcoin', name: 'Bitcoin', symbol: 'BTC' },
  { id: 'ethereum', name: 'Ethereum', symbol: 'ETH' },
  { id: 'solana', name: 'Solana', symbol: 'SOL' },
  { id: 'binancecoin', name: 'BNB', symbol: 'BNB' },
  { id: 'dogecoin', name: 'Dogecoin', symbol: 'DOGE' },
  { id: 'hawk-tuah', name: 'Hawk Tuah', symbol: 'HAWKTUAH' }
];
export default function CryptoChart({ height = 300 }: { height?: number }) {
  const [selectedCoin, setSelectedCoin] = useState(COINS[0].id);
  const [timeIndex, setTimeIndex] = useState(1); 
  const [chartData, setChartData] = useState<{ date: string; price: number }[]>([]);
  const [loading, setLoading] = useState(true);
  const [priceChange, setPriceChange] = useState<{ amount: number, percent: number } | null>(null);
  useEffect(() => {
    let isMounted = true;
    const fetchHistory = async () => {
      setLoading(true);
      const timeframe = TIMEFRAMES[timeIndex];
      try {
        const res = await fetch(`https://api.coingecko.com/api/v3/coins/${selectedCoin}/market_chart?vs_currency=usd&days=${timeframe.days}`);
        if (!res.ok) throw new Error(`CoinGecko returned ${res.status}`);
        const data = await res.json();
        if (data.prices && data.prices.length > 0) {
          const formattedData = data.prices.map((p: [number, number]) => {
            const d = new Date(p[0]);
            let dateStr = '';
            if (timeframe.days === 1) {
              dateStr = d.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
            } else if (timeframe.days <= 30) {
              dateStr = d.toISOString().substring(5, 16).replace('T', ' ');
            } else {
              dateStr = d.toISOString().split('T')[0];
            }
            return {
              date: dateStr,
              price: p[1]
            };
          });
          if (!isMounted) return;
          setChartData(formattedData);
          if (formattedData.length > 1) {
            const first = formattedData[0].price;
            const last = formattedData[formattedData.length - 1].price;
            setPriceChange({
              amount: last - first,
              percent: ((last - first) / first) * 100
            });
          }
        }
      } catch (error) {
        console.warn('CoinGecko fetch failed, using fallback mock data:', error);
        if (!isMounted) return;
        let currentPrice = selectedCoin === 'bitcoin' ? 65000 : 
                           selectedCoin === 'ethereum' ? 3500 : 
                           selectedCoin === 'solana' ? 150 : 
                           selectedCoin === 'binancecoin' ? 600 :
                           selectedCoin === 'dogecoin' ? 0.16 : 0.05;
        const mockData = [];
        const dataPoints = 300;
        const totalMs = timeframe.days * 24 * 60 * 60 * 1000;
        for (let i = dataPoints; i >= 0; i--) {
          const d = new Date(Date.now() - (i / dataPoints) * totalMs);
          if (COIN_RELEASE_DATES[selectedCoin] && d.getTime() < new Date(COIN_RELEASE_DATES[selectedCoin]).getTime()) {
            continue;
          }
          const volatility = currentPrice * 0.005; 
          const price = Math.max(0.0001, currentPrice + (Math.random() - 0.5) * volatility);
          currentPrice = price; 
          let dateStr = '';
          if (timeframe.days === 1) {
            dateStr = d.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
          } else if (timeframe.days <= 30) {
            dateStr = d.toISOString().substring(5, 16).replace('T', ' ');
          } else {
            dateStr = d.toISOString().split('T')[0];
          }
          mockData.push({ date: dateStr, price });
        }
        setChartData(mockData);
        const first = mockData[0].price;
        const last = mockData[mockData.length - 1].price;
        setPriceChange({
          amount: last - first,
          percent: ((last - first) / first) * 100
        });
      } finally {
        if (isMounted) setLoading(false);
      }
    };
    fetchHistory();
    return () => { isMounted = false; };
  }, [selectedCoin, timeIndex]);
  const currentPrice = chartData.length > 0 ? chartData[chartData.length - 1].price : 0;
  return (
    <div className="w-full flex flex-col gap-6">
      <div className="flex flex-col sm:flex-row items-center justify-between gap-4">
        <div className="flex items-center gap-3 bg-surface-inset p-2 rounded-xl border border-border-subtle">
          <select 
            value={selectedCoin} 
            onChange={(e) => setSelectedCoin(e.target.value)}
            className="bg-transparent text-primary font-bold outline-none cursor-pointer p-2"
          >
            {COINS.map(c => <option key={c.id} value={c.id} className="bg-surface-inset text-primary">{c.name} ({c.symbol})</option>)}
          </select>
        </div>
        {chartData.length > 0 && (
          <div className="text-right">
            <div className="text-3xl font-bold text-primary">
              ${currentPrice.toLocaleString(undefined, { minimumFractionDigits: currentPrice < 1 ? 4 : 2, maximumFractionDigits: currentPrice < 1 ? 4 : 2 })}
            </div>
            {priceChange && (
              <div className={`text-sm font-medium flex items-center justify-end gap-1 text-accent`}>
                {priceChange.amount >= 0 ? <TrendingUp size={16} /> : <TrendingDown size={16} />}
                ${Math.abs(priceChange.amount).toLocaleString(undefined, { maximumFractionDigits: currentPrice < 1 ? 4 : 2 })} ({priceChange.percent.toFixed(2)}%)
              </div>
            )}
          </div>
        )}
      </div>
      {loading ? (
        <div className={`w-full flex items-center justify-center`} style={{ height }}>
          <div className="w-8 h-8 border-4 border-transparent/20 border-t-accent rounded-full animate-spin"></div>
        </div>
      ) : chartData.length > 0 ? (
        <div className="w-full" style={{ height }}>
          <ResponsiveContainer width="100%" height={height}>
            <LineChart data={chartData} margin={{ top: 5, right: 5, left: 5, bottom: 5 }}>
              <defs>
                <linearGradient id="cryptoGradient" x1="0%" y1="0" x2="100%" y2="0">
                  <stop offset="0%" stopColor="var(--accent)" />
                  <stop offset="100%" stopColor="var(--accent)" />
                </linearGradient>
              </defs>
              <CartesianGrid strokeDasharray="3 3" stroke="var(--border-subtle)" vertical={false} />
              <XAxis 
                dataKey="date" 
                stroke="var(--text-muted)" 
                fontSize={12} 
                tickMargin={10}
                tickFormatter={(val) => {
                  if (TIMEFRAMES[timeIndex].days === 1) return val;
                  if (TIMEFRAMES[timeIndex].days > 30) return val.substring(5, 7) + '/' + val.substring(2, 4); 
                  return val.substring(5); 
                }}
              />
              <YAxis 
                domain={['auto', 'auto']} 
                stroke="var(--text-muted)" 
                fontSize={12} 
                tickFormatter={(val) => val < 1 ? val.toFixed(4) : val.toLocaleString()}
                width={65}
              />
              <Tooltip 
                contentStyle={{ backgroundColor: 'var(--surface-card)', borderColor: 'var(--accent)', borderRadius: '8px', color: 'var(--text-primary)' }}
                itemStyle={{ color: 'var(--accent-warm)', fontWeight: 'bold' }}
                labelStyle={{ color: 'var(--text-secondary)', marginBottom: '4px' }}
                formatter={(value: any) => [`$${Number(value).toLocaleString(undefined, { maximumFractionDigits: Number(value) < 1 ? 4 : 2 })}`, 'Price']}
              />
              <Line 
                type="linear" 
                dataKey="price" 
                stroke="url(#cryptoGradient)" 
                strokeWidth={3} 
                dot={false}
                activeDot={{ r: 6, fill: 'var(--accent)', stroke: 'var(--surface-card)', strokeWidth: 2 }}
              />
            </LineChart>
          </ResponsiveContainer>
        </div>
      ) : (
        <div className={`w-full flex items-center justify-center text-secondary`} style={{ height }}>
          No data available.
        </div>
      )}
      <div className="flex flex-col gap-2 mt-2">
        <div className="flex justify-between px-2">
          <span className="text-xs font-bold text-secondary">Timeframe: <span className="text-accent">{TIMEFRAMES[timeIndex].fullLabel}</span></span>
        </div>
        <input 
          type="range" 
          min={0} 
          max={TIMEFRAMES.length - 1} 
          step={1} 
          value={timeIndex}
          onChange={(e) => setTimeIndex(Number(e.target.value))}
          className="w-full h-2 bg-surface-inset rounded-lg appearance-none cursor-pointer accent-[#e05a2b]"
        />
        <div className="relative w-full h-4 mt-2 text-[10px] text-muted font-medium">
          {TIMEFRAMES.map((t, i) => {
            const percent = (i / (TIMEFRAMES.length - 1)) * 100;
            return (
              <span 
                key={i} 
                className={`absolute top-0 -translate-x-1/2 text-center whitespace-nowrap ${i === timeIndex ? 'text-accent font-bold' : ''}`} 
                style={{ left: `calc(${percent}% + ${8 - (percent / 100) * 16}px)` }}
              >
                {t.label}
              </span>
            );
          })}
        </div>
        {COIN_RELEASE_DATES[selectedCoin] && (
          <div className="text-center mt-6 text-xs text-muted">
            <span className="font-medium text-secondary">Release Date:</span> {new Date(COIN_RELEASE_DATES[selectedCoin]).toLocaleDateString(undefined, { year: 'numeric', month: 'long', day: 'numeric' })}
          </div>
        )}
      </div>
    </div>
  );
}
