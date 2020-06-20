using System;
using System.Collections.Generic;
using System.Text;

namespace BotConstructor.Database.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string InputMessage { get; set; }
        public string OutputMessage { get; set; }

        public int BotId { get; set; }
        public Bot Bot { get; set; }
    }
}
