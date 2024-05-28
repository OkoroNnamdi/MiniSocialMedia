using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Entities
{
    public class CreatePostRequest
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
