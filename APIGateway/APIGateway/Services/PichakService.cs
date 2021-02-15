using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using APIGateway.ViewModel.Pichak;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace APIGateway.Services
{
    public class PichakService
    {
        #region Initial
        string _commonUrl = "https://Eghtesadnovin.pichak.nibn.ir:9911/api/pichak/";
        public PichakService()
        {

        }
        private RestClient InitialRestClient(string url)
        {
            var client = new RestClient(_commonUrl + url);
            client.Authenticator = new HttpBasicAuthenticator("EghtesadNovin", "E9ht3$@dN0v!n");
            client.Timeout = -1;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            //Add PFX
            string address =""; //@"E:\Document\Pichak\Govahipichackkhadamat\pichak_BankerNet";
            var rootpath = HttpContext.Current.Server.MapPath("~");
             address = rootpath+@"FilesPFX\pichak_BankerNet";
            var certFile = Path.Combine(address, "certificatePichakNetTest.pfx");
            X509Certificate2 certificate = new X509Certificate2(certFile, "testPi@p@ssw0rd");
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Proxy = new WebProxy();
            return client;
        }
        private RestRequest InitialRestRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("callerTerminalName", "InternetBank");
            request.AddHeader("callerBranchUserName", "");
            request.AddHeader("callerBranchCode", "");
            request.AddHeader("customerAuthStatus", "2");
            request.AddHeader("Authorization", "Basic RWdodGVzYWROb3ZpbjpFOWh0MyRAZE4wdiFu");
            request.AddHeader("Content-Type", "application/json");
            return request;
        }
        #endregion


        /// <summary>
        /// سرویس ثبت چک
        /// </summary>
        public string cheque_issue(ViewModel.Pichak.cheque_issue_Root param)
        {
            try
            {
                var client = InitialRestClient("cheque/issue");
                var request = InitialRestRequest();

                var body = new
                {
                    accountOwners = param.accountOwners,
                    receivers = param.receivers,
                    signers = param.signers,
                    sayadId = param.sayadId,
                    seriesNo = param.seriesNo,
                    serialNo = param.serialNo,
                    fromIban = param.fromIban,
                    amount = param.amount,
                    description = param.description,
                    dueDate = param.dueDate,
                    toIban = param.toIban,
                    currency = param.currency,
                    bankCode = param.bankCode,
                    branchCode = param.branchCode,
                    chequeType = param.chequeType,
                    chequeMedia = param.chequeMedia

                };

                request.AddJsonBody(body);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful == false)
                {

                    if (response.ErrorException != null)
                        UtilityAndServices.Utility.Utility.CreateLog(response.ErrorException.ToString());

                    if (response.ErrorMessage != null)
                        UtilityAndServices.Utility.Utility.CreateLog(response.ErrorMessage.ToString());

                    return response.Content;
                }

                return response.Content;
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }


        }
        /// <summary>
        /// سرویس تایید چک توسط گیرنده
        /// </summary>
        public string cheque_accept(ViewModel.Pichak.Cheque_accept_Param param)
        {
            var client = InitialRestClient("cheque/accept");
            var request = InitialRestRequest();

            var body = new
            {
                accept = param.accept,
                sayadId = param.sayadId,
                acceptDescription = param.acceptDescription,
                acceptDate = param.acceptDate,
                acceptor = param.acceptor,
                acceptorAgent = param.acceptorAgent

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {
                return response.Content;
                //ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
                // return error;
            }
            return response.Content;

        }
        /// <summary>
        /// سرویس انتقال چک
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public string cheque_transfer(cheque_transfer_root param)
        {

            try
            {
                var client = InitialRestClient("cheque/transfer");
                var request = InitialRestRequest();

                var body = new
                {
                    sayadId = param.sayadId,
                    holder = param.holder,
                    receivers = param.receivers,
                    signers = param.signers,
                    toIban = param.toIban,
                    transferDate = param.transferDate,
                    acceptTransfer = param.acceptTransfer,
                    description = param.description
                };



                request.AddJsonBody(body);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful == false)
                {

                    if (response.ErrorException != null)
                        return response.ErrorException.ToString();

                    if (response.ErrorMessage != null)
                        return response.ErrorMessage.ToString();

                    return response.Content;
                }

                return response.Content;
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }

        /// <summary>
        /// سرویس لغو ثبت توسط شعبه
        /// </summary>
        public string cheque_cancel(ViewModel.Pichak.cheque_cancel_Param param)
        {
            var client = InitialRestClient("cheque/cancel");
            var request = InitialRestRequest();

            //ViewModel.Pichak.cheque_cancel_Canceller cheque_Cancel_Canceller = new ViewModel.Pichak.cheque_cancel_Canceller()
            //{
            //    idCode = "1236547896",
            //    idType = 2,
            //    shahabId = "10001236547896",

            //};

            //ViewModel.Pichak.cheque_cancel_CancellerAgent cheque_Cancel_CancellerAgent = new ViewModel.Pichak.cheque_cancel_CancellerAgent()
            //{
            //    idCode = "0017523698",
            //    shahabId = "100000017523698",
            //    idType = 1
            //};
            var body = new
            {
                sayadId = param.sayadId,
                cancelDate = param.cancelDate,
                cancelDescription = param.cancelDescription,
                canceller = param.canceller,
                cancellerAgent = param.cancellerAgent

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {
                return response.Content;
                // ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
            return response.Content;

        }

        public string inquiry_issuer_inquiry(inquiry_issuer_inquiry_Root param)
        {
            var client = InitialRestClient("inquiry/issuer-inquiry");
            var request = InitialRestRequest();


            var body = new
            {

                sayadId = param.sayadId,
                shahabId = param.shahabId,
                idType = param.idType,
                idCode = param.idCode

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {

                if (response.ErrorException != null)
                    return response.ErrorException.ToString();

                if (response.ErrorMessage != null)
                    return response.ErrorMessage.ToString();

                return response.Content;
                //ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
            return response.Content;

        }

        /// <summary>
        /// 5 سرویس Unlock کردن چک
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string cashing_unlock_cheque(cashing_unlock_cheque_Root param)
        {
            var client = InitialRestClient("cashing/unlock-cheque");
            var request = InitialRestRequest();
            var body = new
            {


                sayadId = param.sayadId,
                holders = param.holders,
                chequeCarrier = param.chequeCarrier,
                requestDate = param.requestDate,
                cashierBranchCode = param.cashierBranchCode,
                cashierBankCode = param.cashierBankCode

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {
                return response.Content;
            }
            return response.Content;
        }

        /// <summary>
        /// سرویس Lock کردن چک برای نقد کردن
        /// </summary>
        public string cashing_lock_cheque_for_cashing(ViewModel.Pichak.cashing_lock_cheque_Root param)
        {
            //cashing/lock-cheque-for-cashing
            var client = InitialRestClient("cashing/lock-cheque-for-cashing");
            var request = InitialRestRequest();

            //List<ViewModel.Pichak.cashing_lock_cheque_Holder> lst_cashing_Lock_Cheque_Holder = new List<ViewModel.Pichak.cashing_lock_cheque_Holder>();
            //ViewModel.Pichak.cashing_lock_cheque_Holder cashing_Lock_Cheque_Holder = new ViewModel.Pichak.cashing_lock_cheque_Holder()
            //{
            //    idCode = "0079865321",
            //    shahabId = "3000001006423219",
            //    idType = 1
            //};
            //lst_cashing_Lock_Cheque_Holder.Add(cashing_Lock_Cheque_Holder);
            //ViewModel.Pichak.cashing_lock_cheque_ChequeCarrier cashing_Lock_Cheque_ChequeCarrier = new ViewModel.Pichak.cashing_lock_cheque_ChequeCarrier()
            //{
            //    shahabId = "3000001006423219",
            //    idCode = "0079865321",
            //    idType = 1
            //};
            var body = new
            {
                sayadId = param.sayadId,
                holderIban = param.holderIban,
                fromIban = param.fromIban,
                cashingAmount = param.cashingAmount,
                cashingDueDate = param.cashingDueDate,
                requestDate = param.requestDate,
                bounceCheque = param.bounceCheque,
                cashierBranchCode = param.cashierBranchCode,
                cashierBankCode = param.cashierBankCode,
                chequeType = param.chequeType,
                chequeMedia = param.chequeMedia,
                serialNo = param.serialNo,

                holders = param.holders,
                chequeCarrier = param.chequeCarrier

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {
                return response.Content;
                //ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
            return response.Content;

        }
        /// <summary>
        /// سرویس استعلام وضعیت انتقال چک
        /// </summary>

        public string inquiry_transfer(ViewModel.Pichak.inquiry_transfer_Param param)
        {

            var client = InitialRestClient("inquiry/transfer");
            var request = InitialRestRequest();


            var body = new
            {
                sayadId = param.sayadId,
                shahabId = param.shahabId,
                idType = param.idType,
                idCode = param.idCode

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {
                return response.Content;
                //ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
            return response.Content;
        }
        /// <summary>
        /// سرویس انتقال چک
        /// </summary>
        public void cheque_transfer()
        {
            var client = InitialRestClient("cheque/transfer");
            var request = InitialRestRequest();


            var body = new
            {
                sayadId = "8370980000000688",
                shahabId = "1000000058672724",
                idType = 1,
                idCode = "1292154391"

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {

                ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
        }
        /// <summary>
        /// سرویس استعلام چک توسط دارنده
        /// </summary>
        public string inquiry_cheque(ViewModel.Pichak.inquiry_cheque_Param param)
        {
            var client = InitialRestClient("inquiry/inquiry-cheque");
            var request = InitialRestRequest();


            var body = new
            {
                sayadId = param.sayadId,
                shahabId = param.shahabId,
                idType = param.idType,
                idCode = param.idCode

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {
                return response.Content;
                // ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
            return response.Content;
        }
        /// <summary>
        /// سرویس استعلام چک توسط صادرکننده
        /// </summary>
        public string issuer_inquiry()
        {

            var client = InitialRestClient("inquiry/issuer-inquiry");
            var request = InitialRestRequest();


            var body = new
            {
                sayadId = "8370980000000688",
                shahabId = "1000000058672724",
                idType = 1,
                idCode = "1292154391"

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {
                return response.Content;
                //ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
            return response.Content;

        }
        /// <summary>
        /// سرویس استعلام نقد شوندگی
        /// </summary>
        /// <returns></returns>
        public string cashing_inquiry(ViewModel.Pichak.cashing_inquiry_Root param)
        {

            var client = InitialRestClient("cashing/cashing-inquiry");
            var request = InitialRestRequest();


            var body = new
            {
                sayadId = param.sayadId,
                serialNo = param.serialNo,
                fromIban = param.fromIban,
                cashingAmount = param.cashingAmount,
                cashingDueDate = param.cashingDueDate,
                chequeType = param.chequeType,
                chequeMedia = param.chequeMedia,
                requestDate = param.requestDate,
                holders = param.holders,
                cashierBankCode = param.cashierBankCode,
                cashierBranchCode = param.cashierBranchCode
            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {
                return response.Content;
                //ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
            return response.Content;

        }
        /// <summary>
        /// سرویس نقد کردن درون بانکی
        /// </summary>
        /// <returns></returns>
        public string cashing_inner_bank_cashing(ViewModel.Pichak.inner_bank_cashing_Root param)
        {

            var client = InitialRestClient("cashing/inner-bank-cashing");
            var request = InitialRestRequest();


            var body = new
            {

                sayadId = param.sayadId,
                serialNo = param.serialNo,
                fromIban = param.fromIban,
                cashingAmount = param.cashingAmount,
                cashingDueDate = param.cashingDueDate,
                chequeType = param.chequeType,
                chequeMedia = param.chequeMedia,
                requestDate = param.requestDate,
                holders = param.holders,
                holderIban = param.holderIban,
                bounceCheque = param.bounceCheque,
                chequeCarrier = param.chequeCarrier,
                bankCashingResult = param.bankCashingResult,
                cashierBankCode = param.cashierBankCode,
                cashierBranchCode = param.cashierBranchCode

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {
                return response.Content;
                //ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
            return response.Content;
        }
        /// <summary>
        /// سرویس ابطال چک
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string cheque_revoke(ViewModel.Pichak.cheque_revoke_Root param)
        {

            var client = InitialRestClient("cheque/revoke");
            var request = InitialRestRequest();


            var body = new
            {

                sayadId = param.sayadId,
                revokerAgent = param.revokerAgent,
                revoker = param.revoker,
                revokeDate = param.revokeDate

            };
            request.AddJsonBody(body);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful == false)
            {

                if (response.ErrorException != null)
                    return response.ErrorException.ToString();

                if (response.ErrorMessage != null)
                    return response.ErrorMessage.ToString();

                return response.Content;
                //ViewModel.Pichak.Error error = JsonConvert.DeserializeObject<ViewModel.Pichak.Error>(response.Content);
            }
            return response.Content;
        }



    }
}