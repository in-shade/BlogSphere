using BlogSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSphere.Repositories
{
    public interface ITagRepository
    {
        ICollection<Tag> GetAll();
        Tag GetById(int id);
        Tag GetByName(string name);
        bool Insert(Tag post);
    }
}
