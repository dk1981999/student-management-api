using student_management_api.Models;
using System.Collections.Generic;

namespace student_management_api.Repositories.IRepositories
{
    public interface IStudentRepo
    {
        public ICollection<StudentModel> GetStudentsAsync();

        public bool CreateStudentAsync(StudentModel model);

        public StudentModel GetStudentByIdAsync(int id);

        public bool UpdateStudentAsync(StudentModel model);

        public bool DeleteStudentAsync(StudentModel model);

        public bool ExistAsync(int id);

        public bool Save();
    }
}
