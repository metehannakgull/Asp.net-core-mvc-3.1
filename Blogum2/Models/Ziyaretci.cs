using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogum2.Models
{
    public class Ziyaretci : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }

       
        public DateTime DogumTarihi { get; set; }
    }
}
