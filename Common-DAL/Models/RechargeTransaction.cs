using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class RechargeTransaction
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionMode { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionMessage { get; set; }
    }
}
