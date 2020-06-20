using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BotConstructor.Web.Models.Message
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        [Required]
        public string InputMessage { get; set; }
        [Required]
        public string OutputMessage { get; set; }
        public int BotId { get; set; }
        public List<SelectListItem> Bots { get; set; }
    }
}
