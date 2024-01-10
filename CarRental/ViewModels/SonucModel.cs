using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.ViewModels
{
    public class SonucModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}