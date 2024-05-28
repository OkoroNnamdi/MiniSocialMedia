using SocialMedia.Api.Extensions;

namespace SocialMedia.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            var services = builder.Services;
            var env = builder.Environment;

            // Add services to the container.
            // Add services to the container.
            builder.Services.AddHttpClient();
            // Configure AutoMapper
            builder.Services.ConfigureAutoMappers();
            //For Entity Framework

            builder.Services.AddDbContextAndConfigurations(env, config);
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwagger();

            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            
            // Register Dependency Injection Service Extension
            builder.Services.AddDependencyInjection();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
