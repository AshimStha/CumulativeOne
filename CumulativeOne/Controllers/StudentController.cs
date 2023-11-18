using CumulativeOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CumulativeOne.Controllers
{
    // Controller to help navigate through dynamic student views/pages

    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // The student list page

        /// <summary>
        /// A function to return the list of students 
        /// </summary>
        /// <example>GET : localhost:xx/Student/List</example>
        /// <returns>A list of students from the database</returns>
        /// 

        public ActionResult List()
        {
            // The API controller returns the data for the students

            // Instantiating a new object for the controller
            StudentDataController controller = new StudentDataController();
            // Using the method from the API controller to get the data and return it
            // Here, the function returns the data and stores into the list
            IEnumerable<Student> Students = controller.ListStudents();
            // Returning a view with the teachers data
            return View(Students);
        }

        // The student details page

        /// <summary>
        /// A function to return the detail page of selected student
        /// </summary>
        /// <param name="id">The id for the selected student</param>
        /// <returns>
        /// <example>GET : localhost:xx/Student/Show/{id}</example>
        /// A detail page of the selected student using the id
        /// </returns>

        public ActionResult Show(int id)
        {
            // The API controller returns the data for the students
            StudentDataController controller = new StudentDataController();
            // Using the method from the API controller to get the data of the specific student using id and return it
            Student NewStudent = controller.FindStudent(id);
            // Returning a view with the teachers data
            return View(NewStudent);
        }
    }
}