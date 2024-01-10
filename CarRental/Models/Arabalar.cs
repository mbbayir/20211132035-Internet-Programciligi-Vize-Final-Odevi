using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Arabalar
    {
        [Key]
        public int ArabaId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Title { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Keywords { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Description { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(40)]
        public string Marka { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(40)]
        public string Image { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Model { get; set; }


        public string Detail { get; set; }

        [ForeignKey("ArabaDetay")]
        public int DetayId { get; set; }
        public ArabaDetay ArabaDetay { get; set; }
        public bool Status { get; internal set; }
    }
}
