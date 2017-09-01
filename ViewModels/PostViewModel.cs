using BlogSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSphere.ViewModels
{
    public class PostViewModel
    {
        public string Content { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public DateTime PostDate { get; set; }

        public ICollection<PostTag> PostTags { get; set; }        
    }
}
