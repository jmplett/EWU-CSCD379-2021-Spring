using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DbContext = SecretSanta.Data.DbContext;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext()
            : base(new DbContextOptionsBuilder<DbContext>().UseSqlite("Data Source=main.db").Options)
        { }

        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Gift> Gifts => Set<Gift>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<Group>().HasAlternateKey(item => new { item.Name } ); 
            modelBuilder.Entity<User>().HasAlternateKey(item => new { item.FirstName, item.LastName } ); 
            modelBuilder.Entity<Gift>().HasAlternateKey(item => new { item.Title });

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Assignments)
                .WithMany(a => a.groups);
        }
    }
}