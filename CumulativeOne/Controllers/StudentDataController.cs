using CumulativeOne.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CumulativeOne.Controllers
{
    public class StudentDataController : ApiController
    {
        // The model context class that allows us to access the MySQL db 
        private SchoolDBcontext School = new SchoolDBcontext();

        // Controller to access the students table from our school database

        /// <summary>
        /// Returns a list of students from the db
        /// </summary>
        /// <example>GET localhost:xx/api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of students (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Student> ListStudents()
        {
            // Creating an instance for the connection
            MySqlConnection Conn = School.AccessDatabase();

            // Opening the connection between the web server and the db
            Conn.Open();

            // Creating a new query command for the db
            MySqlCommand cmd = Conn.CreateCommand();

            // The sql query to fetch the students data from the db
            cmd.CommandText = "Select * from students";

            // Executing the query command and storing into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Creating a list of string data type to store the students name
            // Here, we are defining the type as Student in order to access the model methods to get or set data using object
            List<Student> StudentNames = new List<Student> { };

            // Looping through each row of values in the variable
            // ResultSet is itself a set of items hence, we can iterate over it
            while (ResultSet.Read())
            {
                // Accessing the table column information using the db column name as the index and storing in variables
                // Int32 means 32 bit integer - used here to explicitly convert to int
                int StudentID = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFName = ResultSet["studentfname"].ToString();
                string StudentLName = ResultSet["studentlname"].ToString();
                string StdNumber = ResultSet["studentnumber"].ToString();
                string EnrollDate = ResultSet["enroldate"].ToString();

                // Creating a new object instance for the student object
                Student NewStudent = new Student();

                // Assigning the values of the variables with the object functions as its properties in Student.cs model class
                NewStudent.StudentId = StudentID;
                NewStudent.StudentFname = StudentFName;
                NewStudent.StudentLname = StudentLName;
                NewStudent.StdNumber = StdNumber;
                NewStudent.EnrollDate = EnrollDate;

                // Adding the fetched student data into the string list 
                StudentNames.Add(NewStudent);
            }

            // Closing the connection between the MySQL Database and the WebServer
            Conn.Close();

            // Returning the final list of students names
            return StudentNames;
        }


        // Controller function to find a certain students and return the data

        /// <summary>
        /// Finds a student in the system given an ID
        /// </summary>
        /// <param name="id">The primary key for the student in the school DB</param>
        /// <example>GET localhost:xx/api/StudentData/FindStudent/{id}</example>
        /// <returns>A student object</returns>
        [HttpGet]

        // Teacher here means we are trying to return a Teacher using object
        public Student FindStudent(int id)
        {
            // Creating a new student class object
            Student NewStudent = new Student();

            // Creating an instance for the connection
            MySqlConnection Conn = School.AccessDatabase();

            // Opening the connection between the web server and the db
            Conn.Open();

            // Creating a new query command for the db
            MySqlCommand cmd = Conn.CreateCommand();

            // The sql query to fetch the student data from the db with the provided id
            cmd.CommandText = "Select * from students where studentid = " + id;

            // Executing the query command and storing into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // While there is data stored in the variable
            while (ResultSet.Read())
            {
                // Accessing the table column information using the db column name as the index and storing in variables
                // Int32 means 32 bit integer - used here to explicitly convert to int
                int StudentID = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFName = ResultSet["studentfname"].ToString();
                string StudentLName = ResultSet["studentlname"].ToString();
                string StdNumber = ResultSet["studentnumber"].ToString();
                string EnrollDate = ResultSet["enroldate"].ToString();

                // Assigning the values of the variables with the object functions as its properties in Student.cs model class
                NewStudent.StudentId = StudentID;
                NewStudent.StudentFname = StudentFName;
                NewStudent.StudentLname = StudentLName;
                NewStudent.StdNumber = StdNumber;
                NewStudent.EnrollDate = EnrollDate;
            }

            // Returning the student data (can be many)
            return NewStudent;
        }
    }
}
