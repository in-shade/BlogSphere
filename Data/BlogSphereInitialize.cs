using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSphere.Models;

namespace BlogSphere.Data
{
    public class BlogSphereInitialize
    {
        private BlogSphereContext _context;

        public BlogSphereInitialize(BlogSphereContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            if (_context.Users.Any())
            {
                return;
            }
            
            var user1 = new User
            {
                Nickname = "insHade",
                Email = "tymon.baran@gmail.com",
                About = "Creator of BlogSphere @2017",
                Posts = new List<Post>()
            };

            var user2 = new User
            {
                Nickname = "TestUsr",
                Email = "tstusr@gmail.com",
                About = "Test user",
                Posts = new List<Post>()
            };

            user1.Posts.Add(
                 new Post()
                {
                    PostDate = DateTime.UtcNow,
                    Content = "Pierwszy post.",
                    PostTags = new List<PostTag>(),
                    UserId = user1.Id,
                    User = user1
                });

            user1.Posts.Add(
                new Post()
                {
                    PostDate = DateTime.UtcNow,
                    Content = "Drugi post!",
                    PostTags = new List<PostTag>(),
                    UserId = user1.Id,
                    User = user1
                });

            user2.Posts.Add(
                new Post()
                {
                    PostDate = DateTime.UtcNow,
                    Content = "The one to test them all.",
                    UserId = user2.Id,
                    User = user2
                });

            _context.Users.Add(user1);
            _context.Users.Add(user2);
            _context.Posts.AddRange(user1.Posts);
            _context.Posts.AddRange(user2.Posts);

            _context.SaveChanges();
           
        }
    }
}
