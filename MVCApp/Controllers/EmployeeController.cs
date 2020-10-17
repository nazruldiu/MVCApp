using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCApp.Data;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDBContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public EmployeeController(AppDBContext appDBContext, IWebHostEnvironment hostEnvironment)
        {
            _db = appDBContext;
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Employee> objList = _db.Employee.Include(x=>x.Department).Include(a => a.Designation);        
            return View(objList);
        }

        public IActionResult Create()
        {
            Employee employee = new Employee
            {
                DateofBirth = DateTime.Now
            };
            PopulateDDl(employee);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    string uniqueFileName = UploadedFile(model);
                    model.ImageFileName = uniqueFileName;
                }
                _db.Employee.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        private string UploadedFile(Employee model)
        {
            string uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.ImageFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = _db.Employee.Find(id);
            PopulateDDl(model);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.ImageFileName != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ImageFileName);
                        System.IO.File.Delete(filePath);
                    }
                    string uniqueFileName = UploadedFile(model);
                    model.ImageFileName = uniqueFileName;
                }
                _db.Employee.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = _db.Employee.Find(id);
            PopulateDDl(model);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var model = _db.Employee.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            if (model.ImageFileName != null)
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ImageFileName);
                System.IO.File.Delete(filePath);
            }
            _db.Employee.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public Employee PopulateDDl(Employee employee)
        {
            employee.DepartmentList = _db.Department.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            employee.DesignationList = _db.Designation.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return employee;
        }
        public IActionResult GetDesignationByDepartmentId(int? id)
        {
            var DesignatonList = _db.Designation.Where(s=>s.DepartmentId == id).Select(x => new 
                                  SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return Json(DesignatonList);
        }
    }
}
