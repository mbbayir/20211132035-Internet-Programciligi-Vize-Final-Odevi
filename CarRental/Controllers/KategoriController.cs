using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CarRental.Models;
using CarRental.ViewModels;

namespace Tagain2.Controllers.Home
{
    public class KategoriController : Controller
    {
        private readonly AppDbContext _context;

        public KategoriController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KategoriListAjax()
        {
            var kategoriModels = _context.Kategoris.Select(x => new KategoriModel()
            {
                Id = x.KategoriId,
                Title = x.Title,
                Status = x.Status,
            }).ToList();

            return Json(kategoriModels);
        }

        public IActionResult KategoriByIdAjax(int id)
        {
            var kategoriModel = _context.Kategoris.Where(s => s.KategoriId == id).Select(x => new KategoriModel()
            {
                Id = x.KategoriId,
                Title = x.Title,
                Status = x.Status,
            }).SingleOrDefault();

            return Json(kategoriModel);
        }

        [HttpPost]
        public IActionResult KategoriAddAjax(KategoriModel model)
        {
          
                var kategori = new Kategori();
                kategori.Title = model.Title;
                kategori.Status = true;
                kategori.Sinif = "arac";
                kategori.Aciklama = "";
                _context.Kategoris.Add(kategori);

                _context.SaveChanges();

                return Json(true);
         
        }

        [HttpPost]
        public IActionResult KategoriEditAjax(KategoriModel model)
        {
            if (ModelState.IsValid)
            {
                var kategori = _context.Kategoris.Find(model.Id);
                kategori!.Title = model.Title;
                kategori.Status = model.Status;
                kategori.Sinif = "arac";
                kategori.Aciklama = "";
                _context.Kategoris.Update(kategori);

                _context.SaveChanges();

                return Json(true);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return Json(new { success = false, errors = errors });
            }
        }



     //  [Route("/Kategori/Remove/{id}")]
        public IActionResult KategoriRemoveAjax(int id)
        {
            var kategori = _context.Kategoris.FirstOrDefault(x => x.KategoriId == id);
            _context.Kategoris.Remove(kategori!);
            _context.SaveChanges();

            return Json(true);

        }
    }
}