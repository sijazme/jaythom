using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVC5.Models
{
    
    public class AnimalViewModel
    {
        public Animal Animal { get; set; }
        public IEnumerable<SelectListItem> StaffList { get; set; }

        public void Select()
        {
            var selecteditem = this.StaffList.FirstOrDefault(x => x.Value == this.Animal.StaffId.ToString());
            if (selecteditem != null)
            { selecteditem.Selected = true; }
        }

        public void SetStaffList(List<Staff> stafflist)
        {
            this.StaffList = stafflist.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.FirstName + " " + x.LastName });

        }
    }

    public class IndexVidelModel
    {
        public List<Animal> Animals { get; set; }
        public List<Staff> Staffs { get; set; }
    }

    public class Animal
    {
        public int Id { get; set; }
       
        [MaxLength(6)]
        public string AnimalID { get; set; }


        [Required]
        public string Type { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [DisplayName("Staff")]       
        public int? StaffId { get; set; }
    }

    public class Staff
    {
        public int Id { get; set; }

        [NotMapped]
        public bool Available { get; set; }

        [MaxLength(6)]
        public string EmployeeID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("Email")]
       
        public string  Email { get; set; }
    }
    public class ZooDBContext : DbContext
    {

        public ZooDBContext()
        {
            Database.SetInitializer<ZooDBContext>(null);
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        
    }
}