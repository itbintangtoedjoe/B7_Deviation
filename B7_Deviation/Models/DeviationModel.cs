using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_Deviation.Models
{
    public class DeviationModel
    {
        public string ReqID { get; set; }
        public string FileName { get; set; }
        public string PathFile { get; set; }

        public string CrDateUser { get; set; }
        public string IdProposer { get; set; }
        public string Departement { get; set; }
        public string Problem { get; set; }
        public string Date_of_incident { get; set; }
        public string LocationSite { get; set; }
        public string DeviationCategory { get; set; }
        public string DeviationType { get; set; }
        public string KetCategory { get; set; }
        public string Location { get; set; }
        public string LocationSiteIncident { get; set; }
        public string LocationDeptIncident { get; set; }
        public string UserInvolved { get; set; }
        public string OrderOfEvents { get; set; }
        public string SamePotenDevFlag { get; set; }
        public string SamePotennDev { get; set; }
        public string PotenDevRlsFlg { get; set; }
        public string PotenDevRls { get; set; }
        public string PotenDevOTHFlg { get; set; }
        public string PotenDevOTH { get; set; }
        public string ActionWhenDev { get; set; }

        public string QualityProduct { get; set; }
        public string Compliance { get; set; }
        public string RiskOperasional { get; set; }
        public string RiskFinancial { get; set; }
        public string RiskOrganization { get; set; }
        public string RiskSecurity { get; set; }
        public string RiskHealty { get; set; }
        public string RiskEnvirontment { get; set; }
        public string RiskIntellectual { get; set; }
        public string SevertyDeviation { get; set; }
        public string DeviationNo { get; set; }
        public string RecordID { get; set; }
        public string FLAG_KOOR { get; set; }
        public string USERNIK { get; set; }
        public string OPTION { get; set; }

        public string PLAN_DEV { get; set; }
        public string THIRTY { get; set; }
        public string THIRTY_FLAG { get; set; }
        public string REQ { get; set; }
        public string NO_WO_ORACLE { get; set; }
        public string UsulanRemidial { get; set; }
        public string Item_Code_Oracle { get; set; }
        public string No_Batch_Oracle { get; set; }
        public string FlagReceipt { get; set; }        
        public string QCMaterialManufacturerNo { get; set; }
        public string NO_DISPOSISI { get; set; }

        //untuk pic
        public string Group { get; set; }
        public string GroupSite { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}