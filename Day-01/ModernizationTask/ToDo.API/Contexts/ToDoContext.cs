using Microsoft.EntityFrameworkCore;
using ToDo.API.Models.DBModels;

namespace ToDo.API.Contexts
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Todo>()
               .HasOne(t => t.User)
               .WithMany(u => u.Todos)
               .HasForeignKey(t => t.UserId);
        }
    }
}
