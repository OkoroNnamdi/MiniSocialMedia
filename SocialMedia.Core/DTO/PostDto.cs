using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.DTO
{
    public  class PostDto
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
