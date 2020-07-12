using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotConstructor.Web.Models.Quiz
{
    public class QuizListViewModel
    {
        public int BotId { get; set; }
        public List<BotConstructor.Database.Models.QuizModels.Quiz> Quizzes { get; set; }
    }
}
