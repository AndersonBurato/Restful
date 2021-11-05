using Microsoft.EntityFrameworkCore;
using Restful.Repository.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful.Repository
{
    internal static class DataBaseHelper
    {
        public static void ModelCreate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(k => k.Id);

            modelBuilder.Entity<Person>()
                .HasMany(c => c.Addresses)
                .WithOne(e => e.Person)
                .HasForeignKey(f => f.PersonId);

            modelBuilder.Entity<Address>()
                .HasKey(r => new { r.Id, r.PersonId });

            modelBuilder.Entity<Address>()
                .HasOne(o => o.Person)
                .WithMany(o => o.Addresses)
                .HasForeignKey(o => o.PersonId);
        }

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(new Person[] { 
                new Person(){ 
                     Id = 1,
                      FirstName = "Teste",
                      LastName = "Pessoa 1"
                }
            });
        }
    }
}
