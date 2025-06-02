
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchApi.Models;

namespace BatchApi.Context
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options){  }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<User> Users { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=ANAMIKA\\SQLSERVER;database=newUserDatabase;integrated security=true;TrustServerCertificate=true");;
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
    .HasData(new User
    {
      Id=1,
        UserName = "user1",
        Password = "user1"
    },
    new User
    {
       Id=2,
        UserName = "user2",
        Password = "user2"
    },
    new User
    {
        Id=3,
          UserName = "user3",
        Password = "user3"
    }
    );

        }
    }
}
