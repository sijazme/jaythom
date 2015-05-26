using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace MVC5
{
    public class Service : IService
    {
        public Animal GetAnimal(int? Id)
        {
            using (ZooDBContext db = new ZooDBContext())
            {
                return db.Animals.Find(Id);
            }
        }

        public List<Animal> GetAnimals()
        {
            using (ZooDBContext db = new ZooDBContext())
            {
                return db.Animals.ToList();
            }
        }

        public void UpdateAnimal(Animal animal)
        {

            using (ZooDBContext db = new ZooDBContext())
            {

                db.Entry(animal).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public void AddAnimal(Animal animal)
        {
            using (ZooDBContext db = new ZooDBContext())
            {                
                db.Animals.Add(animal);
                db.SaveChanges();
            }
        }

       



        public void DeleteAnimal(int id)
        {
            using (ZooDBContext db = new ZooDBContext())
            {
                Animal animal = db.Animals.Find(id);
                db.Animals.Remove(animal);
                db.SaveChanges();
            }
        }



        public List<Staff> GetAvailableStaff()
        {
            using (ZooDBContext db = new ZooDBContext())
            {
                var unavailable_staff_ids = db.Animals.Select(x => x.StaffId).Distinct().ToArray();
                return db.Staffs.Where(p1 => !unavailable_staff_ids.Any(p2 => p2 == p1.Id)).ToList();
            }
        }

        public void UpdateStaff(Staff staff)
        {
            using (ZooDBContext db = new ZooDBContext())
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteStaff(int id)
        {
            using (ZooDBContext db = new ZooDBContext())
            {
                Animal animal = db.Animals.Where(x => x.StaffId == id).SingleOrDefault();
                if (animal != null)
                {
                    animal.StaffId = null;
                    db.Entry(animal).State = EntityState.Modified;
                }

                Staff staff = db.Staffs.Find(id);
                db.Staffs.Remove(staff);
                db.SaveChanges();
            }
        }

        public void CreateStaff(Staff staff)
        {
            using (ZooDBContext db = new ZooDBContext())
            {                
                db.Staffs.Add(staff);
                db.SaveChanges();
            }
        }

        public List<Staff> GetStaffs()
        {
            using (ZooDBContext db = new ZooDBContext())
            {
                var stafflist = db.Staffs.ToList();
                var unavailable_staff_ids = db.Animals.Select(x => x.StaffId).Distinct().ToArray();
                foreach (var s in stafflist)                
                        s.Available = !unavailable_staff_ids.Contains(s.Id);
                return stafflist;
            }
        }

        public Staff GetStaff(int? id)
        {
            using (ZooDBContext db = new ZooDBContext())
            {
                return db.Staffs.Find(id);
            }
        }


    }
}
