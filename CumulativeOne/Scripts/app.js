window.onload = function () {
    // Creating a form handle for the form
    var formHandle = document.forms("teacherForm");

    // Fetching the HTML DOM elements for the error span fields
    var teacherFname = document.getElementById("FnameError");
    var teacherLname = document.getElementById("LnameError");
    var teacherEmpNum = document.getElementById("EmpNumError");
    var teacherSalary = document.getElementById("SalaryError");

    // Run the function when the form is submitted
    formHandle.onsubmit = processForm;

    // The function to be executed when the form is submitted
    function processForm() {

        // Fetching the form input elements using the form handle
        var fname = formHandle.TeacherFname.value;
        var lname = formHandle.TeacherLname.value;
        var empNo = formHandle.EmpNumber.value;
        var salary = formHandle.Salary.value;

        // If the firstname is null or empty
        if (fname === null || fname == "") {
            // Make the error message visible
            teacherFname.style.display = "block";
            // Do not submit the form
            return false;
        }
        // If the lastname is null or empty
        else if (lname === null || lname == "") {
            // Make the error message visible
            teacherLname.style.display = "block";
            // Do not submit the form
            return false;
        }
        // If the employee number is null or empty
        else if (empNo === null || empNo == "") {
            // Make the error message visible
            teacherEmpNum.style.display = "block";
            // Do not submit the form
            return false;
        }
        // If the salary is null or empty
        else if (salary === null || empNo == "") {
            // Make the error message visible
            teacherSalary.style.display = "block";
            // Do not submit the form
            return false;
        }
        // submit the form
        else {
            return true;
        }
    }
}



// AJAX for author Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml


function AddAuthor() {

    //goal: send a request which looks like this:
    //POST : http://localhost:51326/api/AuthorData/AddAuthor
    //with POST data of authorname, bio, email, etc.

    var URL = "http://localhost:51326/api/AuthorData/AddAuthor/";

    var rq = new XMLHttpRequest();
    // where is this request sent to?
    // is the method GET or POST?
    // what should we do with the response?

    var AuthorFname = document.getElementById('AuthorFname').value;
    var AuthorLname = document.getElementById('AuthorLname').value;
    var AuthorEmail = document.getElementById('AuthorEmail').value;
    var AuthorBio = document.getElementById('AuthorBio').value;



    var AuthorData = {
        "AuthorFname": AuthorFname,
        "AuthorLname": AuthorLname,
        "AuthorEmail": AuthorEmail,
        "AuthorBio": AuthorBio
    };


    rq.open("POST", URL, true);
    rq.setRequestHeader("Content-Type", "application/json");
    rq.onreadystatechange = function () {
        //ready state should be 4 AND status should be 200
        if (rq.readyState == 4 && rq.status == 200) {
            //request is successful and the request is finished

            //nothing to render, the method returns nothing.


        }

    }
    //POST information sent through the .send() method
    rq.send(JSON.stringify(AuthorData));

}