using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class Employee
    {
        public Employee()
        {
            this.DepartmentList = new List<SelectListItem>();
            this.DesignationList = new List<SelectListItem>();
        }
        [Key]
        public int Id { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Date of Birth")]
        [Required]
        public DateTime DateofBirth { get; set; }
        [DisplayName("Department")]
        [Required]
        public int DepartmentId { get; set; }
        [DisplayName("Designation")]
        [Required]
        public int DesignationId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
        [DisplayName("Image")]
        public string ImageFileName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public List<SelectListItem> DepartmentList { get; set; }
        [NotMapped]
        public List<SelectListItem> DesignationList { get; set; }
    }
}
