# CumulativeTwo

**Overview**

This documentation outlines the server-rendered cycles that occur when a teacher is added or removed from the system.

---

## Teacher Addition

![Server Render Cycle while adding teacher](/_readme/Teacher_add.png)



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

---

### Initiatives covered in C2

1. The server rendered cycle while creating and removing a teacher.
2. JS validation while inserting a teacher using the form.
3. Creating a teacher using Ajax XHR request.
4. CURL POST request for JSON data test during insert (Error prone).
