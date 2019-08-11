using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class TokenDetailsGeneric
    {
        public string token { get; set; }
        public DateTime created_date { get; set; }
        public bool is_expired { get; set; }
    }

    public class TokenDetailsSpecific
    {
        public string token { get; set; }
        public DateTime created_date { get; set; }
        public DateTime expiry_date { get; set; }
        public string token_generator { get; set; }
        public bool is_expired { get; set; }
    }
}
