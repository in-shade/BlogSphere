using BlogSphere.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSphere.Data
{
    public class BlogSphereContext : DbContext
    {

        public BlogSphereContext(DbContextOptions<BlogSphereContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    //string connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
        //    optionsBuilder.UseSqlServer(Configuration.);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTag>()
                        .HasKey(pt => new { pt.PostId, pt.TagId });

            modelBuilder.Entity<PostTag>()
                        .HasOne(pt => pt.Post)
                        .WithMany(t => t.PostTags)
                        .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
                        .HasOne(pt => pt.Tag)
                        .WithMany(p => p.PostTags)
                        .HasForeignKey(pt => pt.TagId);
        }
    }
}
