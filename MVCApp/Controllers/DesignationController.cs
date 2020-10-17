using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCApp.Data;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class DesignationController : Controller
    {
        private readonly AppDBContext _db;
        public DesignationController(AppDBContext appDBContext)
        {
            _db = appDBContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Designation> objList = _db.Designation.OrderBy(x => x.SortOrder);
            return View(objList);
        }
        public IActionResult Create()
        {
            Designation designation = new Designation
            {
                DepartmentList = _db.Department.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
            };
            return View(designation);
        }
        [HttpPost]
        public IActionResult Create(Designation designation)
        {
            _db.Designation.Add(designation);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var designation = _db.Designation.Find(id);
            designation.DepartmentList = _db.Department.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }
        [HttpPost]
        public IActionResult Edit(Designation designation)
        {
            _db.Designation.Update(designation);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var department = _db.Designation.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var department = _db.Designation.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            _db.Designation.Remove(department);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
