﻿using BotConstructor.Database.Models.Identity;
using BotConstructor.Database.Models.QuizModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotConstructor.Database.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Bot> Bots { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizStep> QuizSteps { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }

        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=constructor;Trusted_Connection=True;");
        }
    }
}
