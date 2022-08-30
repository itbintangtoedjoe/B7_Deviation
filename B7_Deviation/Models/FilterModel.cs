using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class FilterModel
    {
        // Master List & Find Deviation
        public string StatusPenyimpangan { get; set; }
        public string DepartemenPelapor { get; set; }
        public string KategoriPenyimpangan { get; set; }
        public string JenisPenyimpangan { get; set; }
        public string TahunPelaporan { get; set; }
        public string SitePenyimpangan { get; set; }
        // Lead Time
        public string BulanPelaporan { get; set; }
       


    }
}