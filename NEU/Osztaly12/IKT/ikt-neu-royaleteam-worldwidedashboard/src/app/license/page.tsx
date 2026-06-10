import React from 'react';
import Link from 'next/link';
import { ArrowLeft, FileText, Scale } from 'lucide-react';

export default function LicensePage() {
  const licenseText = `MIT License

Copyright (c) 2026 Royal Team

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.`;

  return (
    <div className="min-h-[80vh] flex items-center justify-center py-10 px-4">
      <div className="w-full max-w-3xl">
        <div className="mb-6 flex justify-between items-end">
          <div>
            <div className="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-accent/10 border border-accent/20 text-accent text-xs font-bold uppercase tracking-wider mb-4">
              <Scale size={14} /> Legal
            </div>
            <h1 className="text-4xl font-bold text-primary flex items-center gap-3">
              <FileText className="text-accent-warm" size={36} />
              Open Source License
            </h1>
          </div>
          <Link 
            href="/overview" 
            className="hidden sm:flex items-center gap-2 text-sm font-semibold text-secondary hover:text-primary transition-colors bg-surface-inset border border-border-default hover:border-border-subtle rounded-lg px-4 py-2"
          >
            <ArrowLeft size={16} /> Back to Dashboard
          </Link>
        </div>

        <div className="bg-surface-card border border-border-default rounded-2xl p-6 sm:p-10 shadow-2xl backdrop-blur-xl">
          <pre className="whitespace-pre-wrap font-mono text-sm leading-relaxed text-secondary overflow-x-auto">
            {licenseText}
          </pre>
        </div>

        <div className="mt-6 flex justify-center sm:hidden">
          <Link 
            href="/overview" 
            className="flex items-center gap-2 text-sm font-semibold text-secondary hover:text-primary transition-colors bg-surface-inset border border-border-default hover:border-border-subtle rounded-lg px-4 py-2"
          >
            <ArrowLeft size={16} /> Back to Dashboard
          </Link>
        </div>
      </div>
    </div>
  );
}
