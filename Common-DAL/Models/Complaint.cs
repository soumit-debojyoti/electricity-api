using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class Complaint
    {
        public int CID { get; set; }
        public int TID { get; set; }
        public int CPriority { get; set; }
        public int RaisedBy { get; set; }
        public string UserContactNumber { get; set; }
        public string UserComment { get; set; }
        public int CStatus { get; set; }
        public string ResolverComment { get; set; }
        public int AssignedTo { get; set; }
        public int ResolvedBy { get; set; }
        public string  AssignedToName { get; set; }
        public string CPriorityText { get; set; }
        public string CStatusText { get; set; }
        public string RaisedByText { get; set; }
        public RechargeTransaction Transaction { get; set; }
        public Complaint()
        {
            this.Transaction = new RechargeTransaction();
        }
    }
}
