# Style Guide

This document contains the core design elements and color palette used for the visual appearance of the project.

## Color Palette

The project utilizes a modern, clean, Dark Theme color scheme, structured as follows:

<div align="left">

| Color (Name) | HEX Code | CSS Variable / Tailwind Token | Role |
| :--- | :--- | :--- | :--- |
| **Very Dark Gray** | `#111111` | `--surface-base` | Main application background |
| **Dark Gray** | `#1a1a1a` | `--surface-card` | Background for cards and panels |
| **Hover Gray** | `#222222` | `--surface-card-hover` | Interactive (hover) state for cards |
| **Deep Border** | `#2a2a2a` | `--border-default` | Borders for cards and elements |
| **Orange** | `#e05a2b` | `--accent` | Highlights, buttons, icons, interactions |
| **Light Gray** | `#e0e0e0` | `--text-primary` | Primary text (Headings, main content) |
| **Dim Gray** | `#888888` | `--text-secondary` | Secondary text, metadata, icons |

> **Strict Rule:** *Only* these base colors and their derived transparencies may appear on the UI surface. The system automatically maps these into the Tailwind v4 syntax.

</div>

---

## Usage in Code

### Tailwind CSS v4 & `globals.css`

The project uses **Tailwind CSS v4**, so the colors are defined directly in the `src/app/globals.css` file using `@theme inline`, eliminating the need for a separate `tailwind.config.ts` file.

```css
@theme inline {
  --color-surface-base: var(--surface-base);
  --color-surface-sidebar: var(--surface-sidebar);
  --color-surface-card: var(--surface-card);
  --color-surface-card-hover: var(--surface-card-hover);
  --color-surface-inset: var(--surface-inset);
  
  --color-border-subtle: var(--border-subtle);
  --color-border-default: var(--border-default);
  
  --color-primary: var(--text-primary);
  --color-secondary: var(--text-secondary);
  --color-muted: var(--text-muted);
  
  --color-accent: var(--accent);
}
```

These colors can be used directly as classes within JSX/TSX components, for example:
- For backgrounds: `bg-surface-card`, `bg-surface-base`
- For text: `text-primary`, `text-secondary`, `text-accent`
- For borders: `border-border-default`

---

## Typography

To ensure a modern and clean appearance (aligning with the Next.js / Tailwind stack), the following approach is recommended:
- **Primary Font**: `Inter`, `Roboto`, `Outfit`, or `Geist` (default).
- **Character**: Minimalist, highly legible, with a strong hierarchy (bold headings, thinner body text).
- **Custom Icons**: Instead of OS-level emojis (e.g., 🦅), we use CSS-colorable text characters (e.g., Ħ) or raw SVGs (`fill="currentColor"`) to ensure everything perfectly matches the `--accent` color.

## Layout & Shapes

The project follows **Dark Mode Glassmorphism** & **Modern Flat** design principles:
- **Panel Layout**: Data, charts, and widgets are displayed in clearly separated, clean cards (using the `.glass-card` class).
- **Layout Constraints**: The Dashboard supports both full-width (fluid) layout and a more focused Compact view (centered via `max-w-6xl mx-auto` tailwind classes).
- **Corner Radius**: Distinct, friendly rounding, such as `rounded-xl` or `rounded-2xl`.
- **Inset**: Use `bg-surface-inset` to separate data blocks within cards.
- **Special Views (404 Page)**: Error pages break from traditional layouts; for example, the 404 page uses a completely unique, center-aligned "talking lion" layout with its own independent animations.

## Animations and Interactions

Since the project uses **Anime.js** and Tailwind, the UI must feel "alive":
- **Micro-interactions (Card hover)**: When hovering over cards, the border color becomes more vibrant, and the box shadow increases (built-in effects of `glass-card`).
- **Transitions**: Use Tailwind transitions (e.g., `transition-colors`, `duration-200`) on buttons and links.
- **Neon effects**: The `.glow-accent` class can be used to achieve a special, glowing shadow effect around highlighted data.