using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace zadanie1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserDbContext> Users  { get; set; }
        public DbSet<PaymentDbContext> Payments { get; set; }
        public DbSet<CategoryDbContext> Categories { get; set; }
        public string DbPath { get; private set; }
        public ApplicationContext()
        {

            var folder = Environment.CurrentDirectory;
           
            DbPath = $"{folder}{System.IO.Path.DirectorySeparatorChar} paymentcounter.db";
            
            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {DbPath} ");
        }
    }
}