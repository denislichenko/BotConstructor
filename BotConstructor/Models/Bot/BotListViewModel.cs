using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotConstructor.Web.Models.Bot
{
    public class BotListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsWorking { get; set; }
    }
}
