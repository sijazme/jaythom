using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5.Models;

namespace MVC5.Controllers
{
    public class HomeController : Controller
    {
        private ZooDBContext db = new ZooDBContext();
        private Service svc = new Service();

        public ActionResult Index()
        {
            IndexVidelModel ivm = new IndexVidelModel();
            ivm.Animals = svc.GetAnimals();
            ivm.Staffs = svc.GetStaffs();

            return View(ivm);
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = svc.GetAnimal(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        /// <summary>
        /// Create a new Animal
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            AnimalViewModel avm = new AnimalViewModel();
            AddStaffList(avm);

            return View(avm);
        }

        private void AddStaffList(AnimalViewModel avm)
        {
            List<Staff> stafflist = svc.GetAvailableStaff();

            avm.StaffList = stafflist.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName
            });
        }
        

        /// <summary>
        /// CRUD OPERATIONS ADDS ANIMAL TO DATABASE
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnimalViewModel avm)
        {            
            ModelState.Remove("Animal.Id");

            if (ModelState.IsValid)
            {
                avm.Animal.AnimalID = GetRandomID();
                svc.AddAnimal(avm.Animal);
                return RedirectToAction("Index");
            }

            AddStaffList(avm);
            return View(avm);
        }

        private string GetRandomID()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        /// <summary>
        /// DISPLAYS THE ANIMAL DETAILS FOR UPDATE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Animal animal = svc.GetAnimal(id);

            if (animal == null)
            {
                return HttpNotFound();
            }

            AnimalViewModel avm = GetAnimalViewModel(animal);

            return View(avm);
        }

        /// <summary>
        /// RETURNS A VIEWMODEL WITH THE ANIMAL AND A POPULATED STAFF LIST
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public AnimalViewModel GetAnimalViewModel(Animal animal)
        {

            AnimalViewModel avm = new AnimalViewModel();

            List<Staff> stafflist = svc.GetAvailableStaff();

            if (animal.StaffId != 0)
            {
                Staff toadd = svc.GetStaff(animal.StaffId);
                if (toadd != null)
                {
                    stafflist.Add(toadd);
                }
            }

            avm.Animal = animal;
            avm.SetStaffList(stafflist);
            avm.Select();

            return avm;
        } 

        /// <summary>
        /// CRUD OPERATION UPDATES ANIMAL IN THE DATABASE
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnimalViewModel avm)
        {          
            if (ModelState.IsValid)
            {
                svc.UpdateAnimal(avm.Animal);
                return RedirectToAction("Index");
            }
            return View(avm);
        }

        /// <summary>
        /// DISPLAYS THE ANIMAL DETAILS FOR DELETION
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = svc.GetAnimal(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        /// <summary>
        /// CRUD OPERATIONS DELETES ANIMAL IN THE DATABASE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            svc.DeleteAnimal(id);
            return RedirectToAction("Index");
        }

        /// *************************       STAFF METHODS              *************************      

        /// <summary>
        /// BINDS THE STAFF DETAILS TO THE VIEW
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditStaff(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        /// <summary>
        /// CRUD OPERATIONS UPDATES STAFF MEMBER IN THE DATABASE
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStaff([Bind(Include = "Id,EmployeeID,FirstName,LastName,PhoneNumber,Email,Dob")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                svc.UpdateStaff(staff);
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        /// <summary>
        /// BINDS THE STAFF DETAILS TO THE VIEW
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteStaff(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Staff staff = svc.GetStaff(id);

            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        /// <summary>
        /// CRUD OPERATIONS DELETES STAFF MEMBER IN THE DATABASE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteStaff")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStaffConfirmed(int id)
        {
            svc.DeleteStaff(id);            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns view to allow creating of new staff member
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateStaff()
        {
            return View();
        }

        /// <summary>
        /// CRUD OPERATIONS INSERTS STAFF MEMBER IN THE DATABASE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStaff([Bind(Include = "FirstName,LastName,PhoneNumber,Email,Dob")] Staff staff)
        {

            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                staff.EmployeeID = GetRandomID();
                svc.CreateStaff(staff);
                return RedirectToAction("Index");
            }

            return View(staff);
        }


        /// <summary>
        /// BINDS THE STAFF DETAILS TO THE VIEW
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DetailsStaff(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            Staff staff = svc.GetStaff(id);

            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

      
    }
}