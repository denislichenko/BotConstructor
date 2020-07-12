using BotConstructor.Database.Models;
using BotConstructor.Database.Models.QuizModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            await CreateUserIfNew(messageEventArgs.Message.Chat.Id, messageEventArgs.Message.Chat.Username);

            var isQuiz = context.Quizzes.Any(x => x.Bot.BotId == client.BotId && x.TriggerCommand == message) ||
                         context.Chats.First(x => x.ChatId == messageEventArgs.Message.Chat.Id).IsActiveQuiz; 

            if(isQuiz) await QuizMessage(messageEventArgs.Message); 
            else await TextMessage(messageEventArgs.Message); 
        }

        public async Task CreateUserIfNew(long chatId, string userName)
        {
            var isExist = context.Chats.Any(x => x.ChatId == chatId);
            if (!isExist)
            {
                var botId = (await context.Bots.FirstAsync(x => x.BotId == client.BotId)).Id;
                await context.Chats.AddAsync(new Chat { ChatId = chatId, UserName = userName, BotId = botId });
                await context.SaveChangesAsync();
            }
        }

        public async Task TextMessage(Telegram.Bot.Types.Message msg)
        {
            var answer = await context.Messages.Include(x => x.Bot).FirstOrDefaultAsync(x => x.Bot.BotId == client.BotId && x.InputMessage == msg.Text);
            if (answer == null) await client.SendTextMessageAsync(msg.Chat.Id, "???");
            else await client.SendTextMessageAsync(msg.Chat.Id, answer.OutputMessage); 
        }

        public async Task QuizMessage(Telegram.Bot.Types.Message msg)
        {
            var chat = await context.Chats.FirstAsync(x => x.ChatId == msg.Chat.Id);
            var quiz = await context.Quizzes.Include(x => x.QuizSteps).Where(x => x.Bot.BotId == client.BotId).FirstAsync();

            StartQuizIfNot(chat, quiz.Id);
            SaveMessage(msg, chat, quiz.Id);

            string answer = quiz.QuizSteps.FirstOrDefault(x => x.StepNumber == chat.QuizStepNumber).Text;

            if (chat.QuizStepNumber == quiz.QuizSteps.Max(x => x.StepNumber))
                StopQuiz(chat);
            else chat.QuizStepNumber++;

            await context.SaveChangesAsync(); 

            await client.SendTextMessageAsync(msg.Chat.Id, answer);
        }

        public void StartQuizIfNot(Chat chat, int quizId)
        {
            if (!chat.IsActiveQuiz)
            {
                chat.IsActiveQuiz = true;
                chat.QuizId = quizId;
                chat.QuizStepNumber = 1; 
            }
        }

        public void StopQuiz(Chat chat)
        {
            chat.IsActiveQuiz = false;
            chat.QuizId = null;
            chat.QuizStepNumber = null;
        }

        public void SaveMessage(Telegram.Bot.Types.Message msg, Chat chat, int quizId)
        {
            if(chat.QuizStepNumber > 1)
            {
                context.QuizAnswers.Add(new QuizAnswer
                {
                    QuizStepId = context.QuizSteps.First(x => x.QuizId == chat.QuizId && x.StepNumber == chat.QuizStepNumber).Id,
                    Text = msg.Text
                }); 
            }
        }
    }
}
