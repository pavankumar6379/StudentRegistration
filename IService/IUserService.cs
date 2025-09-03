using StudentRegistration.Models.Login;

namespace StudentRegistration.IService
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);  // Authenticate user by username and password
        Task<bool> RegisterAsync(string username, string password, string role);  // Register a new user asynchronously
        Task<User> GetUserByUsernameAsync(string username);  // Get user by username asynchronously
        Task<User> GetByIdAsync(int id);  // Get user by ID asynchronously
    }
}
