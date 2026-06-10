"use client";
import { useState } from 'react';
import Link from 'next/link';
export default function SignupPage() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const handleSignup = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    if (password !== confirmPassword) {
      setError('Passwords do not match');
      setLoading(false);
      return;
    }
    try {
      const res = await fetch('/api/auth/signup', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password }),
      });
      const data = await res.json();
      if (res.ok) {
        window.location.href = '/overview';
      } else {
        setError(data.error || 'Sign up failed');
      }
    } catch {
      setError('An error occurred. Please try again.');
    } finally {
      setLoading(false);
    }
  };
  return (
    <div className="min-h-[70vh] flex items-center justify-center">
      <div className="glass-card p-8 w-full max-w-sm">
        <h2 className="text-xl font-semibold text-primary mb-1">Create account</h2>
        <p className="text-sm text-secondary mb-6">Start customizing your dashboard.</p>
        {error && (
          <div className="bg-danger/10 border border-danger/20 text-danger p-3 rounded-lg mb-4 text-sm">
            {error}
          </div>
        )}
        <form onSubmit={handleSignup} className="space-y-4">
          <div className="space-y-1.5">
            <label className="text-xs font-medium text-secondary uppercase tracking-wider" htmlFor="username">
              Username
            </label>
            <input
              id="username"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2.5 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors"
              required
            />
          </div>
          <div className="space-y-1.5">
            <label className="text-xs font-medium text-secondary uppercase tracking-wider" htmlFor="password">
              Password
            </label>
            <input
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2.5 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors"
              required
            />
          </div>
          <div className="space-y-1.5">
            <label className="text-xs font-medium text-secondary uppercase tracking-wider" htmlFor="confirmPassword">
              Confirm Password
            </label>
            <input
              id="confirmPassword"
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2.5 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors"
              required
            />
          </div>
          <button
            type="submit"
            disabled={loading}
            className="w-full bg-accent hover:bg-accent/90 text-surface-base font-semibold py-2.5 rounded-lg transition-colors mt-2 flex justify-center items-center text-sm"
          >
            {loading ? (
              <div className="w-4 h-4 border-2 border-surface-base/20 border-t-surface-base rounded-full animate-spin" />
            ) : (
              'Create Account'
            )}
          </button>
        </form>
        <p className="mt-5 text-center text-secondary text-sm">
          Already have an account?{' '}
          <Link href="/login" className="text-accent hover:underline font-medium">
            Sign in
          </Link>
        </p>
      </div>
    </div>
  );
}
