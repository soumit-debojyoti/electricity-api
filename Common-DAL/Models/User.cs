using System;

namespace Electricity_DAL.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string security_stamp { get; set; }
        public string role_name { get; set; }
        public int role_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string father_name { get; set; }
        public string dob { get; set; }
        public string mobile_number { get; set; }
        public string pan_card { get; set; }
        public string aadhar_card { get; set; }
        public string address { get; set; }
        public string post_office { get; set; }
        public string police_station { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state_name { get; set; }
        public string pin { get; set; }
        public string sex { get; set; }
        public string photo { get; set; }
    }

    public class RegisterUser
    {
        public string introcode { get; set; }
        public string introname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string fathername { get; set; }
        public int gender { get; set; }
        public string dob { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string pancard { get; set; }
        public string aadharcard { get; set; }
        public string address { get; set; }
        public string po { get; set; }
        public string ps { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public int state { get; set; }
        public string pincode { get; set; }
        public string bankname { get; set; }
        public string accholdername { get; set; }
        public string accnumber { get; set; }
        public string ifsc { get; set; }
        public string branch { get; set; }

        public bool isKYCLater { get; set; }

        public int idprooftype { get; set; }
        public string idproof { get; set; }
        public int addressprooftype { get; set; }
        public string addressproof { get; set; }
        public string photo { get; set; }
        public string bankdetails { get; set; }
        public bool payonline { get; set; }

        public bool is_employee { get; set; }
    }

    public class UserDetails
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public int role_id { get; set; }
        public string email { get; set; }
        public string security_stamp { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string father_name { get; set; }
        public string dob { get; set; }
        public string mobile_number { get; set; }
        public string pan_card { get; set; }
        public string aadhar_card { get; set; }
        public string address { get; set; }
        public string post_office { get; set; }
        public string police_station { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public int state { get; set; }
        public string pin { get; set; }
        public string sex { get; set; }
        public string middle_name { get; set; }
        public int bank_detail_id { get; set; }
        public int kyc_detail_id { get; set; }

        public string introcode { get; set; }
        public string introname { get; set; }
        public string photo { get; set; }
        public bool is_employee { get; set; }


    }

    public class BankDetails
    {
        public int bank_detail_id { get; set; }
        public string bank_name { get; set; }
        public string account_holder_name { get; set; }
        public string account_number { get; set; }
        public string ifsc_number { get; set; }
        public string branch_name { get; set; }
        public bool is_pay_online { get; set; }
    }

    public class KYCDetails
    {
        public int kyc_detail_id { get; set; }
        public int id_proof_id { get; set; }
        public string id_proof_document_path { get; set; }
        public int address_proof_id { get; set; }
        public string address_proof_document_path { get; set; }
        public string bank_details { get; set; }
        public DateTime created_on { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Trans = 3,
        NA = 4

    }

    public class RankUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SecurityStamp { get; set; }
        public string RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IntroducerSecurityStamp { get; set; }
        public string IntroducerCode { get; set; }
        public DateTime UserJoiningDate { get; set; }
        public int UserRank { get; set; }

    }

    /// <summary>
    /// This model defines the Introducer Information of User
    /// </summary>
    public class Introducer
    {
        /// <summary>
        /// Introducer's User name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Introducer's Security Stamp
        /// </summary>
        public string SecurityStamp { get; set; }
        /// <summary>
        /// Introduction Code used to register the user by this introducer
        /// </summary>
        public Int64 IntroductionCode { get; set; }
        /// <summary>
        /// Introducer's Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Introducer's Joining date
        /// </summary>
        public DateTime JoiningDate { get; set; }
        /// <summary>
        /// Introducer's Role ID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// Introducer's introducer_Security Stamp
        /// </summary>
        public string InroducerSecurityStamp { get; set; }
    }
}