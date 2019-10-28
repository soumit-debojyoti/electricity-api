using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class CommissionSettingValue
    {
        public string RechargeType { get; set; }
        public string OperatorName { get; set; }
        public string CommissionType { get; set; }
        public string CalculationType { get; set; }
        public decimal Value { get; set; }
        public string LevelPayoutType { get; set; }
        public decimal LevelPayoutValue { get; set; }
    }
}
