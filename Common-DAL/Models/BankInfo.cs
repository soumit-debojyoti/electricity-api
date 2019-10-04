using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class BankInfo
    {
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IfscCode { get; set; }
        public string AccountNumber { get; set; }
    }
}
