require("dotenv").config();
const Discord = require("discord.js");
const client = new Discord.Client();

client.on("ready", () => {
  console.log("Logged in");
});

client.on("message", (msg) => {
  console.log(msg.content);
});

client.login(process.env.TOKEN);
