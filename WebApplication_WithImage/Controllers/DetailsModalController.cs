using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication_WithImage.Data;
using WebApplication_WithImage.Models;

namespace WebApplication_WithImage.Controllers
{
    public class DetailsModalController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _environment;

        public DetailsModalController(AppDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: DetailsModal/Index
        public IActionResult Index()
        {
            var data =  _context.detailsModals.ToList();
            return View(data);
        }

        public IActionResult Details(int Id)
        {
            var data =_context.detailsModals.FirstOrDefault(x=> x.Id==Id);
            return View(data);
        }


        // GET: DetailsModal/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetailsModal/Create
        [HttpPost]
        public IActionResult Create(DetailsModal modal)
        {
            
            try
            {
                if (modal!=null)
                {
                    string uniquieFileName = UploadImage(modal);
                    var data = new DetailsModal()
                    {
                       
                        Title = modal.Title, 
                        Name= modal.Name,
                        DocumentName = modal.DocumentName,
                        Path=uniquieFileName

                    };

                    _context.detailsModals.Add(data);
                    _context.SaveChanges();
                    TempData["Success"] = "Succcessfully saved";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Nowt saved..... modal property is not valid");

            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View (modal);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _context.detailsModals.FirstOrDefault(x=> x.Id==id);
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, DetailsModal detailsModal)
        {
            try
            {
                var data = _context.detailsModals.FirstOrDefault(x => x.Id == id);
                if (detailsModal != null)
                {
                    data.Title = detailsModal.Title;
                    data.Name = detailsModal.Name;
                    data.DocumentName = detailsModal.DocumentName;
                    data.Title = detailsModal.Title;

                    if (detailsModal.ImagePath != null)
                    {
                        var uniquiePic = UploadImage(detailsModal);
                        data.Path = uniquiePic;

                    }

                    _context.detailsModals.Add(data);
                    _context.SaveChanges();
                    TempData["Success"] = "Succcessfully updated";
                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, "Update Fail....");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return View();
        }

        // GET: DetailsModal/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var modal = _context.detailsModals.Find(id);
            if (modal == null)
            {
                return NotFound();
            }
            return View(modal);
        }

        // POST: DetailsModal/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var modal = _context.detailsModals.Find(id);
            if (modal != null)
            {
                _context.detailsModals.Remove(modal);
                _context.SaveChanges();
                TempData["Success"] = "Successfully deleted";
            }
            return RedirectToAction("Index");
        }



        private string UploadImage(DetailsModal modal)
        {
            string uniqueFileName = string.Empty;

            if (modal.ImagePath != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + modal.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    modal.ImagePath.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }


    }
}
