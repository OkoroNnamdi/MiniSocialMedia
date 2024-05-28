using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Entities
{
    public  class Follow
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FollowerId { get; set; }

        [ForeignKey(nameof(FollowerId))]
        public User Follower { get; set; }

        public Guid FolloweeId { get; set; }

        [ForeignKey(nameof(FolloweeId))]
        public User Followee { get; set; }
    }
}
