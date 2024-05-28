using AspNetCoreHero.Results;
using SocialMedia.Application.IRepository;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services.SocialMediaServices
{
    public  class CreateUserService
    {

        private readonly IUserRepository _userRepository;

        public CreateUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> ExecuteAsync(string username, string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    throw new ArgumentException("Invalid user details");

                if (await _userRepository.GetUserByEmailAsync(email) == null)
                    throw new ArgumentException("Email is already in use");

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = username,
                    Email = email,
                    PasswordHash = HashPassword(password),
                    Followers = new List<Follow>(),
                    Following = new List<Follow>()
                };

                return await _userRepository.AddUserAsync(user);
            }
            catch (Exception ex)
            {

                return await Result<User>.FailAsync(ex.Message);
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}

