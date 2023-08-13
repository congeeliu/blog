using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext (DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<webapi.Models.Blog> Blog { get; set; } = default!;
    }
}
