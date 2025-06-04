
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
        public DbSet<Role> Roles { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=ANAMIKA\\SQLSERVER;database=newUserDatabase;integrated security=true;TrustServerCertificate=true");;
        //}

        // Fluent Api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    RoleId = 1,
                    RoleName = "Admin"
                },
                new Role()
                {
                    RoleId = 2,
                    RoleName = "Manager"
                },
                new Role()
                {
                    RoleId = 3,
                    RoleName = "User"
                });
            modelBuilder.Entity<User>()
    .HasData(new User
    {
      Id=1,
        UserName = "user1",
        Password = "user1",
        FirstName = "user1",
        LastName = "user1",
        Address = "dl",
        RoleId =1
    },
    new User
    {
       Id=2,
        UserName = "user2",
        Password = "user2",
        FirstName="user2",
        LastName="user2",
        Address="dl",
        RoleId=2
    },
    new User
    {
        Id=3,
          UserName = "user3",
        Password = "user3",
        RoleId=3,
        FirstName = "user3",
        LastName = "user3",
        Address = "dl"
    }
    );

        }
    }
}
