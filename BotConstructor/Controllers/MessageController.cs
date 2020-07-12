using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotConstructor.Database.Models;
using BotConstructor.Web.Models.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BotConstructor.Web.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationContext _context; 

        public MessageController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List(int botId)
        {
            if (botId > 0)
            {
                var messageList = await _context.Messages.Where(x => x.BotId == botId).ToListAsync();
                var model = new MessageListViewModel
                {
                    BotId = botId,
                    Messages = messageList
                }; 

                return View(model);
            }
            return Redirect(Url.Action("List", "Bot"));
        }

        public IActionResult Create(int botId)
        {
            if (botId > 0)
            {
                var model = new MessageViewModel { BotId = botId };
                return View(model); 
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessageViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _context.Messages.AddAsync(new Message
                {
                    InputMessage = model.InputMessage,
                    OutputMessage = model.OutputMessage,
                    BotId = model.BotId
                });

                await _context.SaveChangesAsync();

                return RedirectToAction("List", new { botId = model.BotId });
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {       
            if(id > 0)
            {
                var msg = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
                if(msg != null)
                {
                    var model = new MessageViewModel
                    {
                        Id = msg.Id,
                        InputMessage = msg.InputMessage,
                        OutputMessage = msg.OutputMessage,
                        BotId = msg.BotId
                    };
                    return View(model);
                }  
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var msg = await _context.Messages.FirstAsync(x => x.Id == model.Id);

                if(msg != null)
                {
                    msg.InputMessage = model.InputMessage;
                    msg.OutputMessage = model.OutputMessage;
                    msg.BotId = model.BotId;

                    await _context.SaveChangesAsync();
                }               

                return RedirectToAction("List", new { botId = model.BotId});
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);

            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("List", new { botId = message.BotId }); 
        }
    }
}