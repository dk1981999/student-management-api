using student_management_api.Data;
using student_management_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace student_management_api.Repositories.IRepositories
{
    public class StudentRepo : IStudentRepo
    {
        private readonly ApplicationDbContext _db;

        public StudentRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateStudentAsync(StudentModel model)
        {
            _db.Students.Add(model);
            return Save();
        }

        public bool ExistAsync(int id)
        {
            return _db.Students.Any(a => a.id == id);
        }

        public StudentModel GetStudentByIdAsync(int id)
        {
            return _db.Students.FirstOrDefault(a => a.id == id);
        }

        public ICollection<StudentModel> GetStudentsAsync()
        {
            return _db.Students.OrderBy(a => a.id).ToList();
        }

        public bool UpdateStudentAsync(StudentModel model)
        {
            _db.Students.Update(model);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool DeleteStudentAsync(StudentModel model)
        {
            _db.Students.Remove(model);
            return Save();
        }
    }
}
