using CumulativeOne.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public IEnumerable<Student> ListStudents(string SearchKey=null)
        {
            // Creating an instance for the connection
            MySqlConnection Conn = School.AccessDatabase();

            // Opening the connection between the web server and the db
            Conn.Open();

            // Creating a new query command for the db
            MySqlCommand cmd = Conn.CreateCommand();

            // The sql query to fetch the students data from the db
            cmd.CommandText = "Select * from students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname, ' ', studentlname)) like lower(@key)";

            // Defining the search key for the sql query above
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

            // Creating a prepared version of the sql query
            cmd.Prepare();

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
            cmd.CommandText = "Select * from students where studentid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

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

        // Method to add a new student

        /// <summary>
        /// A method that allows us to create a new student using the form and stores it into the DB
        /// </summary>
        /// <param name="NewStudent">The attributes for the new student from the Student model (A student object)</param>
        /// <returns>
        /// 
        /// POST api/StudentData/AddStudent
        /// 
        /// The curl request below shows that we are using a POST request to pass the post object data enclosed in the ""
        ///     curl -d "" http://localhost:61277/api/StudentData/AddStudent 
        /// 
        /// <example>
        /// {Getting error}
        /// Form Data: (JSON data being sent as the body of the HTTP request)
        ///     {
        ///         "studentid" : "1",
        ///         "studentfname" : "Bucky",
        ///         "studentlname" : "White",
        ///         "studentnumber" : "456",
        ///         "enroldate" : "2021-04-09"
        ///     }
        /// </example>
        /// 
        /// <example>
        ///     curl -d "{\"studentid\":\"1\",\"studentfname\":\"Bucky\",\"studentlname\":\"White\",\"studentnumber\":\"456\",\"enroldate\":\"2021-04-09\"}" -H "Content-Type: application/json" http://localhost:61277/api/StudentData/AddStudent
        /// </returns>
        /// </example>
        /// 
        /// <example>
        /// Using JSON file:
        ///     For this, we created a new folder in the project called testObject and added a new JSON file with the data. We then use the
        ///     following curl request to execute it.
        ///     
        ///     curl -d @students.json -H "Content-Type: application/json" http://localhost:61277/api/StudentData/AddStudent 
        ///     
        ///     (-v) can be used at the end of the curl request to see more details on the request
        /// </example>
        /// 


        [HttpPost]
        [Route("api/StudentData/AddStudent")]
        // The object is from the Student class
        // FromBody means we are getting the data from the body of the HTTP request
        public void AddStudent([FromBody] Student NewStudent)
        {
            // Creating a connection to the DB
            MySqlConnection connect = School.AccessDatabase();

            // Opening a connection between the DB and the web server
            connect.Open();

            // Creating a command using the connection
            MySqlCommand command = connect.CreateCommand();

            // Setting up the query
            command.CommandText = "Insert into `students`(studentfname, studentlname, studentnumber, enroldate) values (@Studentfname, @Studentlname, @StdNumber, CURRENT_DATE())";

            // Parameterization of the input values to avoid sql injection
            // The second parameters are from the class
            command.Parameters.AddWithValue("@Studentfname", NewStudent.StudentFname);
            command.Parameters.AddWithValue("@Studentlname", NewStudent.StudentLname);
            command.Parameters.AddWithValue("@StdNumber", NewStudent.StdNumber);

            // Creating a prepared version of the sql query
            command.Prepare();

            // Using this function to just execute the information and not to read it and also returns the number of rows affected
            command.ExecuteNonQuery();

            // Closing the DB connection
            connect.Close();
        }

        // Method to delete a student

        /// <summary>
        /// A method that receives and id and deletes the student associated with it
        /// </summary>
        /// <param name="studentId">The student id to be deleted</param>
        /// 
        /// <return>
        /// void
        /// </return>
        /// 
        /// <example>
        /// POST : api/StudentData/DeleteStudent/1 -> void
        /// </example>
        /// 


        [HttpPost]
        [Route("api/StudentData/DeleteStudent/{studentId}")]
        public void DeleteStudent(int studentId)
        {
            // Creating an instance of a DB connection
            MySqlConnection Conn = School.AccessDatabase();

            // Opening the connection between the web server and database
            Conn.Open();

            // Establishing a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL query for the delete process
            cmd.CommandText = "Delete from students where studentid=@id";

            // Parameterization of the input values to avoid sql injection
            cmd.Parameters.AddWithValue("@id", studentId);

            // Creating a prepared version of the sql query
            cmd.Prepare();

            // Using this function to just execute the information and not to read it and also returns the number of rows affected
            cmd.ExecuteNonQuery();

            // Closing the DB connection
            Conn.Close();
        }
    }
}
