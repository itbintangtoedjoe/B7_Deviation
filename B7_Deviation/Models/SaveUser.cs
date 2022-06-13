using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class SaveUser
    {
        public string Empid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Empname { get; set; }
        public string Jobttlname { get; set; }
        public string Superiorid { get; set; }
        public string Superiorname { get; set; }
        public string Orggroupname { get; set; }
        public string Emailsuperior { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string KategoriPenyimpangan { get; set; }
        public string Option { get; set; }

    }
}