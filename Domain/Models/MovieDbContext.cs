using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movApp.Models
{
    public class MovieDbContext : IdentityDbContext<User>
    {
        public DbSet<Film> Films { get; set; }


        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }
        //public DbSet<User> Users { get; set; }
        public MovieDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=movie2;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().HasData(
            //    new User[]
            //    {
            //        new User {id=1Name="Барс", Password= null}
            //    });;


        }
    }
}
