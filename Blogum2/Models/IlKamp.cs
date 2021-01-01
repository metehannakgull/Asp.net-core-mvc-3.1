using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogum2.Models
{
    public class IlKamp
    {
        public int Id { get; set; }

        public int IlId { get; set; }
        public int KampYeriId { get; set; }

        public Il Il { get; set; }
        public KampYeri KampYeri { get; set; }

    }
}
