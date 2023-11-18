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

        /// <summary>
        /// A method to return the list of the teachers
        /// </summary>
        /// <param name="SearchKey">The key to be used to make a search</param>
        /// <returns>
        /// The list of the teachers
        /// </returns>
        /// <example>GET : localhost:xx/Teacher/List</example>

        public ActionResult List(string SearchKey = null)
        {
            // The API controller returns the data for the teachers

            // Instantiating a new object for the controller
            TeacherDataController controller = new TeacherDataController();
            // Using the method from the API controller to get the data and return it
            // Here, the function returns the data and stores into the list
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            // Returning a view with the teachers data
            return View(Teachers);
        }

        // The teacher details page

        /// <summary>
        /// A function to return the details page of a teacher
        /// </summary>
        /// <param name="id">The selected teacher id</param>
        /// <returns>
        /// A detail page of the selected teacher using the id
        /// </returns>
        /// <example>GET : localhost:xx/Teacher/Show/{id}</example>

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