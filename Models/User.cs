using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSphere.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [MaxLength(100)]
        public string About { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
