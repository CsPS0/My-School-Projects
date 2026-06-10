FROM oven/bun:1 as builder
WORKDIR /app
COPY package.json bun.lock ./
RUN bun install --frozen-lockfile
COPY . .

ENV DATABASE_URL="file:/app/data/prod.db"
RUN mkdir -p /app/data
RUN bunx prisma db push

RUN bunx prisma generate
ENV NODE_ENV=production
RUN bun run build
FROM oven/bun:1-slim
WORKDIR /app
COPY --from=builder /app/.next/standalone ./
COPY --from=builder /app/.next/static ./.next/static

COPY --from=builder /app/data ./data

ENV NODE_ENV=production
ENV PORT=3000
EXPOSE 3000
CMD ["bun", "server.js"]
