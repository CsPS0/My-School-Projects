import Link from "next/link";
import { ArrowRight, BarChart2 } from "lucide-react";
export default function LandingPage() {
  return (
    <div className="flex flex-col min-h-[80vh] items-center justify-center text-center px-4">
      <div className="w-16 h-16 bg-gradient-to-br from-accent to-accent-warm mb-8 rounded-xl flex items-center justify-center transform rotate-3">
        <BarChart2 size={32} className="text-surface-base transform -rotate-3" />
      </div>
      <h1 className="text-3xl sm:text-4xl md:text-5xl font-bold text-primary mb-6 max-w-3xl leading-tight break-words sm:break-normal">
        WorldWideDashBoard
      </h1>
      <p className="text-xl text-secondary mb-10 max-w-2xl">
        See your Steam stats, YouTube channel performance, crypto portfolio, and global music charts all in one simple place. No clutter, just the numbers you care about.
      </p>
      <div className="flex flex-col sm:flex-row gap-4 items-center">
        <Link 
          href="/signup" 
          className="bg-accent text-surface-base font-bold px-8 py-3.5 rounded-lg text-base flex items-center gap-2 hover:bg-accent/90 transition-colors"
        >
          Create account
          <ArrowRight size={18} />
        </Link>
        <Link 
          href="/login" 
          className="bg-transparent border-2 border-border-default text-primary font-bold px-8 py-3.5 rounded-lg text-base hover:bg-surface-inset transition-colors"
        >
          Sign in
        </Link>
      </div>

    </div>
  );
}
