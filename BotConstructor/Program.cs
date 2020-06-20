using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotConstructor.Bot;
using BotConstructor.Database.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BotConstructor
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            StartBots(); 
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void StartBots()
        {
            using(var context = new ApplicationContext())
            {
                foreach (var bot in context.Bots.Where(x => x.isWorking).ToList())
                {
                    BotControl control = new BotControl();
                    control.StartBot(bot.Token);
                }
            }       
        }
    }
}
