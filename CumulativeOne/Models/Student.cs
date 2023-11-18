using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeOne.Models
{
    // Student class for object creation and data access
    public class Student
    {
        // Defining the getters and setters to work with the database data
        public int StudentId { get; set; }
        public string StudentFname { get; set; }
        public string StudentLname { get; set; }
        public string StdNumber { get; set; }
        public string EnrollDate { get; set; }
    }
}