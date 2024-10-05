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
        public LibraryAPIContext (DbContextOptions<LibraryAPIContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryAPI.Models.Book> Book { get; set; } = default!;
    }
}
