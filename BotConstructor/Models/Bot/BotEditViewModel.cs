using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotConstructor.Web.Models.Bot
{
    public class BotEditViewModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public List<BotMessage> Messages { get; set; }

        public BotEditViewModel()
        {
            Messages = new List<BotMessage>(); 
        }
        
    }

    public class BotMessage
    {
        public int Id { get; set; }
        public string InputMessage { get; set; }
        public string OutputMessage { get; set; }
    }
}
