using System.ComponentModel.DataAnnotations.Schema;

namespace student_management_api.Models
{
    public class UserModel
    {
        public int id { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        [NotMapped]
        public string token { get; set; }
    }
}
