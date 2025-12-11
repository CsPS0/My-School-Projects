FROM php:8.4-cli-alpine3.21
WORKDIR /app
COPY . .
RUN apk update && apk add --no-cache bash
ENTRYPOINT [ "/bin/bash" ]