using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Entities
{
    public class FollowUserRequest
    {
        public Guid FollowerId { get; set; }
        public Guid FolloweeId { get; set; }
    }
}
