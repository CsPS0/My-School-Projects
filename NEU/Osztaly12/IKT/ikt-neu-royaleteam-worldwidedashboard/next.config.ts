import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  output: "standalone",
  allowedDevOrigins: ["http://192.168.68.56:3000", "192.168.68.56"],
};

export default nextConfig;
