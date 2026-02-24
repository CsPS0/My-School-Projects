const packages = [
  {
    id: "starter",
    name: "Starter",
    short_description:
      "Learn the fundamentals of modern web development with a structured beginner path.",
    price: 19,
    discount_percentage: 0.15,
    includes_additional: [
      "Core curriculum: HTML, CSS, basic JavaScript",
      "Beginner exercises (50+)",
      "Downloadable cheat sheets (HTML/CSS)",
      "Community access (read-only)",
    ],
    includes_all_from: [],
  },
  {
    id: "bronze",
    name: "Bronze",
    short_description:
      "Build real projects and gain confidence with guided practice and feedback tools.",
    price: 39,
    discount_percentage: 0.20,
    includes_additional: [
      "Project track: 3 guided projects (responsive landing page, portfolio, mini app)",
      "Code review checklist templates",
      "Community access (post + comment)",
    ],
    includes_all_from: ["starter"],
  },
  {
    id: "silver",
    name: "Silver",
    short_description:
      "Level up with intermediate JavaScript, tooling, and a complete front-end workflow.",
    price: 69,
    discount_percentage: 0.25,
    includes_additional: [
      "Intermediate JavaScript modules (ES6+, async, modules)",
      "Tooling: Git + GitHub workflow, Vite basics",
      "Testing basics (unit tests introduction)",
      "Monthly live workshop replay library",
    ],
    includes_all_from: ["starter", "bronze"],
  },
  {
    id: "gold",
    name: "Gold",
    short_description:
      "Go pro with full-stack foundations, deployment, and career-ready portfolio projects.",
    price: 109,
    discount_percentage: 0.30,
    includes_additional: [
      "Backend fundamentals (Node.js + REST APIs)",
      "Database basics (SQL + simple schema design)",
      "Deployment module (CI basics + hosting walkthroughs)",
      "Portfolio pack: 2 advanced projects (dashboard + full-stack app)",
    ],
    includes_all_from: ["starter", "bronze", "silver"],
  },
  {
    id: "diamond",
    name: "Diamond",
    short_description:
      "Maximum support: mentoring, interview prep, and personalized guidance end-to-end.",
    price: 179,
    discount_percentage: 0.35,
    includes_additional: [
      "2x mentoring sessions / month (30 min each)",
      "CV/LinkedIn review + portfolio feedback",
      "Mock interview pack (front-end + full-stack)",
      "Priority support (48h response target)",
      "Private cohort channel access",
    ],
    includes_all_from: ["starter", "bronze", "silver", "gold"],
  },
];

export function allPackages() {
  return packages;
}

export function mergeIncludes(id) {
  const mainPackage = packages.find((p) => p.id === id);
  if (!mainPackage) return [];

  const includedPackages = mainPackage.includes_all_from
    .map((includedId) => packages.find((p) => p.id === includedId))
    .filter((p) => p !== undefined);

  return [...includedPackages, mainPackage];
}