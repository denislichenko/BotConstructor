using System;
using System.Collections.Generic;
using System.Text;

namespace BotConstructor.Database.Models.QuizModels
{
    public class QuizStep
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public ICollection<QuizAnswer> QuizAnswers { get; set; }
    }
}
