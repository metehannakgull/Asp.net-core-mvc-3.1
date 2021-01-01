using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogum2.Models
{
    public class IlSehirYeri
    {
        public int Id { get; set; }

        public int IlId { get; set; }
        public int SehirdekiYerId { get; set; }

        public Il Il { get; set; }
        public SehirdekiYer SehirdekiYer { get; set; }
    }
}
