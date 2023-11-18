using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeOne.Models
{
    // Teacher class for object creation and data access
    public class Teacher
    {
        // Defining the getters and setters to work with the database data
        public int TeacherId { get; set; }
        public string TeacherFname { get; set; }
        public string TeacherLname { get; set; }
        public string EmpNumber { get; set; }
        public string HireDate { get; set; }
    }
}