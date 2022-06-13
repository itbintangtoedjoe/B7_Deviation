using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class ApprovalModel
    {
        public string REQID { get; set; }
        public string KETERANGAN { get; set; }
        public string IDUSER { get; set; }
        public string Option { get; set; }
        public string ITDP { get; set; }
        public string ITJP { get; set; }
        public string MTDP { get; set; }
        public string MTJP { get; set; }
        public string EvaluasiResiko { get; set; }
        public string NoPenyimpangan { get; set; }

        public string NoDisposisi { get; set; }

        public string PICName { get; set; }

    }
}