using BotConstructor.Database.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotConstructor.Database.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public string UserName { get; set; }

        public bool IsActiveQuiz { get; set; }
        public int? QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public int? QuizStepNumber { get; set;}

        public int BotId { get; set; }
        public Bot Bot { get; set; }
    }
}
