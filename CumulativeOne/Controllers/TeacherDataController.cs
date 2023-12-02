using CumulativeOne.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CumulativeOne.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The model context class that allows us to access the MySQL db 
        private SchoolDBcontext School = new SchoolDBcontext();

        // Controller to access and display the teachers table from our school database

        /// <summary>
        /// Returns a list of teachers from the db
        /// </summary>
        /// <example>GET localhost:xx/api/TeacherData/ListTeachers</example>
        /// <param name="SearchKey">The key that is used to make a search</param>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            // Creating an instance for the connection
            MySqlConnection Conn = School.AccessDatabase();

            // Opening the connection between the web server and the db
            Conn.Open();

            // Creating a new query command for the db
            MySqlCommand cmd = Conn.CreateCommand();

            // The sql query to fetch the teachers data from the db
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            // Defining the search key for the sql query above
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

            // Creating a prepared version of the sql query
            cmd.Prepare();

            // Executing the query command and storing into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Creating a list of string data type to store the teachers name
            // Here, we are defining the type as Teacher in order to access the model methods to get or set data using object
            List<Teacher> Teachers = new List<Teacher> { };

            // Looping through each row of values in the variable
            // ResultSet is itself a set of items hence, we can iterate over it
            while (ResultSet.Read())
            {
                // Accessing the table column information using the db column name as the index and storing in variables
                int TeacherID = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string EmpNo = ResultSet["employeenumber"].ToString() ;
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                int Salary = Convert.ToInt32(ResultSet["salary"]);

                // Creating a new object instance for the teacher object
                Teacher NewTeacher = new Teacher();

                // Assigning the values of the variables with the object functions as its properties in Teacher.cs model class
                NewTeacher.TeacherId = TeacherID;
                NewTeacher.TeacherFname = TeacherFName;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.EmpNumber = EmpNo;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                // Adding the fetched teacher data into the string list 
                Teachers.Add(NewTeacher);
            }

            // Closing the connection between the MySQL Database and the WebServer
            Conn.Close();

            // Returning the final list of teacher names
            return Teachers;
        }


        // Controller function to find a certain teacher and return the data

        /// <summary>
        /// Finds a teacher in the system given an ID
        /// </summary>
        /// <param name="id">The primary key for the teacher in the school DB</param>
        /// <example>GET localhost:xx/api/TeacherData/FindTeacher/{id}</example>
        /// <returns>A teacher object</returns>
        /// <returns>Teacher id, Employee number, Firstname, Lastname, Hiredate and Salary details</returns>
        [HttpGet]

        // Teacher here means we are trying to return a Teacher using object
        public Teacher FindTeacher(int id)
        {
            // Creating a new teacher object
            Teacher NewTeacher = new Teacher();

            // Creating an instance for the connection
            MySqlConnection Conn = School.AccessDatabase();

            // Opening the connection between the web server and the db
            Conn.Open();

            // Creating a new query command for the db
            MySqlCommand cmd = Conn.CreateCommand();

            // The sql query to fetch the teacher data from the db with the provided id
            cmd.CommandText = "Select * from teachers where teacherid = @id";

            // Parameterizing the value for security
            cmd.Parameters.AddWithValue("@id", id);

            // Creating a prepared version of the sql query
            cmd.Prepare();

            // Executing the query command and storing into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // While there is data stored in the variable
            while (ResultSet.Read())
            {
                // Accessing the table column information using the db column name as the index and storing in variables
                // Int32 means 32 bit integer - used here to explicitly convert to int
                int TeacherID = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string EmpNo = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                int Salary = Convert.ToInt32(ResultSet["salary"]);

                // Assigning the values of the variables with the object functions as its properties in Teacher.cs model class
                NewTeacher.TeacherId = TeacherID;
                NewTeacher.TeacherFname = TeacherFName;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.EmpNumber = EmpNo;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            // Returning the teacher data (can be many)
            return NewTeacher;
        }


        // Method to add a new teacher

        /// <summary>
        /// A method that allows us to create a new teacher using the form and stores it into the DB
        /// </summary>
        /// <param name="NewTeacher">The attributes for the new teacher from the Teacher model (A teacher object)</param>
        /// <returns>
        /// 
        /// POST api/TeacherData/AddTeacher
        /// 
        /// The curl request below shows that we are using a POST request to pass the post object data enclosed in the ""
        ///     curl -d "" http://localhost:61277/api/TeacherData/AddTeacher 
        /// 
        /// <example>
        /// {Getting error that says a positional parameter can't be found}
        /// Form Data: (JSON data being sent as the body of the HTTP request)
        ///     {
        ///         "teacherid" : "1",
        ///         "teacherfname" : "John",
        ///         "teacherlname" : "Doe",
        ///         "employeenumber" : "123",
        ///         "hiredate" : "2022-09-18",
        ///         "salary" : "38"
        ///     }
        /// </example>
        /// 
        /// <example>
        ///     curl -d "{\"teacherid\":\"1\",\"teacherfname\":\"John\",\"teacherlname\":\"Doe\",\"employeenumber\":\"123\",\"hiredate\":\"2022-09-18\",\"salary\":\"38\"}" -H "Content-Type: application/json" http://localhost:61277/api/TeacherData/AddTeacher
        /// </returns>
        /// </example>
        /// 
        /// <example>
        /// Using JSON file:
        ///     For this, we created a new folder in the project called testObject and added a new JSON file with the data. We then use the
        ///     following curl request to execute it.
        ///     
        ///     curl -d @teachers.json -H "Content-Type: application/json" http://localhost:61277/api/TeacherData/AddTeacher 
        ///     
        ///     (-v) can be used at the end of the curl request to see more details on the request
        /// </example>
        /// 
        /// Note to Christine:
        ///     While adding a new teacher, there sometimes occurs an error that says null data was passed for salary even though data is seen
        ///     in the payload. I tried it multiple times where it worked most of the time but it often throws an error. I could not figure out 
        ///     why it happens?
        /// 


        [HttpPost]
        [Route("api/TeacherData/AddTeacher")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        // The object is from the Teacher class
        // FromBody means we are getting the data from the body of the HTTP request
        public void AddTeacher ([FromBody] Teacher NewTeacher)
        {
            // Getting the current date in a variable
            DateTime now = DateTime.Now;

            // Creating a connection to the DB
            MySqlConnection connect = School.AccessDatabase();

            // Opening a connection between the DB and the web server
            connect.Open();

            // Creating a command using the connection
            MySqlCommand command = connect.CreateCommand();

            // Setting up the query
            command.CommandText = "Insert into `teachers`(teacherfname, teacherlname, employeenumber, hiredate, salary) values (@Teacherfname, @Teacherlname, @EmpNumber, @Hiredate, @Salary)";

            // Parameterization of the input values to avoid sql injection
            // The second parameters are from the class
            command.Parameters.AddWithValue("@Teacherfname", NewTeacher.TeacherFname);
            command.Parameters.AddWithValue("@Teacherlname", NewTeacher.TeacherLname);
            command.Parameters.AddWithValue("@EmpNumber", NewTeacher.EmpNumber);
            command.Parameters.AddWithValue("@Hiredate", now);
            command.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            // Creating a prepared version of the sql query
            command.Prepare();

            // Using this function to just execute the information and not to read it and also returns the number of rows affected
            command.ExecuteNonQuery();

            // Closing the DB connection
            connect.Close();
        }


        // Method to delete a teacher

        /// <summary>
        /// A method that receives and id and deletes the teacher associated with it
        /// </summary>
        /// <param name="teacherId">The teacher id to be deleted</param>
        /// 
        /// <return>
        /// void
        /// </return>
        /// 
        /// <example>
        /// POST : api/TeacherData/DeleteTeacher/3 -> void
        /// </example>
        /// 


        [HttpPost]
        [Route("api/TeacherData/DeleteTeacher/{teacherId}")]
        public void DeleteTeacher (int teacherId)
        {
            // Creating an instance of a DB connection
            MySqlConnection Conn = School.AccessDatabase();

            // Opening the connection between the web server and database
            Conn.Open();

            // Establishing a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();


            cmd.CommandText = "DELETE FROM classes WHERE classes.teacherid = @id";

            cmd.Parameters.AddWithValue("@id", teacherId);
            cmd.Prepare();

            cmd.ExecuteNonQuery();


            // SQL query for the delete process
            cmd.CommandText = "Delete from teachers where teacherid=@id";

            // Parameterization of the input values to avoid sql injection
            cmd.Parameters.AddWithValue("@id", teacherId);

            // Creating a prepared version of the sql query
            cmd.Prepare();

            // Using this function to just execute the information and not to read it and also returns the number of rows affected
            cmd.ExecuteNonQuery();

            // Closing the DB connection
            Conn.Close();
        }
    }
}
