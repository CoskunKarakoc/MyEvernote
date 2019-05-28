using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer.EntityFramwork
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // FluentAPI=>ilişkisel tablodan veri silme işleminin bir diğer yöntemi
            modelBuilder.Entity<Note>()
                .HasMany(x => x.Comments)
                .WithRequired(c => c.Note)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Note>()
                .HasMany(x=>x.Likes)
                .WithRequired(c=>c.Note)
                .WillCascadeOnDelete(true);
            
        }

        public DbSet<EvernoteUser> EvernoteUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likeds { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
