using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class ArabalarModel
    {
        public int ArabaId { get; set; }

    
        public string Title { get; set; }

        public string Keywords { get; set; }

        
        public string Description { get; set; }

       
        public string Marka { get; set; }

       
        public string Image { get; set; }

        
        public string Model { get; set; }

        public IFormFile Resimss { get; set; }


        public string Detail { get; set; }


        public int DetayId { get; set; }
    }
}
