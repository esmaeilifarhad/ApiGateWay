using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using APIGateway.ViewModel.Pichak;
using Jose;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace APIGateway.Services
{
    public class PichakService
    {
        #region Initial
        string _commonUrl = "https://Eghtesadnovin.pichak.nibn.ir:9911/api/pichak/";
        private string _callerTerminalName { get { return "mobileBank"; } }
        private string _callerBranchUserName { get { return ""; } }
        private string _callerBranchCode { get { return "5501954"; } }
        private string _customerAuthStatus { get { return "2"; } }
        private string _certificateThumbPrint { get { return "d5877653ff221c4bbad3032aa4f76d588d6bceaf"; } }

        public PichakService()
        {

        }
        /// <summary>
        /// initial Certificate and Basic Athentication
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private RestClient InitialRestClient(string url)
        {
            var client = new RestClient(_commonUrl + url);
            client.Authenticator = new HttpBasicAuthenticator("EghtesadNovin", "E9ht3$@dN0v!n");
            client.Timeout = -1;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;


            X509Certificate2 certificate = new X509Certificate2(pathCertFile("pichak_BankerNet", "certificatePichakNetTest"), "testPi@p@ssw0rd");
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            client.Proxy = new WebProxy();
            return client;
        }
        private RestRequest InitialRestRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("callerTerminalName", _callerTerminalName);
            request.AddHeader("callerBranchUserName", _callerBranchUserName);
            request.AddHeader("callerBranchCode", _callerBranchCode);
            request.AddHeader("customerAuthStatus", _customerAuthStatus);

            request.AddHeader("Authorization", "Basic RWdodGVzYWROb3ZpbjpFOWh0MyRAZE4wdiFu");
            request.AddHeader("Content-Type", "application/json");
            //  request.AddHeader("x-jws-signature", "869E0A558A9A59D70E28022BBA5A4DFD0298A317E37E4B31B8556A694C594EEA960B71A9010FD27F87244C7DA899012C000A7262D7954EA9F860DE363C7479267667B61A3361F8BD7FF1B9FD1F5AC122810DC4CFC21929A9EB49B270DBAB597BF04F070DBDB1EEE6E08504CE9D77D91406126637493B233363A1F80E55D89C7828B3EAB992050735DFA367AE42D696562D3A0EAE5D9DAD6C2CEDEF021D05FC90BA5498178B393089D590C50C20B2B25523F088E27BB4D0DD23F6ED3C8F4D3FA6CB18F03F1CB658941CE8125B5578651D17A78BDF3D8906DA8982123AE9E5DD94740AD66B47638B21C4AAC58664365B5AC0D0B7B080D636D83F2147A9371B4193");



            return request;
        }
        /// <summary>
        /// path Certificate
        /// </summary>
        /// <returns></returns>
        private string pathCertFile(string FolderName, string FileName)
        {
            //Add PFX

            //string address = "";
            //var rootpath = HttpContext.Current.Server.MapPath("~");
            //address = rootpath + @"PFXFiles\pichak_BankerNet";
            //var certFile = Path.Combine(address, "certificatePichakNetTest.pfx");
            //return certFile;

            string address = "";
            var rootpath = HttpContext.Current.Server.MapPath("~");
            address = rootpath + @"PFXFiles\" + FolderName;
            var certFile = Path.Combine(address, FileName + ".pfx");
            return certFile;

        }
        /// <summary>
        /// sign in header
        /// </summary>
        /// <param name="plaintext"></param>
        /// <param name="path"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //private  string Token12(string plaintext, string path, string password)
        //{
        //    try
        //    {
        //        X509Certificate2 certX509 = new X509Certificate2(path, password);
        //        byte[] privateKey = certX509.Export(X509ContentType.Cert, password);
        //        //var key1 = certX509.PrivateKey as RSACryptoServiceProvider;
        //        //string token = Jose.JWT.Encode(plaintext, privateKey, JwsAlgorithm.HS256);
        //        string token = Jose.JWT.Encode(plaintext, privateKey, JwsAlgorithm.RS256);
        //        //var token =  JWT.EncodeBytes(Encoding.UTF8.GetBytes(plaintext), privateKey, JwsAlgorithm.HS256,null,null,null);
        //        // var token=JWT.Encode(plaintext,privateKey,JwsAlgorithm.ES256);
        //        // var token = JWT.Encode(plaintext, privateKey, JwsAlgorithm.HS512);

        //        //string token = Jose.JWT.Encode(Encoding.UTF8.GetBytes(plaintext), privateKey, , JweEncryption.A128CBC_HS256, null, null);
        //        return token;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        //private string jwt(string plaintext, string path, string password) {
        //    X509Certificate2 certificate = new X509Certificate2(path, password);
        //   // RSACryptoServiceProvider key = certificate.PrivateKey as RSACryptoServiceProvider;
        //    byte[] privateKey = certificate.Export(X509ContentType.Cert, password);


        //    // var key1 = certX509.PrivateKey as RSACryptoServiceProvider;
        //    //string token = Jose.JWT.Encode(plaintext, privateKey, JwsAlgorithm.HS256);

        //    //System.Security.Cryptography.X509Certificates.X509Certificate2 certificate = LoadCertificate("Certificate.pfx", "PasswordofCertificate");

        //    //RSACryptoServiceProvider key = certificate.PrivateKey as RSACryptoServiceProvider;

        //    //--------------------------
        //    var header = new { alg = "RS256", typ = "JWT" };
        //    //            var payload1 = new Dictionary<string, object>()
        //    //{
        //    //{"iss", "6cc52d24-cb4f-46de-bf88-d08bf559e8e5"},
        //    //{"sub", "fc86d3e0-2d5c-4ebb-9765-cb8b0e011ee4"},
        //    //{"iat", 1589979664},
        //    //{"exp", 1590008421},
        //    //{"aud", "account-d.docusign.com"},
        //    //{"scope", "signature impersonation"}
        //    //};

        //    var payload1 = plaintext;



        //    byte[] pBytes =  Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(payload1,Formatting.None));
        //    byte[] hBytes =  Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));


        //    string stringToSign = Convert.ToBase64String(hBytes) + "." + Convert.ToBase64String(pBytes);
        //    try
        //    {
        //        string token = Jose.JWT.Encode(stringToSign, privateKey, JwsAlgorithm.RS256);
        //        return token;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }


        //}

        //private string jwtRsa(string payload, string path, string password) {

        //    X509Certificate2 cert = new X509Certificate2(path, password);
        //   var pub = cert.PublicKey.Key.ToXmlString(false);
        //   RSA    key1 = cert.PrivateKey as RSACryptoServiceProvider;

        //    // tbKey.Text += Environment.NewLine;
        //    // var pri = cert.PrivateKey.ToXmlString(true);


        //    //            var payload = new Dictionary<string, object>()
        //    //{
        //    //    { "sub", "mr.x@contoso.com" },
        //    //    { "exp", 1300819380 }
        //    //};

        //    var privateKey = new X509Certificate2(path, password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet).PrivateKey as RSACryptoServiceProvider;

        //    string token = Jose.JWT.Encode(payload, key1, JwsAlgorithm.RS256);
        //    return token;
        //}


        private string GenerateSign(string pichakReqBody, string callerTerminalName, string callerBranchCode, string callerBranchUserName, string customerAuthStatus, string certificateThumbPrint)
        {

            var payload = customerAuthStatus +
                          callerTerminalName +
                          callerBranchCode +
                          callerBranchUserName +
                          pichakReqBody;

            return GenerateJwt(certificateThumbPrint, payload, true, detachPayload: true);
        }

        private string GenerateJwt(string certificateFingerPrint, string payload, bool addCertificateHeader = false, Dictionary<string, object> extraHeaders = null, bool detachPayload = false)
        {
            try
            {
                var certCollectionStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                certCollectionStore.Open(OpenFlags.ReadOnly);
                var collection = certCollectionStore.Certificates.Find(X509FindType.FindByThumbprint, certificateFingerPrint, false);
                if (collection.Count == 0)
                {
                    certCollectionStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    certCollectionStore.Open(OpenFlags.ReadOnly);
                    certCollectionStore.Certificates.Find(X509FindType.FindByThumbprint, certificateFingerPrint, false);
                    collection = certCollectionStore.Certificates.Find(X509FindType.FindByThumbprint, certificateFingerPrint, false);
                }
                var certificate = collection[0];
                certCollectionStore.Close();

                var certificateString = Convert.ToBase64String(certificate.Export(X509ContentType.Cert), Base64FormattingOptions.None);

                if (extraHeaders == null)
                    extraHeaders = new Dictionary<string, object>();

                if (addCertificateHeader)
                    extraHeaders.Add("x5c", new[] { certificateString });

                var options = new JwtOptions { DetachPayload = detachPayload };
                var token = JWT.Encode(payload, certificate.GetRSAPrivateKey(), JwsAlgorithm.RS256, extraHeaders, options: options);

                return token;
            }
            catch (Exception ex)
            {

                throw;
            }

        }



        #endregion

        #region Services
        /// <summary>
        /// سرویس ثبت چک
        /// </summary>
        /// 
        //public string cheque_issue(ViewModel.Pichak.cheque_issue_Root param)
        //{
        //    try
        //    {
        //        var client = new RestClient("https://Eghtesadnovin.pichak.nibn.ir:9911/api/pichak/" + "cheque/issue");
        //        client.Timeout = -1;

        //        //ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.DefaultConnectionLimit = 9999;
        //        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
        //        ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        string address = "";
        //        var rootpath = HttpContext.Current.Server.MapPath("~");
        //        address = rootpath + @"FilesPFX\pichak_BankerNet";
        //        var certFile = Path.Combine(address, "certificatePichakNetTest.pfx");

        //        client.Proxy = new WebProxy();
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("Authorization", "Basic RWdodGVzYWROb3ZpbjpFOWh0MyRAZE4wdiFu");
        //        request.AddHeader("Content-Type", "application/json");

        //        var body = new
        //        {
        //            accountOwners = param.accountOwners,
        //            receivers = param.receivers,
        //            signers = param.signers,
        //            sayadId = param.sayadId,
        //            seriesNo = param.seriesNo,
        //            serialNo = param.serialNo,
        //            fromIban = param.fromIban,
        //            amount = param.amount,
        //            description = param.description,
        //            dueDate = param.dueDate,
        //            toIban = param.toIban,
        //            currency = param.currency,
        //            bankCode = param.bankCode,
        //            branchCode = param.branchCode,
        //            chequeType = param.chequeType,
        //            chequeMedia = param.chequeMedia

        //        };
        //        //add Sign
        //        //string plainTextConcat = "2" + "InternetBank" + "5501954" + "" + JsonConvert.SerializeObject(body);

        //        string plainTextConcat = "2" + "mobileBank" + "5501954" + "" + JsonConvert.SerializeObject(body);

        //        var stringSign = SignTool.Token12(plainTextConcat, certFile, "testPi@p@ssw0rd");
        //        request.AddHeader("x-jws-signature", stringSign);

        //        //----------------------------------------
        //        request.AddJsonBody(body);
        //        IRestResponse response = client.Execute(request);
        //        if (response.IsSuccessful == false)
        //        {

        //            if (response.ErrorException != null)
        //                UtilityAndServices.Utility.Utility.CreateLog(response.ErrorException.ToString());

        //            if (response.ErrorMessage != null)
        //                UtilityAndServices.Utility.Utility.CreateLog(response.ErrorMessage.ToString());

        //            return response.Content;
        //        }

        //        return response.Content;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new ArgumentException(ex.Message);
        //    }


        //}
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
                //add Sign
                var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
                request.AddHeader("x-jws-signature", stringSign);
                //----------------------------------------
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
            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------
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
                //add Sign
                var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
                request.AddHeader("x-jws-signature", stringSign);
                //----------------------------------------


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
            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------
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

            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------

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
            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------
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
            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------
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
            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------
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
            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------
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
            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------

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

            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------

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

            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------

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
            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------
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
            //add Sign
            var stringSign = GenerateSign(JsonConvert.SerializeObject(body), _callerTerminalName, _callerBranchCode, _callerBranchUserName, _customerAuthStatus, _certificateThumbPrint.ToUpper());
            request.AddHeader("x-jws-signature", stringSign);
            //----------------------------------------
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
        #endregion


    }



}