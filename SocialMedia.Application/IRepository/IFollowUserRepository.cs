using AspNetCoreHero.Results;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.IRepository
{
    public interface IFollowUserRepository
    {
        Task<Result<Follow>> FollowUserAsync(Guid followerId, Guid followeeId);
        Task<bool> IsFollowingAsync(Guid followerId, Guid followeeId);
         
    }
}
