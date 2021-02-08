using APIGateway.YaghutService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.Models.Yaghut
{
    public static class InitialParam
    {
        private static string _SessionId;

        public static string SessionId {
            get {
                if ((DateTime.Now - DateCreated_SessionId).TotalHours > 2)
                {

                    _SessionId = CreateSessionYaghut().sessionId;
                    DateCreated_SessionId = DateTime.Now;
                }

                return _SessionId;
            
            }  

        }
        public static DateTime DateCreated_SessionId { get; set; }
        private static loginResponseBean CreateSessionYaghut (){
            YaghutService.SoapServicesClient yaghut = new YaghutService.SoapServicesClient("soapPort");
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            {
                return true;
            };

            var loginStatic = yaghut.loginStatic(null, new YaghutService.userInfoRequestBean()
            {
                includeSubscribers = true,
                includeSubscribersSpecified = true,
                password = "02355367",
                username = "statement"
            });
            return loginStatic;

        }
    }
}