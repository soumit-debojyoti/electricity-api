using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class WalletReportResponse
    {
        public List<UserLog> user_logs { get; set; }
        public List<WalletLog> wallet_logs { get; set; }
        public List<DateLog> date_logs { get; set; }
    }

    public class UserLog
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
    }

    public class WalletLog
    {
        public int wallet_transaction_id { get; set; }
        public double transaction_amount { get; set; }
        public int transaction_initiated_user { get; set; }
        public string message { get; set; }
        public DateTime created_on { get; set; }
        public string transaction_mode { get; set; }
    }

    public class DateLog
    {
        public int month_number { get; set; }
        public string month_name { get; set; }
        public string year_name { get; set; }
    }
}
