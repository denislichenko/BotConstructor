using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotConstructor.Database.Models;
using BotConstructor.Database.Models.QuizModels;
using BotConstructor.Web.Models.Quiz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BotConstructor.Web.Controllers
{
    public class QuizController : Controller
    {
        private ApplicationContext _context;
        public QuizController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List(int botId)
        {
            if(botId > 0)
            {
                var quizzes = await _context.Quizzes.Where(x => x.BotId == botId).ToListAsync();
                var model = new QuizListViewModel
                {
                    BotId = botId,
                    Quizzes = quizzes
                };

                return View(model); 
            }
            return Redirect(Url.Action("List", "Bot")); 
        }

        public IActionResult Create(int botId)
        {
            if (botId > 0) return View(new QuizEditViewModel { BotId = botId }); 
            return RedirectToAction("List"); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuizEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _context.Quizzes.AddAsync(new Quiz
                {
                    BotId = model.BotId, 
                    Name = model.Name
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
                var quiz = await _context.Quizzes.Include(x => x.QuizSteps).FirstOrDefaultAsync(x => x.Id == id); 
                if(quiz != null)
                {
                    var model = new QuizEditViewModel
                    {
                        Id = quiz.Id,
                        BotId = quiz.BotId,
                        Name = quiz.Name,
                        Steps = quiz.QuizSteps.Select(x => new QuizStepViewModel
                        {
                            Id = x.Id, 
                            QuizId = x.QuizId, 
                            Name = x.Name, 
                            Text = x.Text
                        }).ToList()
                    };

                    return View(model); 
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(QuizEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var quiz = await _context.Quizzes.FirstOrDefaultAsync(x => x.Id == model.Id); 
                if(quiz != null)
                {
                    quiz.Name = model.Name; 
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("List", new { botId = model.BotId }); 
            }

            return View(model); 
        }

        public async Task<IActionResult> Delete(int id)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(x => x.Id == id); 
            if(quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync(); 
            }

            return RedirectToAction("List", new { botId = quiz.BotId }); 
        }

        #region Steps

        public IActionResult CreateStep(int quizId)
        {
            return View(new QuizStepViewModel { QuizId = quizId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateStep(QuizStepViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _context.QuizSteps.AddAsync(new QuizStep
                {
                    Name = model.Name, 
                    Text = model.Text,
                    QuizId = model.QuizId
                });

                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = model.QuizId }); 
            }

            return View(model); 
        }

        public async Task<IActionResult> EditStep(int id)
        {
            var step = await _context.QuizSteps.FirstOrDefaultAsync(x => x.Id == id);
            if(step != null)
            {
                return View(new QuizStepViewModel
                {
                    Id = step.Id,
                    QuizId = step.QuizId,
                    Name = step.Name,
                    Text = step.Text
                }); 
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> EditStep(QuizStepViewModel model)
        {
            if(ModelState.IsValid)
            {
                var step = await _context.QuizSteps.FirstOrDefaultAsync(x => x.Id == model.Id); 
                if(step != null)
                {
                    step.QuizId = model.QuizId;
                    step.Name = model.Name;
                    step.Text = model.Text;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Edit", new { id = model.QuizId }); 
                }
            }
            return View(model); 
        }

        public async Task<IActionResult> DeleteStep(int id)
        {
            var step = await _context.QuizSteps.FirstOrDefaultAsync(x => x.Id == id); 
            if(step != null)
            {
                _context.QuizSteps.Remove(step);
                await _context.SaveChangesAsync(); 
            }
            return RedirectToAction("Edit", new { id = step.QuizId }); 
        }

        #endregion
    }
}