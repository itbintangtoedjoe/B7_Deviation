using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class ErrorModel
    {
        public string REQ_ID { get; set; }
        public string UserLogin { get; set; }
        public string ErrorMsg { get; set; }
        public string ErrorType { get; set; }
    }
}