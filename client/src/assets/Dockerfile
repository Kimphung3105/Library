FROM node:22-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm clean-install
COPY . .
RUN npm run build

FROM nginx:alpine AS final
COPY nginx.conf.template /
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 8080
CMD envsubst '$BACKEND_URL' < /nginx.conf.template > /etc/nginx/conf.d/default.conf \
  && nginx -g 'daemon off;'