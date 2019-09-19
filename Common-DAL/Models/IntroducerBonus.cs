using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class IntroducerBonus
    {
        public int IntroducerLevel { get; set; }
        public decimal ReferralBonus { get; set; }
        public decimal MonthlyPayout { get; set; }
    }
}
