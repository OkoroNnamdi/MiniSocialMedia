using AspNetCoreHero.Results;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.IRepository;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Repository
{
    public class FollowUserRepository : IFollowUserRepository
    {
        private readonly SocialMediaDbContext _context;
        public FollowUserRepository(SocialMediaDbContext context)
        { 
            _context = context;
        }
        public async Task<Result<Follow>> FollowUserAsync(Guid followerId, Guid followeeId)
        {
            try
            {
                var userFollow = new Follow
                {
                    FollowerId = followerId,
                    FolloweeId = followeeId
                };

                _context.Follows.Add(userFollow);
                await _context.SaveChangesAsync();
                return await Result<Follow>.SuccessAsync(userFollow);
            }
            catch (Exception ex)
            {

                return await Result<Follow>.FailAsync(ex.Message);
            }
        }

        public async Task<bool> IsFollowingAsync(Guid followerId, Guid followeeId)
        {
            try
            {

                 return await _context.Follows
                    .AnyAsync(uf => uf.FollowerId == followerId && uf.FolloweeId == followeeId);
                
            }
            catch (Exception )
            {

                throw;
            }
           
        }
    }
}
