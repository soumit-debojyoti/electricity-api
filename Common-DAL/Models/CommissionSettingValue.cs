using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class CommissionSettingValue
    {
        /// <summary>
        /// Transation Setting ID :: either commission setting or servie charge setting
        /// </summary>
        public int T_ID { get; set; }
        public int L_ID { get; set; }
        public int CS_ID { get; set; }
        public string RechargeType { get; set; }
        public string OperatorName { get; set; }
        public string CommissionType { get; set; }
        public string CalculationType { get; set; }
        public decimal Value { get; set; }
        public string LevelPayoutType { get; set; }
        public decimal LevelPayoutValue { get; set; }
        public int Ref_CS_ID { get; set; }
    }
}
