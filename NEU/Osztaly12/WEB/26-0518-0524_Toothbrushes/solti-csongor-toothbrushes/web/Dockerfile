FROM node:24-alpine3.22

WORKDIR /app

RUN corepack enable pnpm \
    && pnpm i pluralize lodash json-server@0.17.4 multer body-parser@1.19.0 \
    &&  pnpm add -D nodemon

COPY data data

COPY index.js .

EXPOSE 3000

ENTRYPOINT ["pnpm", "exec", "nodemon", "index.js"]