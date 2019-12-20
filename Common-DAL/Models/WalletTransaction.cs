using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class WalletTransaction
    {
        public int WalletTransactionID { get; set; }
        public decimal TransactionAmount { get; set; }
        public string UserName { get; set; }
        public string TransactionMessage { get; set; }
        public DateTime TransactionTime { get; set; }
        public string TransactionMode { get; set; }
        public string TransactionByFirstName { get; set; }
        public string TransactionByLastName { get; set; }
        public string MobileNumber { get; set; }
        public int UserID { get; set; }
    }
}
