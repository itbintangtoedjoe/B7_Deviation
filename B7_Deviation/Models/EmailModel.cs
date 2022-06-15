using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class EmailModel
    {
        public string ReqID { get; set; }
        public string EmailReceiver { get; set; }
        public string Receiver { get; set; }
        public string EmailSender { get; set; }
        public string Sender { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }




        // SORT BASED ON RECEIVER
        public string WhoReceiver { get; set; }
        public string TableType { get; set; }

        //KHUSUS DISPOSISI
        public string Urutan { get; set; }
    }
}