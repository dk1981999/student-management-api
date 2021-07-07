using student_management_api.Data;
using student_management_api.Models;
using System.Collections.Generic;
using System.Linq;

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
            db.Students.Add(model);
            return Save();
        }

        public bool ExistAsync(int id)
        {
            return db.Students.Any(a => a.id == id);
        }

        public StudentModel GetStudentByIdAsync(int id)
        {
            return db.Students.FirstOrDefault(a => a.id == id);
        }

        public ICollection<StudentModel> GetStudentsAsync()
        {
            return db.Students.OrderBy(a => a.id).ToList();
        }

        public bool UpdateStudentAsync(StudentModel model)
        {
            db.Students.Update(model);
            return Save();
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0 ? true : false;
        }

        public bool DeleteStudentAsync(StudentModel model)
        {
            db.Students.Remove(model);
            return Save();
        }
    }
}
