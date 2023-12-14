using n01635069C_Cumulative3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace n01635069C_Cumulative3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Teacher/List
        public ActionResult List(string TeacherSearchKey = null)
        {
            //pass teachers information into View

            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(TeacherSearchKey);
            return View(Teachers);
        }

        //GET: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }


        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //GET: /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();
        }

        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(int TeacherId, string TeacherFName, string TeacherLName, string EmployeeNumber, decimal Salary)
        {
            Debug.WriteLine("I have accessed the Create Method");
            Debug.WriteLine(TeacherId);
            Debug.WriteLine(TeacherFName);
            Debug.WriteLine(TeacherLName);

            Teacher NewTeacher = new Teacher();

            NewTeacher.teacherid = TeacherId;
            NewTeacher.teacherfname = TeacherFName;
            NewTeacher.teacherlname = TeacherLName;
            NewTeacher.employeenumber = EmployeeNumber;
            NewTeacher.salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }


        //GET: /Teacher/Update/{id}
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        //POST: /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, Decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.teacherid = id;
            TeacherInfo.teacherfname = TeacherFname;
            TeacherInfo.teacherlname = TeacherLname;
            TeacherInfo.employeenumber = EmployeeNumber;
            TeacherInfo.salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}