using AspNetCoreHero.Results;
using SocialMedia.Core.DTO;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.IRepository
{
    public interface IPostRepository
    {
        Task<Result<Post>> AddPostAsync(Post postDto);
        Task<IEnumerable<Post>> GetPostsForUserAsync(Guid userId, int pageNumber, int pageSize);
    }
}
