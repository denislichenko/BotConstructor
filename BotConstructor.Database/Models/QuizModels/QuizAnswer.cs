using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BotConstructor.Database.Models.QuizModels
{
    public class QuizAnswer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        
        public int QuizStepId { get; set; }
        public QuizStep QuizStep { get; set; }
    }
}
