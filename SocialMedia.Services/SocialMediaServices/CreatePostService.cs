using AspNetCoreHero.Results;
using SocialMedia.Application.IRepository;
using SocialMedia.Application.Repository;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services.SocialMediaServices
{
    public class CreatePostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public CreatePostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        public async Task<Result<Post>> CreatePostAsync(Guid userId, string content)
        {
            if (string.IsNullOrEmpty(content) || content.Length > 140)
                throw new ArgumentException("Content is invalid");
            var post = new Post
            {
              
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow,
                Likes = 0
            };
            
            return await _postRepository.AddPostAsync(post);

        }

        public async Task<IEnumerable<Post>> GetPostAsync(Guid userId, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Invalid pagination parameters");

            var user = await _userRepository.GetUserFollowAsync(userId);
            var followingIds = user.Following.Select(f => f.FolloweeId).ToList();
            followingIds.Add(userId); // include the user's own posts

            var posts = await _postRepository.GetPostsForUserAsync(userId, pageNumber, pageSize);
            return posts.OrderByDescending(p => p.Likes);
        }
    }
}
