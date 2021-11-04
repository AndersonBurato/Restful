using Microsoft.EntityFrameworkCore;
using Restful.Repository.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Restful.Repository
{
    public class DataBase : DbContext
    {
        public DataBase([NotNullAttribute] DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ModelCreate();
            modelBuilder.SeedData();
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
