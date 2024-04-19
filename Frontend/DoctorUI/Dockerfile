FROM node:alpine as angular-built
WORKDIR /usr/src/app
RUN npm i -g @angular/cli
COPY . .
FOROM as build
RUN npm install
EXPOSE 4200
CMD [ "ng", "serve", "--host", "0.0.0.0" ]
