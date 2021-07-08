using student_management_api.Models;

namespace student_management_api.Repositories.IRepositories
{
    public interface IUserRepo
    {
        public bool IsUniqueUser(string userName);

        public UserModel Authenticate(string userName, string password);

        public UserModel Register(string userName, string password);
    }
}
