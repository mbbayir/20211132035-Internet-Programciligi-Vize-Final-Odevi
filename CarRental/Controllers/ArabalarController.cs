using AspNetCoreHero.ToastNotification.Abstractions;
using CarRental.Models;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace CarRental.Controllers
{
    public class ArabalarController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly IFileProvider _fileProvider;

        public ArabalarController(AppDbContext context, IConfiguration configuration, INotyfService notyfService, IFileProvider fileProvider)
        {
            _context = context;
            _configuration = configuration;
            _notyfService = notyfService;
            _fileProvider = fileProvider;
        }
        public IActionResult Index()
        {
            var arabaModels = _context.Arabalars
                .Select(x => new ArabalarModel

                {
                    ArabaId = x.ArabaId,
                    Title = x.Title,
                    Marka = x.Marka,
                    Model = x.Model
                })
                .ToList();

            return View(arabaModels);
        }

        public IActionResult ArabaEkle()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ArabaEkle(ArabalarModel model)
        {
            var rootfolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "-";
            //if (model.Resimss.Length > 0 && model.Resimss != null)
            //{
            //    var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Resimss.FileName);
            //    var photoPath = Path.Combine(rootfolder.First(x => x.Name == "Araç").PhysicalPath, filename);
            //    using var stream = new FileStream(photoPath, FileMode.Create);
            //    model.Resimss.CopyTo(stream);
            //    photoUrl = filename;
            //}


            var arabalar = new Arabalar();
            arabalar.Title = model.Title;
            arabalar.Keywords = model.Keywords;
            arabalar.Description = model.Description;
            arabalar.Marka = model.Marka;
            arabalar.Image = photoUrl;
            arabalar.Model = model.Model;

            _context.Arabalars.Add(arabalar);
            _context.SaveChanges();

            return RedirectToAction("Index", "Arabalar");




        }

        [HttpPost]
        public async Task<IActionResult> ArabaSil(int id)
        {

            var arabalar = await _context.Arabalars.FindAsync(id);

            if (arabalar != null)
            {
                _context.Arabalars.Remove(arabalar);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Araba başarıyla silindi." });
            }
            else
            {
                return Json(new { success = false, message = "Belirtilen ID'ye sahip araba bulunamadı." });
            }


        }


        [HttpPost]
        public async Task<IActionResult> ArabaGuncelle(ArabalarModel model)
        {
            var arabalar = await _context.Arabalars.FindAsync(model.ArabaId);
            if (arabalar != null)
            {
                arabalar.Title = model.Title;
                arabalar.Keywords = model.Keywords;
                arabalar.Description = model.Description;
                arabalar.Marka = model.Marka;

                _context.Arabalars.Update(arabalar);

                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Araba başarıyla güncellendi." });
            }
            else
            {
                return Json(new { success = false, message = "Belirtilen ID'ye sahip araba bulunamadı." });
            }


        }

    }
}
