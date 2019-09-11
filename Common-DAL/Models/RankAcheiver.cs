using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class RankAcheiver
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserRank { get; set; }
        public DateTime RankAchievementDate { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
