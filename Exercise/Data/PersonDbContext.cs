using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using Exercise.Models;

namespace Exercise.Data
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> People => Set<Person>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=people.db");
        }
    }
}
