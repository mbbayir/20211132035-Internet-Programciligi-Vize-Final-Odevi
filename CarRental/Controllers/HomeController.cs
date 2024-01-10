using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarRental.Models;
using CarRental.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using AspNetCoreHero.ToastNotification.Notyf.Models;
using System.Security.Claims;
using NETCore.Encrypt;
using Microsoft.Extensions.FileProviders;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.IO;

namespace CarRental.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly INotyfService _notify;
        private readonly IConfiguration _config;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
       
        public HomeController(ILogger<HomeController> logger, AppDbContext context, INotyfService notify, IConfiguration config, IFileProvider fileProvider,UserManager<User> userManager, SignInManager<User> signInManager ,RoleManager<AppRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _notify = notify;
            _config = config;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user =await _userManager.FindByNameAsync(model.UserName);
            if(user==null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz Kullanıcı Adi");
                return View();        
            }
            var sigInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (sigInResult.Succeeded)
            {
                return RedirectToAction("Index","Admin");
            }
            if (sigInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Kullanıcı Girişi" + user.LockoutEnd + "Kadar kısıtlanmıştır!");
            }
            ModelState.AddModelError("", "Geçersiz Kullanıcı adı veya Parola Yanlış");
            
            return View();
        }

        //[AllowAnonymous]
        //public IActionResult GoogleLogin()
        //{
        //    var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        //    return Challenge(properties, "Google");
        //}

        //public async Task<IActionResult> GoogleResponse()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    if (result.Succeeded)
        //    {
        //        var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
        //        var user=await userManager.GetUserAsync(User);

        //        var userId = user.Id;
        //        var userEmail=user.Email;

        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("Login");
        //}
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var rootfolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "-";
            if (model.PhotoFile.Length > 0 && model.PhotoFile != null)
            {   
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.PhotoFile.FileName);
                var photoPath = Path.Combine(rootfolder.First(x => x.Name == "Uye").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.PhotoFile.CopyTo(stream);
                photoUrl = filename;
            }

            var user = new User
            {
                UserName=model.UserName,
                Email=model.Email,
                City=model.City,
                FullName=model.FullName,
                PhotoUrl = photoUrl
            };

            var identityResult = await _userManager.CreateAsync(user, model.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {

                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }


            //Uye Role Ekleme
            var roleExit = await _roleManager.RoleExistsAsync("Uye");
            if (!roleExit)
            {
                var role = new AppRole { Name = "Uye" };
                await _roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(user, "Uye");
            return RedirectToAction("Login");
        }

        public IActionResult AccesDenied()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        
        public IActionResult Iletisim()
        {
            return View();
        }

        
        public IActionResult IletisimBilgi(IletisimModel model)
        {
            var kaydet = new Iletisim();
            kaydet.Name = model.Name;
            kaydet.Email = model.Email;
            kaydet.Surname = model.Surname;
            kaydet.Message = model.Message;
            _context.Iletisims.Add(kaydet);
            _context.SaveChanges();
            return Json(new {success= true});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult aracgetir()
        {
            var arabalar = _context.Arabalars.Select(x => new ArabalarModel()
            {
                Title = x.Title,
                Description = x.Description,
                Image = x.Image,
                Marka = x.Marka,
            }).ToList();
            return View(arabalar);
        }
        
        public async Task<IActionResult> UserPage()
        {
            var username = User.Identity!.Name;
           
            var user = await _context.Users.Where(x=>x.UserName == username!.ToString()).Select(x => new User()
            {
                FullName = x.FullName,
                Email = x.Email,
                UserName = x.UserName,
                PhotoUrl = x.PhotoUrl
            }).ToListAsync();
            return View(user);
        }

        public IActionResult Hakkimizda()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    
}