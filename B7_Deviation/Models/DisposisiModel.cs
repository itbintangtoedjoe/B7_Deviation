using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class DisposisiModel
    {
        public string REQ_ID { get; set; }
        public string DEV_ID { get; set; }
        public string USER_NIK { get; set; }
        public string KETERANGAN_DISPOSISI{ get; set; }
        public string PIC_REMEDIAL_NIK { get; set; }
        public string PIC_REMEDIAL_NAME { get; set; }
        public string GROUP_SITE { get; set; }
        public DateTime DUE_DATE { get; set; }
        public string NO_DISPOSISI { get; set; }
        public string CURR_USER { get; set; }
        public string KETERANGAN_REJECT { get; set; }
        public long TOTAL_COST { get; set; }
        public string STATUS { get; set; }
    }
}