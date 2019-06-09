using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class AdminWalletAddApprovalNotificationModel
    {
        public int addRequestCount { get; set; }
        public List<AddWallet> addWalletModels { get; set; }
        public string message { get; set; }
    }

    public class AddWallet
    {
        public int addwalletid { get; set; }
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
    }
}
