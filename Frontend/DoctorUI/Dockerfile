FROM node:alpine as angular-built
WORKDIR /usr/src/app/DoctorUI/
RUN npm i -g @angular/cli
COPY . .

RUN npm install
EXPOSE 4200
CMD [ "ng", "serve", "--host", "0.0.0.0" ]