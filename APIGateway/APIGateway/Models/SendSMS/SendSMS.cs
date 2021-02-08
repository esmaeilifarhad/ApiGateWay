using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.Models.SendSMS
{
    public static class SendSMS
    {
        public static bool SendSMSMethod(string mobileNumber, string msg)
        {
            string footerMessage= "\n بانک اقتصادنوین\n 02148031000";
            var result = false;
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

                SendSmsService.send_sms_web_servicePortTypeClient client = new SendSmsService.send_sms_web_servicePortTypeClient();
                mobileNumber.Substring(mobileNumber.Length - 10);
   

                mobileNumber = "98" + mobileNumber.Substring(mobileNumber.Length - 10);
                msg += footerMessage;
                string res = client.send_sms_web_service("saham_edalat", "P9W2nK3QAO5xbGs9x8e183d7iRKHzy", "9820005027", mobileNumber, msg, "FA");
                return true;

            }
            catch (Exception)
            {

                return result;
            }
    
        }
    }
}