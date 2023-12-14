function AddStudent() {

	//Send a request which looks like this:
	//POST : http://localhost:51380/api/StudentData/AddStudent
	//with POST data of studentfname, studentlname, etc.

	var URL = "http://localhost:51380/api/StudentData/AddStudent/";

	//make a variable for request
	var request = new XMLHttpRequest();

	//variables of the student tables
	var StudentId = document.getElementById('StudentId').value;
	var StudentFName = document.getElementById('StudentFName').value;
	var StudentLName = document.getElementById('StudentLName').value;
	var StudentNumber = document.getElementById('StudentNumber').value;


	var StudentData = {
		"StudentId": StudentId,
		"StudentFName": StudentFName,
		"StudentLName": StudentLName,
		"StudentNumber": StudentNumber
	};

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished
		}

	}
	rq.send(JSON.stringify(StudentData));

}

function UpdateStudent(StudentId) {

	//Send a request which looks like this:
	//POST : http://localhost:51380/api/StudentData/UpdateStudent
	//with POST data of studentfname, studentlname, etc.

	var URL = "http://localhost:51380/api/StudentData/UpdateStudent/"+StudentId;

	//make a variable for request
	var request = new XMLHttpRequest();

	//variables of the student tables
	var StudentId = document.getElementById('StudentId').value;
	var StudentFName = document.getElementById('StudentFName').value;
	var StudentLName = document.getElementById('StudentLName').value;
	var StudentNumber = document.getElementById('StudentNumber').value;

	var StudentData = {
		"StudentId": StudentId,
		"StudentFName": StudentFName,
		"StudentLName": StudentLName,
		"StudentNumber": StudentNumber
	};

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished
		}

	}
	rq.send(JSON.stringify(StudentData));
}