"use client";
import { useState } from 'react';
import Link from 'next/link';
export default function LoginPage() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [showForgotModal, setShowForgotModal] = useState(false);
  const [resetUsername, setResetUsername] = useState('');
  const [resetPassword, setResetPassword] = useState('');
  const [resetError, setResetError] = useState('');
  const [resetSuccess, setResetSuccess] = useState('');
  const [resetLoading, setResetLoading] = useState(false);
  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    try {
      const res = await fetch('/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password }),
      });
      const data = await res.json();
      if (res.ok) {
        window.location.href = '/overview';
      } else {
        setError(data.error || 'Login failed');
      }
    } catch {
      setError('An error occurred. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  const handleReset = async (e: React.FormEvent) => {
    e.preventDefault();
    setResetLoading(true);
    setResetError('');
    setResetSuccess('');
    try {
      const res = await fetch('/api/auth/reset', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username: resetUsername, newPassword: resetPassword }),
      });
      const data = await res.json();
      if (res.ok) {
        setResetSuccess('Password reset successfully! You can now log in.');
        setTimeout(() => setShowForgotModal(false), 2000);
      } else {
        setResetError(data.error || 'Password reset failed');
      }
    } catch {
      setResetError('An error occurred. Please try again.');
    } finally {
      setResetLoading(false);
    }
  };
  return (
    <div className="min-h-[70vh] flex items-center justify-center">
      <div className="glass-card p-8 w-full max-w-sm">
        <h2 className="text-xl font-semibold text-primary mb-1">Welcome back</h2>
        <p className="text-sm text-secondary mb-6">Sign in to your dashboard.</p>
        {error && (
          <div className="bg-danger/10 border border-danger/20 text-danger p-3 rounded-lg mb-4 text-sm">
            {error}
          </div>
        )}
        <form onSubmit={handleLogin} className="space-y-4">
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
            <div className="flex justify-between items-end mb-1.5">
              <label className="text-xs font-medium text-secondary uppercase tracking-wider" htmlFor="password">
                Password
              </label>
              <button 
                type="button" 
                onClick={() => {
                  setShowForgotModal(true);
                  setResetError('');
                  setResetSuccess('');
                  setResetUsername('');
                  setResetPassword('');
                }}
                className="text-xs text-accent hover:underline"
              >
                Forgot password?
              </button>
            </div>
            <input
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
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
              'Sign In'
            )}
          </button>
        </form>
        <p className="mt-5 text-center text-secondary text-sm">
          No account?{' '}
          <Link href="/signup" className="text-accent hover:underline font-medium">
            Sign up
          </Link>
        </p>
      </div>

      {showForgotModal && (
        <div className="fixed inset-0 bg-black/60 backdrop-blur-sm z-50 flex items-center justify-center p-4 animate-fade-in">
          <div className="glass-card w-full max-w-sm p-6 relative">
            <h3 className="text-lg font-bold text-primary mb-2">Reset Password</h3>
            <p className="text-sm text-secondary mb-4">
              Enter your username and new password to simulate an email reset link.
            </p>
            {resetError && <div className="text-danger text-sm mb-3">{resetError}</div>}
            {resetSuccess && <div className="text-success text-sm mb-3">{resetSuccess}</div>}
            
            <form onSubmit={handleReset} className="space-y-4">
              <div className="space-y-1.5">
                <label className="text-xs font-medium text-secondary uppercase tracking-wider">Username</label>
                <input
                  type="text"
                  value={resetUsername}
                  onChange={(e) => setResetUsername(e.target.value)}
                  className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2.5 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors"
                  required
                />
              </div>
              <div className="space-y-1.5">
                <label className="text-xs font-medium text-secondary uppercase tracking-wider">New Password</label>
                <input
                  type="password"
                  value={resetPassword}
                  onChange={(e) => setResetPassword(e.target.value)}
                  className="w-full bg-surface-inset border border-border-subtle rounded-lg px-3 py-2.5 text-sm text-primary placeholder:text-muted outline-none focus:border-accent transition-colors"
                  required
                />
              </div>
              <div className="flex gap-3 mt-5">
                <button
                  type="button"
                  onClick={() => setShowForgotModal(false)}
                  className="flex-1 px-4 py-2 bg-surface-inset hover:bg-surface-elevated text-primary border border-border-subtle rounded-lg transition-colors text-sm font-medium"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  disabled={resetLoading}
                  className="flex-1 px-4 py-2 bg-accent hover:bg-accent/90 text-surface-base rounded-lg transition-colors text-sm font-medium flex justify-center items-center"
                >
                  {resetLoading ? <div className="w-4 h-4 border-2 border-surface-base/20 border-t-surface-base rounded-full animate-spin" /> : 'Reset'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
