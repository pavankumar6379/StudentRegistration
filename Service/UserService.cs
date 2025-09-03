using Microsoft.AspNetCore.Identity;
using StudentRegistration.IRepository.Login;
using StudentRegistration.IService;
using StudentRegistration.Models.Login;
using System.Threading.Tasks;

namespace StudentRegistration.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        // Authenticate user by comparing plain-text password with hashed password
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return null;

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (verificationResult != PasswordVerificationResult.Success)
                return null;

            return user;
        }

        // Register a new user
        public async Task<bool> RegisterAsync(string username, string password, string role)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser != null)
                return false;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Role = role,
                //CreatedOn = DateTime.UtcNow,
               /// ModifiedOn = DateTime.UtcNow,
               // IsActive = true
            };

            await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
    }
}
