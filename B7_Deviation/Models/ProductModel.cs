using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class ProductModel
    {
        public string ReqID { get; set; }
        public string ItemCodeOracle { get; set; }
        public string NoBatchOracle { get; set; }
        public string NoWOOracle { get; set; }
        public string NoQCMaterial { get; set; }
        public string KeteranganKategori { get; set; }
    }
}