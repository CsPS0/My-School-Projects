"use client";
import React from "react";
import Link from "next/link";
import LionIcon from "./components/LionIcon";
export default function NotFound() {
  return (
    <>
      <style>{`
        .nf-root {
          font-family: var(--font-geist), system-ui, -apple-system, sans-serif;
          height: 100%;
          min-height: 60vh;
          background: var(--surface-base);
          display: flex;
          align-items: center;
          justify-content: center;
          overflow: hidden;
          position: relative;
        }.nf-rule {
          width: 48px;
          height: 2px;
          background: linear-gradient(90deg, var(--accent), transparent);
          margin: 0 0 28px 0;
        }@keyframes subtlePulse {
          0%, 100% { opacity: 1; }
          50%       { opacity: 0.7; }
        }
        .nf-404 {
          font-size: clamp(96px, 18vw, 160px);
          font-weight: 900;
          line-height: 1;
          letter-spacing: -4px;
          color: transparent;
          -webkit-text-stroke: 1.5px var(--border-default);
          user-select: none;
          animation: subtlePulse 4s ease-in-out infinite;
        }@keyframes lionFloat {
          0%, 100% { transform: translateY(0px); }
          50%       { transform: translateY(-8px); }
        }
        }.nf-bubble {
          position: relative;
          background: var(--surface-card);
          border: 1px solid var(--border-default);
          border-radius: 20px;
          padding: 24px 28px;
          backdrop-filter: blur(6px);
          box-shadow: 0 8px 32px rgba(0,0,0,0.3);
          max-width: 380px;
        }
        }
        .nf-quote {
          font-style: italic;
          font-size: clamp(15px, 2vw, 18px);
          line-height: 1.65;
          color: var(--text-primary);
          margin: 0;
          font-weight: 500;
        }
        .nf-quote-attr {
          display: block;
          margin-top: 12px;
          font-size: 11px;
          font-weight: 700;
          letter-spacing: 0.15em;
          text-transform: uppercase;
          color: var(--accent);
          font-style: normal;
        }.nf-headline {
          font-size: clamp(22px, 4vw, 32px);
          font-weight: 800;
          color: var(--text-primary);
          line-height: 1.2;
          margin: 0 0 10px;
        }
        .nf-sub {
          font-size: 14px;
          font-weight: 500;
          color: var(--text-secondary);
          margin: 0 0 32px;
          letter-spacing: 0.02em;
        }.nf-btn-ghost {
          display: inline-flex;
          align-items: center;
          gap: 8px;
          padding: 11px 22px;
          border-radius: 10px;
          border: 2px solid var(--border-default);
          background: transparent;
          color: var(--text-primary);
          font-size: 13px;
          font-weight: 700;
          cursor: pointer;
          transition: border-color 0.2s, background 0.2s;
          text-decoration: none;
        }
        .nf-btn-ghost:hover {
          border-color: var(--border-subtle);
          background: var(--surface-inset);
        }
        .nf-btn-primary {
          display: inline-flex;
          align-items: center;
          gap: 8px;
          padding: 11px 22px;
          border-radius: 10px;
          border: none;
          background: var(--accent);
          color: var(--surface-base);
          font-size: 13px;
          font-weight: 700;
          cursor: pointer;
          transition: background 0.2s;
          text-decoration: none;
        }
        .nf-btn-primary:hover {
          background: var(--accent-warm);
        }.nf-badge {
          display: inline-block;
          font-size: 10px;
          font-weight: 700;
          letter-spacing: 0.18em;
          text-transform: uppercase;
          color: var(--accent);
          border: 1px solid var(--border-default);
          border-radius: 4px;
          padding: 4px 10px;
          margin-bottom: 20px;
          background: var(--surface-inset);
        }.nf-divider {
          width: 100%;
          height: 1px;
          background: linear-gradient(90deg, transparent, var(--border-default), transparent);
          margin: 8px 0 32px;
        }.nf-footer {
          font-size: 11px;
          color: var(--text-secondary);
          letter-spacing: 0.08em;
          margin-top: 28px;
          font-weight: 500;
        }@keyframes fadeInUp {
          from { opacity: 0; transform: translateY(20px); }
          to   { opacity: 1; transform: translateY(0); }
        }
        .nf-animate {
          animation: fadeInUp 0.6s ease both;
        }
        .nf-animate-delay-1 { animation-delay: 0.1s; }
        .nf-animate-delay-2 { animation-delay: 0.22s; }
        .nf-animate-delay-3 { animation-delay: 0.36s; }
        .nf-animate-delay-4 { animation-delay: 0.5s; }
      `}</style>
      <div className="nf-root">
        <div
          style={{
            position: "relative",
            zIndex: 10,
            width: "100%",
            maxWidth: "960px",
            padding: "40px 24px",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <div className="nf-animate nf-404" aria-hidden="true">
            404
          </div>
          <div
            className="nf-animate nf-animate-delay-1"
            style={{
              display: "flex",
              flexDirection: "row",
              flexWrap: "wrap",
              gap: "48px",
              alignItems: "center",
              justifyContent: "center",
              marginTop: "-16px",
              width: "100%",
            }}
          >
            <LionIcon
              style={{ width: 140, height: 140, color: "var(--accent)", filter: "drop-shadow(0 0 20px var(--accent-glow))", animation: "lionFloat 5s ease-in-out infinite" }}
            />
            <div style={{ flex: "1 1 300px", maxWidth: "440px" }}>
              <div className="nf-animate nf-animate-delay-2">
                <div className="nf-badge">Page not found</div>
                <div className="nf-rule" />
                <h1 className="nf-headline">We searched everywhere.</h1>
                <p className="nf-sub">Even the lion helped. Still nothing.</p>
              </div>
              <div className="nf-animate nf-animate-delay-3" style={{ marginBottom: "32px" }}>
                <div className="nf-bubble">
                  <p className="nf-quote">
                    &quot;We checked behind the coats, but no &mdash; this is not the way to Narnia.&quot;
                    <span className="nf-quote-attr">— The Lion, probably</span>
                  </p>
                </div>
              </div>
              <div className="nf-divider" />
              <div
                className="nf-animate nf-animate-delay-4"
                style={{ display: "flex", gap: "12px", flexWrap: "wrap" }}
              >
                <Link href="/overview" className="nf-btn-primary">
                  Return to the real world
                </Link>
              </div>
              <p className="nf-footer nf-animate nf-animate-delay-4">
                Error code 404 · Page does not exist
              </p>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
