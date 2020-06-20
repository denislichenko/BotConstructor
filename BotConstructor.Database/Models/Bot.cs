using System;
using System.Collections.Generic;
using System.Text;

namespace BotConstructor.Database.Models
{
    public class Bot
    {
        public int Id { get; set; }
        public int BotId { get; set; }
        public string Token { get; set; }
        public string Title { get; set; }
        public bool isWorking { get; set; }

        public List<Chat> Chats { get; set; }
        public List<Message> Messages { get; set; }
    }
}
