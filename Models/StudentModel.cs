
using System;
using System.ComponentModel.DataAnnotations;

namespace student_management_api.Models
{
    public class StudentModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string fatherName { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public string collegeName { get; set; }

        [Required]
        public int year { get; set; }

        [Required]
        public string department { get; set; }

        [Required]
        public int yearOfJoin { get; set; }

        public bool firstGraduate { get; set; }

        [Required]
        public int age { get; set; }

        [Required]
        [StringLength(10)]
        public string mobile { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string degree { get; set; }

        [Required]
        public string rollNo { get; set; }

        [Required]
        public string gender { get; set; }

        public DateTime createdDate { get; set; }

        public DateTime updatedDate { get; set; }

    }
}