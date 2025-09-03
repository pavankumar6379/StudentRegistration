using Microsoft.EntityFrameworkCore;
using StudentRegistration.IRepository;
using StudentRegistration.Models;

namespace StudentRegistration.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor - takes ApplicationDbContext as a parameter
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //  method to get all students from the database
        public async Task<IEnumerable<StudentModel>> GetAllStudentsAsync()
        {
            return await _context.Student.ToListAsync();
        }
        
        public async Task<StudentModel> GetStudentByIdAsync(int id)
        {
            return await _context.Student.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddStudentAsync(StudentModel student)
        {
            await _context.Student.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task EditStudentAsync(StudentModel student)
        {
            _context.Student.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}