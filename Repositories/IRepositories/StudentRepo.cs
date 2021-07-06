using student_management_api.Data;
using student_management_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_api.Repositories.IRepositories
{
    public class StudentRepo : IStudentRepo
    {
        private readonly ApplicationDbContext db;

        public StudentRepo(ApplicationDbContext _db)
        {
            db = _db;
        }
        public bool CreateStudentAsync(StudentModel model)
        {
            db.StudentsModel
        }

        public bool ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public StudentModel GetStudentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<StudentModel> GetStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public bool UpdateStudentAsync(StudentModel model)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
