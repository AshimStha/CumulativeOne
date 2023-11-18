using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeOne.Models;

namespace CumulativeOne.Controllers
{
    // Controller to help navigate through dynamic teacher views/pages

    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // The teacher list page

        //GET : /Teacher/List
        public ActionResult List()
        {
            // The API controller returns the data for the teachers

            // Instantiating a new object for the controller
            TeacherDataController controller = new TeacherDataController();
            // Using the method from the API controller to get the data and return it
            // Here, the function returns the data and stores into the list
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            // Returning a view with the teachers data
            return View(Teachers);
        }

        // The teacher details page

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            // The API controller returns the data for the teachers
            TeacherDataController controller = new TeacherDataController();
            // Using the method from the API controller to get the data of the specific teacher using id and return it
            Teacher NewTeacher = controller.FindTeacher(id);
            // Returning a view with the teachers data
            return View(NewTeacher);
        }
    }
}