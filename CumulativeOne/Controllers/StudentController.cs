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

        //GET : /Student/List
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

        //GET : /Student/Show/{id}
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