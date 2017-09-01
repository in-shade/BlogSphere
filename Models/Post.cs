using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSphere.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        

        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        public DateTime PostDate { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
