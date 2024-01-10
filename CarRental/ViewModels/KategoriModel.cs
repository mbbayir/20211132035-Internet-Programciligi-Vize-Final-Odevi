using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.ViewModels
{
    public class KategoriModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
    }
}