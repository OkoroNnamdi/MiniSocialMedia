using AutoMapper;
using SocialMedia.Core.DTO;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SocialMedia.Core.Utilities
{
    public  class MapInitializer:Profile
    {
        public Mapper regMapper { get; set; }
        public MapInitializer()
        {
            CreateMap<Post, PostDto>();
            CreateMap<CreatePostRequest, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<User, UserDto>();
            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Followers, opt => opt.Ignore())
                .ForMember(dest => dest.Following, opt => opt.Ignore());
                
                

        }
    }
}
