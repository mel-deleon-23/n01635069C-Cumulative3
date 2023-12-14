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
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of school database.
        /// <summary>
        /// Returns a list of teachers 
        /// </summary>
        /// <param name="TeacherSearchKey"></param>
        /// <returns>
        /// A list of teacher objects with fields mapped to the database column values
        /// </returns>
        /// <example> GET api/TeacherData/ListTeachers </example>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{TeacherSearchKey}")]
        public IEnumerable<Teacher> ListTeachers(string TeacherSearchKey = null)
        {
            //Create connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open connection 
            Conn.Open();

            //Create a new query 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or  lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + TeacherSearchKey + "%");
            cmd.Prepare();

            //Gather Result Set 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher>();

            while (ResultSet.Read())
            {
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();

                NewTeacher.teacherid = TeacherId;
                NewTeacher.employeenumber = EmployeeNumber;
                NewTeacher.teacherfname = TeacherFName;
                NewTeacher.teacherlname = TeacherLName;
                NewTeacher.hiredate = HireDate;
                NewTeacher.salary = Salary;

                Teachers.Add(NewTeacher);
            }

            //close connection
            Conn.Close();

            //return final list of teachers names
            return Teachers;

        }


        /// <summary>
        /// Finds a teacher from MySQL database through teacher id
        /// </summary>
        /// <param name="TeacherId"></param>
        /// <returns>
        /// Teacher object containing info about the teacher with matching ID
        /// </returns>

        [HttpGet]
        public Teacher FindTeacher(int TeacherId)
        {
            Teacher NewTeacher = new Teacher();

            //create connection
            MySqlConnection Conn = School.AccessDatabase();
            //open connection
            Conn.Open();
            //create new query
            MySqlCommand cmd = Conn.CreateCommand();
            //SQL query & command
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", TeacherId);
            cmd.Prepare();
            //gather result set 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {

                int teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherfname = ResultSet["teacherfname"].ToString();
                string teacherlname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString();
                DateTime hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal salary = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.teacherid = TeacherId;

                NewTeacher.teacherfname = ResultSet["teacherfname"].ToString();

                NewTeacher.teacherlname = ResultSet["teacherlname"].ToString();

                NewTeacher.employeenumber = ResultSet["employeenumber"].ToString();

                NewTeacher.hiredate = Convert.ToDateTime(ResultSet["hiredate"]);

                NewTeacher.salary = Convert.ToDecimal(ResultSet["salary"]);
            }

            //close connection
            Conn.Close();

            return NewTeacher;

        }


        /// <summary>
        /// Adds a New Teacher to MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	    "TeacherFname":"Bob",
        ///	    "TeacherLname":"Dylan",
        ///	    "EmployeeNumber":"T415",
        ///	    "Salary":"60"
        /// }
        /// </example>

        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {

            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.teacherfname);

            //open connection
            Conn.Open();

            //establish new query
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherId, @TeacherFName, @TeacherLName, @EmployeeNumber, CURRENT_DATE(), @Salary)";
            cmd.Parameters.AddWithValue("@TeacherId", NewTeacher.teacherid);
            cmd.Parameters.AddWithValue("@TeacherFName", NewTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@TeacherLName", NewTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close connection
            Conn.Close();

        }


        /// <summary>
        /// Deletes a Teacher from the MySQL Database.
        /// </summary>
        /// <param name="Teacherid">The ID of the teacher</param>
        /// <example>
        /// POST api/TeacherData/DeleteTeacher/{id}
        /// </example>

        [HttpPost]
        public void DeleteTeacher(int Teacherid)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //establish new query
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", Teacherid); ;
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close connection
            Conn.Close();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TeacherInfo"></param>
        /// <example>
        ///  POST api/TeacherData/UpdateTeacher
        ///  FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "StudentId":"11",
        ///	    "StudentFname":"Simon",
        ///	    "StudentLname":"Bassett"
        /// }
        /// </example>
        [HttpPost]
        public void UpdateTeacher(int Teacherid, [FromBody] Teacher TeacherInfo)
        {
            //create a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //establish new query
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, hiredate=@HireDate, salary=@Salary  where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherId", TeacherInfo.teacherid);
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.teacherfname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.teacherlname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.employeenumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.hiredate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close connection
            Conn.Close();
        }
    }
}
