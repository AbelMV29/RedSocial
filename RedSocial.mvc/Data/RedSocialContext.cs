using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedSocial.mvc.DataModels;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RedSocial.mvc.Data
{
    public class RedSocialContext : IdentityDbContext
    {
        public RedSocialContext(DbContextOptions<RedSocialContext> options) : base(options) { }
        public DbSet<ProfileUser> ProfileUser { get; set; } 
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Response> Response { get; set; }
        public DbSet<ResponsePostProfile> ResponsePostProfile { get; set; }
        public DbSet<CommentPostProfile> CommentPostProfile { get; set; }
        //public DbSet<Request> Request { get; set; }
        public DbSet<PostCategory> PostCategory { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();

            builder.Entity<Response>()
                .HasMany(r => r.ResponseChilds)
                .WithOne(r => r.ResponseParent)
                .HasForeignKey(r => r.ResponseParentId);

            builder.Entity<PostCategory>()
                .HasKey(p => new { p.PostId, p.CategoryId });

            builder.Entity<CommentPostProfile>()
                .HasKey(c => new { c.CommentId, c.PostId });

            builder.Entity<ResponsePostProfile>()
                .HasKey(r => new { r.ResponseId, r.ProfileUserId });

            //builder.Entity<Profile>

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.IsAssignableFrom(typeof(ProfileUser)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(Comment)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(Post)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(Response)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(ResponsePostProfile)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(CommentPostProfile)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(PostCategory)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(Category))
                    // Otros DbSet que quieras incluir
                    // entityType.ClrType.IsAssignableFrom(typeof(OtraEntidad))
                    )
                {
                    foreach (var relationship in entityType.GetForeignKeys())
                    {
                        relationship.DeleteBehavior = DeleteBehavior.Restrict;
                    }
                }
            }

            //builder.Entity<Request>()
            //    .HasKey(r => new { r.ProfileUserId, r.FriendId });

            //builder.Entity<Request>()
            //    .HasOne(r=>r.ProfileUser)
            //    .WithMany(p=>p.Requests)
            //    .HasForeignKey(r=>r.ProfileUserId);

            //builder.Entity<Request>()
            //   .HasOne(r => r.FriendUser)
            //   .WithMany(p => p.Requests)
            //   .HasForeignKey(r => r.FriendId);
        }

    }
}
