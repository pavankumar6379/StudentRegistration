using StudentRegistration.Models;

namespace StudentRegistration.IRepository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentModel>> GetAllStudentsAsync();       
        Task<StudentModel> GetStudentByIdAsync(int id);
        Task AddStudentAsync(StudentModel student);
        Task EditStudentAsync(StudentModel student);
        Task DeleteStudentAsync(int id);
    }
}
