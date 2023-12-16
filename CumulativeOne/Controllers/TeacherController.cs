using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <example>GET : localhost:xx/Teacher/Show/5  ->  Jessica Morris</example>

        public ActionResult Show(int id)
        {
            // The API controller returns the data for the teachers
            TeacherDataController controller = new TeacherDataController();
            // Using the method from the API controller to get the data of the specific teacher using id and return it
            Teacher NewTeacher = controller.FindTeacher(id);
            // Returning a view with the teachers data
            return View(NewTeacher);
        }

        // The view with the form

        /// <summary>
        /// A method to show the page with the form to add a new teacher
        /// </summary>
        /// <returns>
        /// A view with the create teacher form
        /// </returns>
        /// 
        /// <example>
        ///     GET localhost:xx/Teacher/New -> View with form
        /// </example>
        /// 

        public ActionResult New ()
        {
            return View();
        }

        // Method to open the view for the AJAX add feature

        /// <summary>
        /// A method to access the view with the form to create a new teacher
        /// </summary>
        /// <returns>
        /// The view with the create form
        /// </returns>
        /// 

        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }

        // The method to add the new teacher 

        /// <summary>
        /// A method that receives the new teacher data and sends it to the API
        /// </summary>
        /// <param name="EmpNumber">The employee number of the teacher</param>
        /// <param name="TeacherFname">The first name for the teaher</param>
        /// <param name="TeacherLname">The last name for the teacher</param>
        /// <param name="Salary">The salary for the teacher</param>
        /// <returns>
        /// Adds the teacher and returns the list
        /// </returns>
        /// 
        /// <example>
        /// POST localhost:xx/Teacher/Add
        /// </example>
        /// 

        [HttpPost]
        public ActionResult Add (string TeacherFname, string TeacherLname, string EmpNumber, int Salary)
        {
            // Creating a new object instance for the Teacher class
            Teacher NewTeacher = new Teacher();

            // Assigning the values from the form to the object properties
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmpNumber = EmpNumber;
            NewTeacher.Salary = Salary;

            // Creating a new instance for the teacher API controller
            TeacherDataController controller = new TeacherDataController();

            // Adding the new teacher using the object
            controller.AddTeacher(NewTeacher);

            // returning back to the List view
            return RedirectToAction("List");
        }

        // Method to show the delete confirmation view

        /// <summary>
        /// A function that redirects to the delete confirmation page for teachers
        /// </summary>
        /// <returns>
        /// A view for the delete confirmation
        /// </returns>
        /// 
        /// <example>
        ///     navigate to Views/Teacher/DeleteConfirm.cshtml
        /// </example>
        /// 
        /// <example>
        ///     GET localhost:xx/Teacher/DeleteConfirm/{id}
        /// </example>
        /// 

        [HttpGet]
        public ActionResult DeleteConfirm (int id)
        {
            // Creating an instance of the TeacherDataController
            TeacherDataController controller = new TeacherDataController();

            // Using the method of the API controller to find the teacher with the id and storing in an object instance of the model class
            Teacher SelectedTeacher = controller.FindTeacher(id);

            // Returning the view with the teacher data
            return View(SelectedTeacher);
        }

        // Method to delete the teacher

        /// <summary>
        /// A method that deletes the selected teacher
        /// </summary>
        /// <param name="id">The id of the teacher to be deleted</param>
        /// <returns>
        /// The List view after the deletion of the teacher
        /// </returns>
        /// 
        /// <example>
        ///     POST: localhost:xx/Teacher/Delete/{id}
        /// </example>
        /// 

        [HttpPost]
        public ActionResult Delete (int id)
        {
            // Creating an instance of the API controller
            TeacherDataController controller = new TeacherDataController();

            // Calling the delete function from the API controller and id is passed as the parameter
            controller.DeleteTeacher(id);

            // Redirecting to the List view
            return RedirectToAction("List");
        }

        // Method to navigate to the update form view for teachers

        /// <summary>
        /// A method to navigate to the update/edit form with the data of the selected teacher
        /// </summary>
        /// <param name="id">The selected teacher id</param>
        /// <returns>
        /// The view with the update form and the object details 
        /// </returns>
        /// <example>
        ///     GET: /Teacher/Update/{id}
        ///     Route to /Views/Teacher/Update.cshtml
        /// </example>
        /// <example>
        ///     /Teacher/Update/7 -> Update.cshtml with details of Shannon Barton
        /// </example>

        public ActionResult Update(int id)
        {
            // Creating an instance of the API controller
            TeacherDataController Controller = new TeacherDataController();

            // Using the method of the API controller to find the teacher with the id and storing it in an object instance of the model class
            Teacher selectedTeacher = Controller.FindTeacher(id);

            // Returning the view update.cshtml with the teacher instance data
            return View(selectedTeacher);
        }

        // Method to access the view for the AJAX update feature

        /// <summary>
        /// A method to access the view with the form to update a selected teacher and show the details in
        /// the input fields
        /// </summary>
        /// <returns>
        /// The view with the update teacher form
        /// </returns>
        /// 

        //GET : /Teacher/AjaxUpdate/{id}
        public ActionResult AjaxUpdate(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        // Function to update the teacher data 

        /// <summary>
        /// A function that receives a POST request with the data of an existing teacher with the new values from the update
        /// form and redirects to the teacher details page using the API
        /// </summary>
        /// <param name="id">Id of the Teacher to be updated</param>
        /// <param name="EmpNumber">The employee number of the teacher</param>
        /// <param name="TeacherFname">The first name for the teaher</param>
        /// <param name="TeacherLname">The last name for the teacher</param>
        /// <param name="Salary">The salary for the teacher</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/6
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"John",
        ///	"TeacherLname":"Doe",
        ///	"EmpNumber":"T5634",
        ///	"Salary":"40.45"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmpNumber, int Salary)
        {
            // Creating a new teacher instance
            Teacher TeacherInfo = new Teacher();

            // Assigning the values from the update form to the object properties
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmpNumber = EmpNumber;
            TeacherInfo.Salary = Salary;

            // Creating a new instance for the teacher API controller
            TeacherDataController controller = new TeacherDataController();

            // Accessing the UpdateTeacher method in the API controller to update the selected teacher
            controller.UpdateTeacher(id, TeacherInfo);

            // Redirecting to the teacher details page for the selected teacher
            return RedirectToAction("Show/" + id);
        }
    }
}