using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogSphere.Models;

namespace BlogSphere.Repositories
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        User GetByName(string name);
        User GetById(int id);

        bool Insert(User user);
        bool Update(User user);

        bool Delete(int id);
    }
}
