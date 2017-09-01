using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSphere.Models;
using BlogSphere.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogSphere.Repositories
{
    public class PostRepository : IPostRepository
    {
        private BlogSphereContext _context;

        public PostRepository(BlogSphereContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            var post = _context.Posts.Where(p => p.Id == id).FirstOrDefault();
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public ICollection<Post> GetAll()
        {
            var posts = _context.Posts;

            foreach (Post p in posts)
            {
                _context.Users.Where(u => u.Id == p.UserId).Load();
            }

            return _context.Posts.ToList();
        }

        public Post GetById(int id)
        {
            var post = _context.Posts.Where(p => p.Id == id).Include(p => p.User);

            return post.FirstOrDefault();
        }

        public ICollection<Post> GetAllByUserId(int id)
        {
            var posts = _context.Posts.Where(p => p.UserId == id)
                .Include(p => p.User);
            //TODO: include tags

            foreach (Post p in posts)
            {
                _context.Users.Where(u => u.Id == p.UserId).Load();
            }

            return posts.ToList();
        }

        public bool Insert(Post post)
        {
            if(_context.Users.Where(u => u.Id == post.UserId).Any())
            {
                _context.Posts.Add(post);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool InsertTag(int id, Tag tag)
        {
            var tagFound = _context.Tags.Where(t => t.Name.Equals(tag.Name, StringComparison.CurrentCultureIgnoreCase));
            if ( tagFound.Any() )
            {
                _context.PostTags.Add(new PostTag
                {
                    PostId = id,
                    TagId = tagFound.FirstOrDefault().Id
                });
                _context.SaveChanges();

            return true;
            }

            return false;
        }

        public bool Update(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
