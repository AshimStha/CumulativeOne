// AJAX for teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml


function AddTeacher() {

    //goal: send a request which looks like this:
    //POST : http://localhost:xxx/api/TeacherData/AddTeacher
    //with POST data of teacherfname, teacherlname, empnumber, hiredate and salary.

    // setting up the URL for the request
    var URL = "http://localhost:61277/api/TeacherData/AddTeacher/";

    // creating an XML HTTP request object
    var rq = new XMLHttpRequest();

    // where is this request sent to?
    // is the method GET or POST?
    // what should we do with the response?

    // Fetching the HTML DOM elements for the error span fields
    var teacherFname = document.getElementById("TeacherFname").value;
    var teacherLname = document.getElementById("TeacherLname").value;
    var teacherEmpNum = document.getElementById("EmpNumber").value;
    var hireDate = document.getElementById("Hiredate").value;
    var teacherSalary = document.getElementById("Salary").value;



    var TeacherData = {
        "TeacherFname": teacherFname,
        "TeacherLname": teacherLname,
        "EmpNumber": teacherEmpNum,
        "HireDate": hireDate,
        "Salary": teacherSalary
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
    rq.send(JSON.stringify(TeacherData));

}