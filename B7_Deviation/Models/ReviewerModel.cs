using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class ReviewerModel
    {
        public string REQID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_NIK { get; set; }
        public string KETERANGAN_REVIEW { get; set; }
        public string KETERANGAN_REJECT { get; set; }
        public string Option { get; set; }
        public string LOGIN_USER { get; set; }
    }
}