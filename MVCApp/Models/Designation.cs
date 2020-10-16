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
    public class Designation
    {
        public Designation()
        {
            this.DepartmentList = new List<SelectListItem>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Sort Order")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Sort Order must be greater than 0")]
        public int SortOrder { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [NotMapped]
        public List<SelectListItem> DepartmentList { get; set; }
    }
}
