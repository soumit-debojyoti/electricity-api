using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class Rank
    {
        public int child { get; set; }
        public List<int> ids { get; set; }
    }
    public class AchieverList
    {
        public int user_id { get; set; }
        public int rank { get; set; }
    }
}
