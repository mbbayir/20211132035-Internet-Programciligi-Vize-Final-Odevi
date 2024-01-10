using AspNetCoreHero.ToastNotification.Abstractions;
using CarRental.Models;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace CarRental.Controllers
{

    //[Authorize(Roles = "User,Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly INotyfService _notify;
        private readonly IConfiguration _config;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public AdminController(ILogger<HomeController> logger = null, AppDbContext context = null, INotyfService notify = null, IConfiguration config = null, IFileProvider fileProvider = null, UserManager<User> userManager = null, RoleManager<AppRole> roleManager = null, SignInManager<User> signInManager = null)
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
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }
        public IActionResult RoleAdd()
        {
            return View();
        }
        public async Task<IActionResult> GetUserList()
        {
            var userModels = await _userManager.Users.Select(x => new UserModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                UserName = x.UserName,
                City = x.City,

            }).ToListAsync();

            return View(userModels);
        }
        public async Task<IActionResult> GetRoleList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> RoleAdd(AppRole model)
        {
            var role = await _roleManager.FindByNameAsync(model.Name);
            if (role == null)
            {
                var newrole = new AppRole();
                newrole.Name = model.Name;
                await _roleManager.CreateAsync(newrole);
            }
            return RedirectToAction("GetRoleList");
        }

        [HttpGet]
        public IActionResult Iletisims()
        {
            var iletisim = _context.Iletisims.Select(x => new IletisimModel()
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.Name,
                Surname = x.Surname,
                Message = x.Message,

            }).ToList();
            return View(iletisim);
        }
        public IActionResult IletisimDelete(int ıd)
        {
            var iletisimdelete = _context.Iletisims.Where(x => x.Id == ıd).FirstOrDefault();
            _context.Iletisims.Remove(iletisimdelete);
            _context.SaveChanges();
            return RedirectToAction("Iletisims", "Admin");
        }
        [HttpPost]
        public IActionResult AjaxMethod(string parameter)
        {

            return Json(new { result = "success", message = "İşlem başarılı!" });
        }

        public async Task<IActionResult> Logout()
        {
            
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Home");
        }
    }
}
