using BlogSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSphere.Repositories
{
    public interface IPostRepository
    {
        ICollection<Post> GetAll();
        ICollection<Post> GetAllByUserId(int id);
        Post GetById(int id);

        bool Insert(Post post);
        bool InsertTag(int id, Tag tag);
        bool Update(Post post);

        bool Delete(int id);
    }
}
