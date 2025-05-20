using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyPortfolio.Models
{
  
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Comment> Comments { get; set; }
        }
    
}
