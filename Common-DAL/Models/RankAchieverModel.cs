using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class RankAchieverModel
    {
        public OwnModel self { get; set; }
        public ParentModel parent { get; set; }
        public List<ChildModel> children { get; set; }
        public List<SiblingModel> siblings { get; set; }
    }

    public class OwnModel
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public string role_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string sex { get; set; }
        public string father_name { get; set; }
        public DateTime dob { get; set; }
        public string mobile_number { get; set; }
    }

    public class ParentModel
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public string role_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string sex { get; set; }
        public string father_name { get; set; }
        public DateTime dob { get; set; }
        public string mobile_number { get; set; }
    }

    public class ChildModel
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public string role_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string sex { get; set; }
        public string father_name { get; set; }
        public DateTime dob { get; set; }
        public string mobile_number { get; set; }
    }

    public class SiblingModel
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public string role_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string sex { get; set; }
        public string father_name { get; set; }
        public DateTime dob { get; set; }
        public string mobile_number { get; set; }
    }
}
