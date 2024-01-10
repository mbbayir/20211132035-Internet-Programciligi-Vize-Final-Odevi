using CarRental.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRental.Models
{
    public class AppDbContext : IdentityDbContext<User,AppRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Kategori> Kategoris { get; set; }

        public DbSet<ArabaDetay> ArabaDetays { get; set; }

        public DbSet<Arabalar> Arabalars { get; set; }
        
        public DbSet<Iletisim> Iletisims { get; set; }


    }
}