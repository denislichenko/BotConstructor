using System;
using System.Collections.Generic;
using System.Text;

namespace BotConstructor.Database.Models.QuizModels
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TriggerCommand { get; set; }

        public int BotId { get; set; }
        public Bot Bot { get; set; }

        public ICollection<Chat> Chats { get; set; }
        public ICollection<QuizStep> QuizSteps { get; set; }
    }
}
