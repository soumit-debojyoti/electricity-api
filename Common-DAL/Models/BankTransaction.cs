using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class BankTransaction
    {
        public int BankTransactionID { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public bool Verified { get; set; }
        public bool Received { get; set; }
        public int walletTransactionID { get; set; }
    }
}
