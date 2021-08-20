using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class PICModel
    {
        public string REQID { get; set; }
        public string IDUSER { get; set; }
        public string Option { get; set; }
        public string HasilTindakan { get; set; }
        public string TestDP { get; set; }
        public string TestJP { get; set; }
        public string TestMV { get; set; }
        public string TestCA { get; set; }
        public string ProcDP { get; set; }
        public string ProcJP { get; set; }
        public string ProcMV { get; set; }
        public string ProcM { get; set; }
        public string ProcQ { get; set; }
        public string ProcCM { get; set; }
        public string RejM { get; set; }
        public string RejQ { get; set; }
        public string RejCM { get; set; }
        public string McDP { get; set; }
        public string McJT { get; set; }
        public string McMV { get; set; }
        public string McNM { get; set; }
        public string McMH { get; set; }
        public string McS { get; set; }
        public string McHS { get; set; }
        public string HiddenCostType { get; set; }
    }
}