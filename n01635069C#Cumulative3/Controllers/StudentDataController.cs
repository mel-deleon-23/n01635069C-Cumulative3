using MySql.Data.MySqlClient;
using n01635069C_Cumulative3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01635069C_Cumulative3.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        //This controller will access the students table of the school database
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>
        /// GET api/StudentData/ListStudents
        /// </example>
        /// <returns>
        /// A list of students
        /// </returns>

        [HttpGet]
        [Route("api/StudentData/ListStudent/{StudentSearchKey}")]
        public IEnumerable<Student> ListStudents(string StudentSearchKey = null)
        {
            //Create instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open connection between database and web server
            Conn.Open();

            //Create a new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or  lower(concat(studentfname, ' ', studentlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + StudentSearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Student Names
            List<Student> Students = new List<Student>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                string StudentFName = ResultSet["studentfname"].ToString();
                string StudentLName = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                DateTime EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                Student NewStudent = new Student();
                NewStudent.studentid = StudentId;
                NewStudent.studentnumber = StudentNumber;
                NewStudent.studentfname = StudentFName;
                NewStudent.studentlname = StudentLName;
                NewStudent.enroldate = EnrolDate;

                Students.Add(NewStudent);
            }

            //close connection between database and web server
            Conn.Close();

            //return final list of students names
            return Students;

        }



        /// <summary>
        /// Finds a student from MySQL database through student id
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns>
        /// Student object containing info about the student with matching ID
        /// </returns>

        [HttpGet]
        [Route("api/StudentData/FindStudent/{StudentId}")]

        public Student FindStudent(int StudentId)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();
            //open connection
            Conn.Open();
            //create new query
            MySqlCommand cmd = Conn.CreateCommand();
            //SQL query & command
            cmd.CommandText = "Select * from students where studentid = @id";
            cmd.Parameters.AddWithValue("@id", StudentId);
            cmd.Prepare();
            //gather result set 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            Student SelectedStudent = new Student();
            while (ResultSet.Read())
            {
                SelectedStudent.studentid = Convert.ToInt32(ResultSet["studentid"]);
                SelectedStudent.studentfname = ResultSet["studentfname"].ToString();
                SelectedStudent.studentlname = ResultSet["studentlname"].ToString();
                SelectedStudent.studentnumber = ResultSet["studentnumber"].ToString();
                SelectedStudent.enroldate = Convert.ToDateTime(ResultSet["enroldate"]);
            }

            //close connection
            Conn.Close();

            return SelectedStudent;

        }



        /// <summary>
        /// Adds a New Student to MySQL Database.
        /// </summary>
        /// <param name="NewStudent">An object with fields that map to the columns of the student's table.</param>
        /// <example>
        /// POST api/StudentData/AddStudent 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	    "StudentFname":"Melissa",
        ///	    "StudentLname":"De Leon",
        ///	    "StudentNumber":"N1623"
        /// }
        /// </example>

        [HttpPost]
        public void AddStudent([FromBody] Student NewStudent)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewStudent.studentfname);

            //open connection
            Conn.Open();

            //establish new query
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query
            cmd.CommandText = "insert into students (studentid, studentfname, studentlname, studentnumber, enroldate) values (@StudentId, @StudentFName, @StudentLName, @StudentNumber, CURRENT_DATE())";
            cmd.Parameters.AddWithValue("@StudentId", NewStudent.studentid);
            cmd.Parameters.AddWithValue("@StudentFName", NewStudent.studentfname);
            cmd.Parameters.AddWithValue("@StudentLName", NewStudent.studentlname);
            cmd.Parameters.AddWithValue("@StudentNumber", NewStudent.studentnumber);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close connection
            Conn.Close();

        }


        /// <summary>
        /// Deletes a Student from MySQL Database.
        /// </summary>
        /// <param name="Studentid">The ID of the student</param>
        /// <example>
        /// POST api/StudentData/DeleteStudent/{id}
        /// </example>

        [HttpPost]
        public void DeleteStudent(int Studentid)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //establish new query
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from students where studentid=@id";
            cmd.Parameters.AddWithValue("@id", Studentid); ;
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close connection
            Conn.Close();

        }



        /// <summary>
        /// Updates an Author on the MySQL Database
        /// </summary>
        /// <param name="StudentInfo">">An object with fields that map to the columns of the students table</param>
        /// <example>
        ///  POST api/StudentData/UpdateStudent
        ///  FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "StudentId":"33",
        ///	    "StudentFname":"Melissa",
        ///	    "StudentLname":"De Leon",
        ///	    "StudentNumber":"N1623"
        /// }
        /// </example>
        [HttpPost]
        public void UpdateStudent(int Studentid, [FromBody]Student StudentInfo)
        {
            //create a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //establish new query
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "update students set studentfname=@StudentFname, studentlname=@StudentLname, studentnumber=@StudentNumber  where studentid=@StudentId";
            cmd.Parameters.AddWithValue("@StudentId", StudentInfo.studentid);
            cmd.Parameters.AddWithValue("@StudentFname", StudentInfo.studentfname);
            cmd.Parameters.AddWithValue("@StudentLname", StudentInfo.studentlname);
            cmd.Parameters.AddWithValue("@StudentNumber", StudentInfo.studentnumber);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close connection
            Conn.Close();
        }

    }
}
