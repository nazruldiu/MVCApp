using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Data;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDBContext _db;
        public DepartmentController(AppDBContext appDBContext)
        {
            _db = appDBContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Department> departmentList = _db.Department.OrderBy(x=>x.SortOrder);
            return View(departmentList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            _db.Department.Add(department);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var department = _db.Department.Find(id);
            if(department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            _db.Department.Update(department);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var department = _db.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var department = _db.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            _db.Department.Remove(department);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
