using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_api.Models.Dtos
{
    public class StudentModelDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public string fatherName { get; set; }

        public string address { get; set; }

        public string collegeName { get; set; }

        public int year { get; set; }

        public string department { get; set; }

        public int yearOfJoin { get; set; }

        public bool firstGraduate { get; set; }

        public int age { get; set; }

        public string mobile { get; set; }

        public string email { get; set; }

        public string degree { get; set; }

        public string rollNo { get; set; }

        public string gender { get; set; }

        public DateTime createdDate { get; set; }

        public DateTime updatedDate { get; set; }
    }
}
