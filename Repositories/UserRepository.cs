using BlogSphere.Data;
using BlogSphere.Models;
using BlogSphere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSphere.Repositories
{
    public class UserRepository : IUserRepository
    {
        private BlogSphereContext _context;

        public UserRepository(BlogSphereContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if(user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public ICollection<User> GetAll()
        {
            //TODO: includePosts
            return _context.Users.ToList();
        }

        public User GetByName(string name)
        {
            return _context.Users.Where(u => u.Nickname.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public User GetById(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public bool Insert(User user)
        {
            if( _context.Users.Where( u => u.Nickname.Equals(user.Nickname, StringComparison.CurrentCultureIgnoreCase) ).Any() )
            {
                return false;
            }
            
            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
