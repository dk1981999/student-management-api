using System.ComponentModel.DataAnnotations;

namespace student_management_api.Models
{
    public class AuthenticationModel
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }
    }
}
