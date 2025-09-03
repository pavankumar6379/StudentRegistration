using Microsoft.EntityFrameworkCore;
using StudentRegistration.IRepository.Login;
using StudentRegistration.Models;
using StudentRegistration.Models.Login;

namespace StudentRegistration.Repository.Login
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get user by username
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);  // Fetch user by username asynchronously
        }

        // Get user by ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);  // Fetch user by ID asynchronously
        }

        // Add new user to the database
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);  // Add user to the DbContext
            await _context.SaveChangesAsync();  // Save changes to the database
        }
    }
}