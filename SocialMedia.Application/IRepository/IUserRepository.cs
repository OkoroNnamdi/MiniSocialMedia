using AspNetCoreHero.Results;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserFollowAsync(Guid userId);
        Task<Result<User> >AddUserAsync(User user);
        Task<Result<User>> GetUserByIdAsync(Guid userId);
        Task<Result<User>> GetUserByEmailAsync(string email);
        Task FollowUserAsync(Guid followerId, Guid followeeId);
    }
}
