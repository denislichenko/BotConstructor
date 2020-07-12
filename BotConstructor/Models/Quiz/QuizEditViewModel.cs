using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotConstructor.Web.Models.Quiz
{
    public class QuizEditViewModel
    {
        public int Id { get; set; }
        public int BotId { get; set; }
        public string Name { get; set; }
        public string TriggerCommand { get; set; }

        public List<QuizStepViewModel> Steps { get; set; }
    }

    public class QuizStepViewModel
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int StepNumber { get; set; }
    }
}
