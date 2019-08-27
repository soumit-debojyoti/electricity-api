using System;
using System.Collections.Generic;

namespace Electricity_DAL.Models
{
    public class AdminApprovalNotificationModel
    {
        public int withdrawalRequestCount { get; set; }
        public List<WithdrawalWalletModel> withdrawalWalletModels { get; set; }
        public string message { get; set; }
    }

    public class WithdrawalWalletModel
    {
        public int withdrawalid { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string comment { get; set; }
        public DateTime createdon { get; set; }
        public bool approved { get; set; }
        public bool rejected { get; set; }
        public string admin_comment { get; set; }
        public DateTime approved_on { get; set; }
        public string message { get; set; }
        public int request_initiator_id { get; set; }
        public decimal wallet_balance { get; set; }
        public string balance_request_type { get; set; }
    }
}
