using AncientAura.Core.Entities;
using AncientAura.Core.Entities.Community;
using AncientAura.Core.Entities.GameEntities;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Entities.WishLists;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data.Contexts
{
    public class AncientAuraDbContext : IdentityDbContext<AppUser>
    {
        public AncientAuraDbContext(DbContextOptions<AncientAuraDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }


        public DbSet<AncientSites> AncientSites { get; set; }
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Documentries> Documentries { get; set; }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<ImageURLs> ImageURLs { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Links> Links { get; set; }

        //Community DbSets
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<React> Reacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Blocked> BlockedUsers { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        //Game DbSets
        public DbSet<Game> Games { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<Practice> Practices { get; set; }

        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Items> Items { get; set; }
    }
}
