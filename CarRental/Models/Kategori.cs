using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }

        public string Sinif { get; set; }

        public string Title { get; set; }

        public string Aciklama { get; set; }

        public bool Status { get; set; }

        // Arabalar sınıfının doğru using bildirimiyle eklenmiş bir ICollection örneği
        public ICollection<Arabalar> Cars { get; set; }
    }
}
