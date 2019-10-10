FROM artyomturkin/vuepress
WORKDIR /app
COPY ./Docs .
EXPOSE 8080
RUN npm i
ENTRYPOINT ["npm", "start"]