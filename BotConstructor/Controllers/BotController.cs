﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotConstructor.Bot;
using BotConstructor.Database.Models;
using BotConstructor.Web.Models.Bot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BotConstructor.Web.Controllers
{  

    public class BotController : Controller
    {
        private ApplicationContext _context;
        public BotController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            var model = await _context.Bots.Select(x => new BotListViewModel()
            {
                Id = x.Id, 
                Name = x.Title, 
                IsWorking = x.isWorking
            }).ToListAsync(); 
            return View(model);
        }

        public async Task<IActionResult> Dashboard(int id)
        {
            if(id > 0)
            {
                var bot = await _context.Bots.FirstOrDefaultAsync(x => x.Id == id);
                if(bot != null) return View(new DashboardViewModel { BotId = bot.Id, BotName = bot.Title });
            }

            return RedirectToAction("List"); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            if(id > 0)
            {
                var bot = await _context.Bots.Include(x => x.Messages).FirstOrDefaultAsync(x => x.Id == id);
                if (bot != null)
                {
                    var model = new BotEditViewModel
                    {
                        Id = bot.Id,
                        Token = bot.Token,
                        Name = bot.Title,
                        Messages = bot.Messages.Select(x => new BotMessage
                        {
                            Id = x.Id,
                            InputMessage = x.InputMessage,
                            OutputMessage = x.OutputMessage
                        }).ToList() 
                    };
                    return View(model);
                }
            }

            return RedirectToAction("List"); 
        }


        [HttpPost]
        public async Task<IActionResult> AddBot(BotViewModel model)
        {
            BotControl control = new BotControl();
            await _context.Bots.AddAsync(new Database.Models.Bot { 
                BotId = control.StartBot(model.Token), 
                isWorking = true,
                Title = model.Name, 
                Token = model.Token
            });
            await _context.SaveChangesAsync(); 
            return RedirectToAction("List"); 
        }

        public async Task<IActionResult> StartBot(int id)
        {
            if(id > 0)
            {
                
                var bot = await _context.Bots.FirstOrDefaultAsync(x => x.Id == id); 
                if(bot != null && !bot.isWorking)
                {
                    BotControl control = new BotControl();
                    control.StartBot(bot.Token);

                    bot.isWorking = true;
                    _context.Bots.Update(bot);
                    await _context.SaveChangesAsync(); 
                }
            }

            return RedirectToAction("List");
        }

        //public async Task<IActionResult> StopBot(int id)
        //{
        //    if(id > 0)
        //    {
        //        var bot = await _context.Bots.FirstOrDefaultAsync(x => x.Id == id);
        //        if (bot != null && bot.isWorking)
        //        {
        //            BotControl control = new BotControl();
        //            control.StopBot(bot.Token);

        //            bot.isWorking = true;
        //            _context.Bots.Update(bot);
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //}
    }
}