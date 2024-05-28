using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Api.Extensions
{
    public static  class DBRegistryExtension
    {
        public static void AddDbContextAndConfigurations(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            services.AddDbContextPool<SocialMediaDbContext>(options =>
            {
                string connStr;

                if (env.IsDevelopment())
                {
                    // connStr = GetRenderConnectionString();
                    // options.UseNpgsql(connStr);
                    connStr = config.GetConnectionString("ConnStr");
                    options.UseSqlServer(connStr);
                }
                else
                {
                    //connStr = $"postgres://queenfisher_user:XdvZANh0qUqLUXvxcUElLGoVkAqHZfNi@dpg-cgb289g2qv267uepue2g-a.oregon-postgres.render.com/queenfisher";
                    //options.UseNpgsql(connStr);
                    connStr = config.GetConnectionString("ProdDb");
                   // options.UseNpgsql(connStr);
                }
            });
        }
    }
}

