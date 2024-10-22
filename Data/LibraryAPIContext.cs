using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryAPI.Data
{
    public class LibraryAPIContext : DbContext
    {
        public LibraryAPIContext(DbContextOptions<LibraryAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;  // Add User DbSet here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // If you want to store enum as string
            modelBuilder.Entity<User>()
                .Property(u => u.UserRole);
                // .HasConversion<string>();  // Enum as string instead of int
        }
    }
}


