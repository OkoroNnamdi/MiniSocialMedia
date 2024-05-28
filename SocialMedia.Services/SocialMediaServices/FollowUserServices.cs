using AspNetCoreHero.Results;
using SocialMedia.Application.IRepository;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services.SocialMediaServices
{
    public  class FollowUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IFollowUserRepository _followUserRepository;

        public FollowUserServices(IUserRepository userRepository, IFollowUserRepository followUserRepository)
        {
            _userRepository = userRepository;
            _followUserRepository = followUserRepository;
        }

        public async Task<Result<string>> FollowUserAsync(Guid followerId, Guid followeeId)
        {
            try
            {
                if (followerId == followeeId)
                    throw new ArgumentException("Cannot follow yourself");
                if (followerId == Guid.Empty || followeeId == Guid.Empty)
                    throw new ArgumentException("Invalid Followership credential");
                var follower = await _userRepository.GetUserByIdAsync(followerId);
                if (follower == null)
                    throw new ArgumentException("Follower user not found.");

                var followee = await _userRepository.GetUserByIdAsync(followeeId);
                if (followee == null)
                    throw new ArgumentException("Followee user not found.");

                if (await _followUserRepository.IsFollowingAsync(followerId, followeeId))
                    throw new ArgumentException("Already following this user.");

                await _followUserRepository.FollowUserAsync(followerId, followeeId);
                return await Result<string>.SuccessAsync("Successful Follow");
            }
            catch (Exception ex )
            {

                return await Result<string>.FailAsync(ex.Message);
            }

           
        }
    }
}
