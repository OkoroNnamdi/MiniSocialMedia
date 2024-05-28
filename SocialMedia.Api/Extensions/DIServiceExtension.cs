using SocialMedia.Application.IRepository;
using SocialMedia.Application.Repository;
using SocialMedia.Services.SocialMediaServices;

namespace SocialMedia.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {


            // Add Repository Injections Here


            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFollowUserRepository, FollowUserRepository>();    



            // Add Model Services Injection Here

            services.AddScoped<CreatePostService>();
            services.AddScoped<FollowUserServices>();
            services.AddScoped<CreateUserService>();



            // Add Fluent Validator Injections Here

        }
    }
}
