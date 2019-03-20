using FirstMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstMVCApp.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        //string
       
            IList<Student> studentList = new List<Student>{
                            new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
                            new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
                            new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
                            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
                            new Student() { StudentId = 5, StudentName = "Ron" , Age = 31 } ,
                            new Student() { StudentId = 4, StudentName = "Chris" , Age = 17 } ,
                            new Student() { StudentId = 4, StudentName = "Rob" , Age = 19 }
                        };
            // Get the students from the database in the real application

          
            //return "This is the Index action method of Student Controller";
        
         public ActionResult Index()
        {
            return View(studentList);
        }

        public ActionResult Edit(int ID)
        {
            //Get the student from studentList sample collection for demo purpose.
            //Get the student from the database in the real application
            var studentID = studentList.Where(s => s.StudentId == ID).FirstOrDefault();

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Student sID)
        {
            //write code to update student 

            return RedirectToAction("Index");
        }


        /* [ActionName("find")]//find/1 instead of getbyid/1
         public ActionResult GetById(int id)
         {
             return View();
         }


         [HttpPost]
        public ActionResult PostAction()
         {
             return View("Index");
         }

         [HttpPut]
         public ActionResult PutAction()
         {
             return View("Index");
         }

         [HttpDelete]
         public ActionResult DeleteAction()
         {
             return View("Index");
         }

         [HttpHead]
         public ActionResult HeadAction()
         {
             return View("Index");
         }

         [HttpOptions]
         public ActionResult OptionsAction()
         {
             return View("Index");
         }

         [HttpPatch]
         public ActionResult PatchAction()
         {
             return View("Index");
         }*/


    }
}