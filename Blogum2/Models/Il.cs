using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogum2.Models
{
    public class Il
    {
        public int Id { get; set; }
        public string IlAd { get; set; }

        public int TurId { get; set; } //fk için

        public int IlceId { get; set; }//fk için

        public Tur Tur { get; set; } //fk

        public Ilce Ilce { get; set; }//fk
    }
}
