using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class ArabaDetay
    {
        [Key]
        public int DetayId { get; set; }
        public String Sehir { get; set; }
        public int Motorgücü { get; set; }
        public int Yıl { get; set; }
        public int Vites { get; set; }
        public int Km { get; set; }
        public int Fiyat { get; set; }
        public bool Status { get; set; }
        public bool Kirada { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
