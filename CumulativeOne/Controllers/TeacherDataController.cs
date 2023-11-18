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
            List<Teacher> TeacherNames = new List<Teacher> { };

            // Looping through each row of values in the variable
            // ResultSet is itself a set of items hence, we can iterate over it
            while (ResultSet.Read())
            {
                // Accessing the table column information using the db column name as the index and storing in variables
                int TeacherID = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string EmpNo = ResultSet["employeenumber"].ToString() ;
                string HireDate = ResultSet["hiredate"].ToString();

                // Creating a new object instance for the teacher object
                Teacher NewTeacher = new Teacher();

                // Assigning the values of the variables with the object functions as its properties in Teacher.cs model class
                NewTeacher.TeacherId = TeacherID;
                NewTeacher.TeacherFname = TeacherFName;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.EmpNumber = EmpNo;
                NewTeacher.HireDate = HireDate;

                // Adding the fetched teacher data into the string list 
                TeacherNames.Add(NewTeacher);
            }

            // Closing the connection between the MySQL Database and the WebServer
            Conn.Close();

            // Returning the final list of teacher names
            return TeacherNames;
        }


        // Controller function to find a certain teacher and return the data

        /// <summary>
        /// Finds a teacher in the system given an ID
        /// </summary>
        /// <param name="id">The primary key for the teacher in the school DB</param>
        /// <example>GET localhost:xx/api/TeacherData/FindTeacher/{id}</example>
        /// <returns>A teacher object</returns>
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
            cmd.Parameters.AddWithValue("@id", id);
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
                string HireDate = ResultSet["hiredate"].ToString();

                // Assigning the values of the variables with the object functions as its properties in Teacher.cs model class
                NewTeacher.TeacherId = TeacherID;
                NewTeacher.TeacherFname = TeacherFName;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.EmpNumber = EmpNo;
                NewTeacher.HireDate = HireDate;
            }

            // Returning the teacher data (can be many)
            return NewTeacher;
        }
    }
}
