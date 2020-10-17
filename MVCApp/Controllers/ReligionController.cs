using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Data;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class ReligionController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public ReligionController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Religion> objList = _unitOfWork.ReligionRepository.GetAll();
            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Religion model)
        {
            _unitOfWork.ReligionRepository.Add(model);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = _unitOfWork.ReligionRepository.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Religion model)
        {
            _unitOfWork.ReligionRepository.Update(model);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = _unitOfWork.ReligionRepository.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            var model = _unitOfWork.ReligionRepository.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            _unitOfWork.ReligionRepository.Delete(model);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

    }
}
