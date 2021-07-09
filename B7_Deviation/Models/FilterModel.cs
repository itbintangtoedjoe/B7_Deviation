using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class FilterModel
    {
        public string StatusPenyimpangan { get; set; }
        public string DepartemenPelapor { get; set; }
        public string KategoriPenyimpangan { get; set; }
        public string JenisPenyimpangan { get; set; }
        public string TahunPelaporan { get; set; }
    }
}