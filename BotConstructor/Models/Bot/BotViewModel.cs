using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BotConstructor.Web.Models.Bot
{
    public class BotViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }
        public string Name { get; set; }
    }
}
