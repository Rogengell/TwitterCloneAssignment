using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace EFramework.Data
{
    public class AGWDbContext : DbContext
    {
        public AGWDbContext(DbContextOptions<AGWDbContext> options) : base(options) { }

        public DbSet<UsersTable>? usersTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}