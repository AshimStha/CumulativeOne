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
        /// <param name="SearchKey">The key to be used to make a search</param>
        /// <example>GET : localhost:xx/Student/List</example>
        /// <returns>A list of students from the database</returns>
        /// 

        public ActionResult List(string SearchKey=null)
        {
            // The API controller returns the data for the students

            // Instantiating a new object for the controller
            StudentDataController controller = new StudentDataController();
            // Using the method from the API controller to get the data and return it
            // Here, the function returns the data and stores into the list
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);
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
        /// <example>GET : localhost:xx/Student/Show/5  ->  Elizabeth Murray</example>
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

        // The view with the form

        /// <summary>
        /// A method to show the page with the form to add a new student
        /// </summary>
        /// <returns>
        /// </returns>
        /// 
        /// <example>
        ///     GET localhost:xx/Views/Student/New.cshtml
        /// </example>
        /// 

        public ActionResult New()
        {
            return View();
        }

        // The method to add the new student 

        /// <summary>
        /// A method that receives the new student data and sends it to the API
        /// </summary>
        /// <param name="StdNumber">The student number</param>
        /// <param name="StudentFname">The student firstname</param>
        /// <param name="StudentLname">The student lastname</param>
        /// <returns>
        /// Adds the student and returns the list
        /// </returns>
        /// 

        [HttpPost]
        public ActionResult Add(string StudentFname, string StudentLname, string StdNumber)
        {
            // Creating a new object instance for the Student class
            Student NewStudent = new Student();

            // Assigning the values from the form to the object properties
            NewStudent.StudentFname = StudentFname;
            NewStudent.StudentLname = StudentLname;
            NewStudent.StdNumber = StdNumber;

            // Creating a new instance for the student API controller
            StudentDataController controller = new StudentDataController();

            // Adding the new student using the object
            controller.AddStudent(NewStudent);

            // returning back to the List view
            return RedirectToAction("List");
        }

        // Method to show the delete confirmation view

        /// <summary>
        /// A function that redirects to the delete confirmation page for students
        /// </summary>
        /// <returns>
        /// A view for the delete confirmation
        /// </returns>
        /// 
        /// <example>
        ///     navigate to Views/Student/DeleteConfirm.cshtml
        /// </example>
        /// 

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            // Creating an instance of the StudentDataController
            StudentDataController controller = new StudentDataController();

            // Using the method of the controller to find the student with the id and storing in an object instance of the model class
            Student SelectedStudent = controller.FindStudent(id);

            // Returning the view with the student data
            return View(SelectedStudent);
        }

        // Method to delete the student

        /// <summary>
        /// A method that deletes the selected student
        /// </summary>
        /// <param name="id">The id of the student to be deleted</param>
        /// <returns>
        /// The List view after the deletion of the student
        /// </returns>
        /// 
        /// <example>
        ///     POST: localhost:xx/Student/Delete
        /// </example>
        /// 

        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Creating an instance of the API controller
            StudentDataController controller = new StudentDataController();

            // Calling the delete function from the API controller and id is passed as the parameter
            controller.DeleteStudent(id);

            // Redirecting to the List view
            return RedirectToAction("List");
        }
    }
}