using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Entities
{
    public  class User
{
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Following { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
