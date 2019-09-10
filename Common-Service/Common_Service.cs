using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Electricity_Service
{
    public class Common_Service
    {
        private Electricity_DAL.Common_DAL _common = null;

        public Common_Service(ConnectionStrings connectionString)
        {
            _common = new Electricity_DAL.Common_DAL(connectionString);
        }

        public async Task<List<AddressProof>> GetAddressProof()
        {
            return await _common.GetAddressProof();
        }

        public async Task<List<IdProof>> GetIdProof()
        {
            return await _common.GetIdProof();
        }

        public async Task<List<State>> GetStates()
        {
            return await _common.GetStates();
        }

        public async Task<bool> ReUploadPhoto(string userName, string filePath)
        {
            return await _common.ReUploadPhoto(userName, filePath);
        }

        public async Task<PagePermissionResponse> PagePermission(string roleId)
        {
            return await _common.PagePermission(roleId);
        }

        public async Task<ConfigurationModel> GetConfiguration()
        {
            return await _common.GetConfiguration();
        }


        public async Task<string> UpdateConfiguration(ConfigurationModel config)
        {
            return await _common.UpdateConfiguration(config);
        }

        public async Task<AddWalletResponse> AddWallet(string user_security_stamp)
        {
            AddWalletResponse response = new AddWalletResponse();
            response = await _common.AddWallet(user_security_stamp);
            return response;
        }

        public async Task<AddWalletTrnsactionResponse> AddWalletTransaction(double amount, int userId, string message, string transaction_mode)
        {
            AddWalletTrnsactionResponse response = new AddWalletTrnsactionResponse();
            response = await _common.AddWalletTransaction(amount, userId, message, transaction_mode);
            return response;
        }

        public async Task<WalletWidthdrawalResponse> AddWalletWidthdrawalRequest(int request_initiator_id, string comment)
        {
            WalletWidthdrawalResponse response = new WalletWidthdrawalResponse();
            response = await _common.AddWalletWidthdrawalRequest(request_initiator_id, comment);
            return response;
        }

        public async Task<BalanceRequestResponse> AddWalletBalanceRequest(int requestInitiatorId, decimal amount, string comment)
        {
            BalanceRequestResponse response = new BalanceRequestResponse();
            response = await _common.AddWalletBalanceRequest(requestInitiatorId, amount, comment);
            return response;
        }

        public async Task<BalanceRequestResponse> DeductWalletBalanceRequest(int requestInitiatorId, decimal amount, string comment)
        {
            BalanceRequestResponse response = new BalanceRequestResponse();
            response = await _common.DeductWalletBalanceRequest(requestInitiatorId, amount, comment);
            return response;
        }

        public async Task<AdminApprovalNotificationModel> AdminApprovalNotification(int userId)
        {
            AdminApprovalNotificationModel response = new AdminApprovalNotificationModel();
            response = await _common.AdminApprovalNotification(userId);
            return response;
        }

        public async Task<AdminWalletAddDeductApprovalNotificationModel> AdminAddDeductWalletApprovalNotification(int userId)
        {
            AdminWalletAddDeductApprovalNotificationModel response = new AdminWalletAddDeductApprovalNotificationModel();
            response = await _common.AdminAddDeductWalletApprovalNotification(userId);
            return response;
        }

        public async Task<List<WithdrawalWallet>> UpdateWalletWithdrawal(List<WithdrawalWallet> withdrawalWalletModels)
        {
            foreach (WithdrawalWallet withdrawalWalletModel in withdrawalWalletModels)
            {
                WithdrawalWallet response = await _common.UpdateWalletWithdrawal(withdrawalWalletModel);

            }


            return withdrawalWalletModels;
        }

        public async Task<List<AddDeductWallet>> UpdateWalletAddDeduct(List<AddDeductWallet> withdrawalWalletModels)
        {
            foreach (AddDeductWallet withdrawalWalletModel in withdrawalWalletModels)
            {
                AddDeductWallet response = await _common.UpdateWalletAddDeduct(withdrawalWalletModel);

            }


            return withdrawalWalletModels;
        }

        public async Task<bool> IsWithdrawalRequestSendByThisUser(int userId)
        {
            var response = await _common.IsWithdrawalRequestSendByThisUser(userId);
            return response;
        }

        public bool SendEmail(string toMailAddress, string mailSubject, string mailBody, string fromEmailAddress)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(toMailAddress);
                mail.From = new MailAddress(fromEmailAddress);
                mail.Subject = mailSubject;
                string Body = mailBody;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.telecharge.biz";
                smtp.Port = 8889;
                smtp.Credentials = new System.Net.NetworkCredential
                     (fromEmailAddress, "Terminator#123");

                //smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
