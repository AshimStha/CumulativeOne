# CumulativeOne

**Overview**

This documentation outlines the server-rendered cycles that occur when a teacher is added or removed from the system. The process involves handling both the addition and removal scenarios separately.

---

## Teacher Addition

When a teacher is added, the process commences with user interaction through the application interface. Users input relevant teacher information in a designated form. There could also be client-side validation for the form which is then submitted to the server through a specific endpoint or route. 

Upon receiving the form data, the server performs server-side validation to guarantee data integrity. After successful validation, the server interacts with the database to store the validated teacher information, creating a new teacher record. Subsequently, the server updates the relevant views or templates to reflect the addition of the new teacher.

### Cycle 1

The first cycle of this process is the part till where the user fills in the create user form and hits sumbit.In context of this assignment, the user is first shown the main index page with the options to view the teacher or the students available in the system (DB). The user then fills in the form found in the New.cshtml view which can be accessed using the 'Add a Teacher' button and the data is validated before being processed further. 

### Cycle 2

After filling in the form, the Teacher WebAPI controller comes into play where the add method is used to define the inputs for the new teacher object and then the AddTeacher method in the TeacherData API controller is accessed. This method uses the Http POST method to define the attributes for the object and moves to the next method that is responsible for the data entry into the DB. This is also done utilizing the Http POST request as we are passing information over the web server into the DB. The view with the teachers list is then shown after the process is completed.



---

## Teacher Removal

To remove a teacher, users interact with the application by selecting the teacher to be removed and confirming the action. A confirmation step may be included to prevent accidental removal, presented as a modal dialog or a separate confirmation page. Once confirmation is received, a request is sent to the server to initiate the teacher removal process. The server, in turn, processes the removal request by deleting the corresponding teacher record from the database.

Following the database interaction, the server updates the relevant views or templates to reflect the removal of the teacher. This ensures that the user interface accurately represents the current state of the system, providing a seamless and responsive user experience.

### Cycle 1

For the first part, the detailed page view of a teacher is accessed by the user where the delete option is present. Pressing the button will take the user to the delete confirm page along with the id of the teacher to be deleted. The 'DeleteConfirm' method uses an Http GET method to fetch the data of the teachers and returns the corresponding view.

### Cycle 2

This is where the confirmation is done and it takes place with the help of the 'Delete' method in the Web API controller that uses the Http POST method to send the data of the teacher with the selected id to the 'DeleteTeacher' function of the API controller. This method has the definition for deleting a teacher and then the page is redirected to the list view with the updated data set. 

---

## Conclusion

This comprehensive documentation covers the steps of the server-rendered cycles involved in adding or removing a teacher from the system. The explanation supports user interactions and the server-side processes required to maintain data consistency and deliver an intuitive and coherent user experience.
