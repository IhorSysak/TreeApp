using Microsoft.EntityFrameworkCore;
using TreeApplication.Models;

namespace TreeApplication.Context
{
    public class TreeContext : DbContext
    {
        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<MJournal> MJournals { get; set; }

        public TreeContext() : base() { }
        public TreeContext(DbContextOptions<TreeContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TreeNode>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TreeNode>()
                .HasMany(c => c.Children)
                .WithOne(p => p.Parent)
                .HasForeignKey(k => k.ParentId);
        }
    }
}
