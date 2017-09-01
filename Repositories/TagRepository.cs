using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSphere.Models;
using BlogSphere.Data;

namespace BlogSphere.Repositories
{
    public class TagRepository : ITagRepository
    {
        private BlogSphereContext _context;

        public TagRepository(BlogSphereContext context)
        {
            _context = context;
        }

        public ICollection<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            return _context.Tags.Where(t => t.Id == id).FirstOrDefault();
        }

        public Tag GetByName(string name)
        {
            return _context.Tags.Where(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public bool Insert(Tag tag)
        {
            if ( _context.Tags.Where(t => t.Name.ToLowerInvariant().Equals(tag.Name) ).Any() )
            {
                return false;
            }
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return true;
        }
    }
}
