using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAccess;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Utility;
using System.Data;

namespace Rocky.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository _appTypeRepo;

        public ApplicationTypeController(IApplicationTypeRepository appTypeRepo)
        {
            _appTypeRepo = appTypeRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _appTypeRepo.GetAll();
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(ApplicationType obj)
        {
            if(ModelState.IsValid)
            {
                await Task.Run(() => {
                    _appTypeRepo.Add(obj);
                    _appTypeRepo.Save();
                    
                });
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id ==0) 
            {
                return NotFound();
            }

            var obj = _appTypeRepo.Find(id.GetValueOrDefault());

            if(obj == null) {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => {
                    _appTypeRepo.Update(obj);
                    _appTypeRepo.Save(); 
                });
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _appTypeRepo.Find(id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _appTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null) {
                return NotFound();
            }

            _appTypeRepo.Remove(obj);
            _appTypeRepo.Save();

            return RedirectToAction("Index");

        }

    }
}
