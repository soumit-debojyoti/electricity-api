using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class NewsFeed
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string PostDate { get; set; }
        public string ExpirationDate { get; set; }
        public int PostValidity { get; set; }
        public int FeedID { get; set; }
    }
}
