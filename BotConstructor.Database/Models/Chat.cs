using System;
using System.Collections.Generic;
using System.Text;

namespace BotConstructor.Database.Models
{
    public class Chat
    {
        public long ChatId { get; set; }
        public string UserName { get; set; }

        public int BotId { get; set; }
        public Bot Bot { get; set; }
    }
}
