using AspNetCoreHero.Results;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.IRepository;
using SocialMedia.Core.DTO;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialMediaDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(SocialMediaDbContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        public  async Task<Result<User>> AddUserAsync(User user)
        {
            try
            {
                var newUser = _mapper.Map<User>(user);
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return Result<User>.Success(newUser);
            }
            catch (Exception ex)
            {

                return Result<User>.Fail(ex.Message);
            }
        }

        public async Task FollowUserAsync(Guid followerId, Guid followeeId)
        {
            var follow = new Follow
            {
                FollowerId = followerId,
                FolloweeId = followeeId
            };

            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserFollowAsync(Guid userId)
        {
            var user= await _context.Users.Include(u => u.Following)
                                       .FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<Result<User>> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _context.Users
                                               .FirstOrDefaultAsync(u => u.Email==email);
                return await Result<User>.SuccessAsync(user);
            }
            catch (Exception ex)
            {

                return await Result<User>.FailAsync(ex.Message);
            }
        }

        public async  Task<Result<User>> GetUserByIdAsync(Guid userId)
        {

            try
            {
                var user = await _context.Users.FindAsync(userId);
                return await Result<User>.SuccessAsync(user);
            }
            catch (Exception ex)
            {

                return await Result<User>.FailAsync(ex.Message);
            }
        }
    }
}
