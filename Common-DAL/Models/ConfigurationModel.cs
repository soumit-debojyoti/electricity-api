﻿namespace Electricity_DAL.Models
{
    public class ConfigurationModel
    {
        public double referal_wallet_balance_deduct_amount { get; set; }
        public int down_side_direct_numer_of_joinee { get; set; }
        public int down_side_direct_of_joinee_point { get; set; }
        public double point_unit_price { get; set; }
        public double first_registration_wallet_balance { get; set; }
        public int wallet_approver_role { get; set; }
        public int kyc_submission_days { get; set; }
    }
}
