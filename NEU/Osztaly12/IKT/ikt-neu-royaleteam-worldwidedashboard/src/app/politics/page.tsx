"use client";
import React, { useEffect, useState } from 'react';
import { WidgetCard } from '../components/WidgetCard';
import { Landmark, Users } from 'lucide-react';
export default function PoliticsPage() {
  const [politics, setPolitics] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [selectedMinisterCountry, setSelectedMinisterCountry] = useState('Hungary');
  useEffect(() => {
    fetch('/api/dashboard')
      .then((res) => res.json())
      .then((data) => {
        const politicsData = data['world_politics'];
        if (politicsData && politicsData.parties && politicsData.parties.length > 0) {
           const sortedParties = [...politicsData.parties].sort((a: any, b: any) => b.totalMandates - a.totalMandates);
           politicsData.leadingParty = sortedParties[0].name;
        }
        setPolitics(politicsData);
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
  return (
    <div className="w-full space-y-10">
      <div className="grid grid-cols-1 xl:grid-cols-2 gap-10">
        <WidgetCard id="hungary" title="World Politics & Elections - Hungarian Elections 2026 (OGY)" icon={Landmark} delay={100} className="w-full h-full flex flex-col">
        {politics ? (
          <div className="flex flex-col flex-grow justify-center p-4 h-full">
            <div className="grid grid-cols-1 sm:grid-cols-3 gap-6">
              <div className="flex flex-col justify-center items-center bg-surface-inset border border-border-subtle p-6 rounded-2xl">
                <Users size={28} className="text-primary mb-3" />
                <div className="text-5xl font-bold text-primary mb-2">{politics.turnoutPercentage}%</div>
                <div className="text-base text-secondary">Voter Turnout</div>
              </div>
              <div className="flex flex-col justify-center items-center bg-surface-card border-border-subtle border p-6 rounded-2xl">
                <Landmark size={28} className="text-accent-warm mb-3" />
                <div className="text-4xl font-bold text-accent-warm mb-3 text-center">{politics.leadingParty}</div>
                <div className="text-base text-secondary">Leading Party</div>
              </div>
              <div className="flex flex-col justify-center items-center bg-surface-inset border border-border-subtle p-6 rounded-2xl">
                <div className="text-5xl font-bold text-primary mb-3">{politics.processedPercentage}%</div>
                <div className="text-base text-secondary">Processed Votes</div>
                <div className="w-full bg-surface-base h-2 rounded-full mt-4 overflow-hidden">
                  <div 
                    className="bg-accent h-2 rounded-full" 
                    style={{ width: `${politics.processedPercentage}%` }}
                  ></div>
                </div>
              </div>
            </div>
          </div>
        ) : (
          <div className="text-muted text-center py-12">Data unavailable</div>
        )}
        </WidgetCard>
        
        <WidgetCard id="ministers" title="Current Government Ministers" icon={Users} delay={150} className="w-full h-full flex flex-col">
          {politics?.ministers ? (
            <div className="p-6 flex flex-col h-full">
              <div className="mb-6 flex items-center justify-between">
                <span className="text-sm font-bold text-secondary">Select Country</span>
                <select 
                  value={selectedMinisterCountry}
                  onChange={(e) => setSelectedMinisterCountry(e.target.value)}
                  className="bg-surface-inset border border-border-subtle rounded-lg px-4 py-2 text-sm text-primary outline-none focus:border-accent"
                >
                  <option value="Hungary">Hungary</option>
                  <option value="Slovakia">Slovakia</option>
                  <option value="Ukraine">Ukraine</option>
                  <option value="France">France</option>
                  <option value="USA">United States</option>
                </select>
              </div>
              
              <div className="space-y-4 flex-grow">
                {politics.ministers[selectedMinisterCountry]?.map((minister: any, idx: number) => (
                  <div key={idx} className="bg-surface-inset border border-border-subtle p-4 rounded-xl flex flex-col sm:flex-row sm:items-center justify-between gap-2">
                    <div className="font-bold text-primary text-base">{minister.name}</div>
                    <div className="text-sm text-accent-warm font-medium">{minister.role}</div>
                  </div>
                ))}
              </div>
            </div>
          ) : (
            <div className="text-muted text-center py-12">Data unavailable</div>
          )}
        </WidgetCard>
      </div>
      <div className="grid grid-cols-1 xl:grid-cols-2 gap-10">
        <WidgetCard id="leaders" title="World Leaders Directory" icon={Users} delay={200} className="w-full h-full flex flex-col">
          {politics?.globalLeaders ? (
          <div className="overflow-x-auto p-4">
            <table className="w-full text-sm text-left">
              <thead className="text-[11px] text-muted uppercase border-b border-border-subtle">
                <tr>
                  <th className="pb-2 font-medium">Country</th>
                  <th className="pb-2 font-medium">Name</th>
                  <th className="pb-2 font-medium">Role</th>
                  <th className="pb-2 font-medium text-right">Time Remaining</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-border-subtle">
                {politics.globalLeaders.map((leader: any) => (
                  <tr key={leader.id}>
                    <td className="py-3 font-medium text-accent-warm text-sm">{leader.country}</td>
                    <td className="py-3 font-bold text-primary text-sm">{leader.name}</td>
                    <td className="py-3 text-secondary text-sm">{leader.role}</td>
                    <td className="py-3 text-right text-sm">
                      {leader.timeRemaining && (
                        <span className="bg-surface-inset border border-border-subtle px-2.5 py-0.5 rounded-full text-secondary text-xs">
                          {leader.timeRemaining}
                        </span>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        ) : (
          <div className="text-muted text-center py-12">Data unavailable</div>
          )}
        </WidgetCard>
        <WidgetCard id="parliaments" title="Global Parliaments Overview" icon={Landmark} delay={300} className="w-full h-full flex flex-col">
          {politics?.globalParliaments ? (
          <div className="overflow-x-auto p-4">
            <table className="w-full text-sm text-left">
              <thead className="text-[11px] text-muted uppercase border-b border-border-subtle">
                <tr>
                  <th className="pb-2 font-medium">Country</th>
                  <th className="pb-2 font-medium">Chamber</th>
                  <th className="pb-2 font-medium text-right">Women Representation</th>
                  <th className="pb-2 font-medium text-right">Next Election</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-border-subtle">
                {politics.globalParliaments.map((parl: any) => (
                  <tr key={parl.country + parl.chamber}>
                    <td className="py-3 font-bold text-primary text-sm">{parl.country}</td>
                    <td className="py-3 text-secondary text-sm">{parl.chamber}</td>
                    <td className="py-3 text-right">
                      <span className="bg-surface-inset border border-border-subtle px-2.5 py-0.5 rounded-full text-accent-warm font-medium text-xs">
                        {parl.womenPercentage}%
                      </span>
                    </td>
                    <td className="py-3 text-right font-bold text-primary text-sm">{parl.nextElection}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        ) : (
          <div className="text-muted text-center py-12">Data unavailable</div>
          )}
        </WidgetCard>
      </div>
    </div>
  );
}
