using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotConstructor.Web.Models.Message
{
    public class MessageListViewModel
    {
        public int BotId { get; set; }
        public List<Database.Models.Message> Messages { get; set; }
    }
}
