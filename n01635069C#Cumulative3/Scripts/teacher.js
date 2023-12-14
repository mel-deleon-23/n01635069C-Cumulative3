function AddTeacher() {

	//Send a request which looks like this:
	//POST : http://localhost:51380/api/TeacherData/AddTeacher
	//with POST data of teacherfname, teacherlname, employeenumber, etc.

	var URL = "http://localhost:51380/api/TeacherData/AddTeacher/";

	//make a variable for request
	var request = new XMLHttpRequest();

	//variables of the teacher tables
	var TeacherId = document.getElementById('TeacherId').value;
	var TeacherFName = document.getElementById('TeacherFName').value;
	var TeacherLName = document.getElementById('TeacherLName').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;


	var TeacherData = {
		"TeacherId": TeacherId,
		"TeacherFName": TeacherFName,
		"TeacherLName": TeacherLName,
		"EmployeeNumber": EmployeeNumber,
		"HireDate": HireDate,
		"Salary": Salary
	};

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished
		}

	}
	rq.send(JSON.stringify(TeacherData));

}

function UpdateTeacher(TeacherId) {

	//Send a request which looks like this:
	//POST : http://localhost:51380/api/TeacherData/UpdateTeacher
	//with POST data of teacherfname, teacherlname, employeenumber, etc.

	var URL = "http://localhost:51380/api/TeacherData/AddTeacher/"+TeacherId;

	//make a variable for request
	var request = new XMLHttpRequest();

	//variables of the teacher tables
	var TeacherId = document.getElementById('TeacherId').value;
	var TeacherFName = document.getElementById('TeacherFName').value;
	var TeacherLName = document.getElementById('TeacherLName').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;


	var TeacherData = {
		"TeacherId": TeacherId,
		"TeacherFName": TeacherFName,
		"TeacherLName": TeacherLName,
		"EmployeeNumber": EmployeeNumber,
		"HireDate": HireDate,
		"Salary": Salary
	};

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished
		}

	}
	rq.send(JSON.stringify(TeacherData));

}