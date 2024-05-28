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
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaDbContext _dbContext;
        private readonly IMapper _mapper;
        public PostRepository(SocialMediaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Result<Post>> AddPostAsync(Post postDto)
        {

            try
            {
                var newPost =_mapper .Map<Post>(postDto);
                _dbContext.Posts.Add(newPost);
                await _dbContext.SaveChangesAsync();
                return Result<Post>.Success(newPost);
            }
            catch (Exception ex)
            {

                return Result<Post>.Fail(ex.Message);
            }
            }


        public async Task<IEnumerable<Post>> GetPostsForUserAsync(Guid userId, int pageNumber, int pageSize)
        {
            try
            {
                var user = await _dbContext.Users.Include(u => u.Following)
                                                  .ThenInclude(f => f.Followee)
                                                  .FirstOrDefaultAsync(u => u.Id == userId);

                var followingIds = user.Following.Select(f => f.FolloweeId).ToList();
                followingIds.Add(userId); // Include user's own posts

                var userPost = _dbContext.Posts
                                     .Where(p => followingIds.Contains(p.UserId))
                                     .OrderByDescending(p => p.Likes)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

                return await userPost;
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }

       
    }
    }

