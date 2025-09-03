using StudentRegistration.Models.Login;

namespace StudentRegistration.IRepository.Login
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);  // Fetch user by username asynchronously
        Task<User> GetUserByIdAsync(int id);  // Fetch user by ID asynchronously
        Task AddUserAsync(User user);  // Save new user asynchronously
    }
}
