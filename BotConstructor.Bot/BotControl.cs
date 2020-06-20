using BotConstructor.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace BotConstructor.Bot
{
    public class BotControl
    {
        public TelegramBotClient client;
        private ApplicationContext context = new ApplicationContext(); 

        public int GetBotId(string token = null)
        {
            if (!string.IsNullOrEmpty(token)) return (new TelegramBotClient(token)).BotId;
            else if (client != null) return client.BotId;
            
            return -1; 
        }

        public int StartBot(string token)
        {
            if (string.IsNullOrEmpty(token)) return -1;
            if (client != null) return client.BotId; 

            client = new TelegramBotClient(token);
            client.OnMessage += OnMessageReceived; 
            client.StartReceiving(Array.Empty<UpdateType>());
            return client.BotId; 
        }

        public async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message.Text.Split(' ').First();
            //if (message == null || message.Type != MessageType.Text) return;

            var answer = context.Messages.Include(x => x.Bot).FirstOrDefault(x => x.Bot.BotId == client.BotId && x.InputMessage == message);
            if(answer != null) await client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, answer.OutputMessage); 
            else await client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, "???"); 
        }
    }
}
