
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
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=ANAMIKA\\SQLSERVER;database=newUserDatabase;integrated security=true;TrustServerCertificate=true");;
        //}
    }
}
