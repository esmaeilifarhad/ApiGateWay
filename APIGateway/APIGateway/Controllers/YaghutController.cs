using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIGateway.Controllers
{
    //[RoutePrefix("api/[Controller]/[Action]")]
    public class YaghutController : ApiController
    {
        public Action Post()
        {
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



            //------------------------
            /*
            Yaghut.depositResponseBean depositResponseBean = yaghut.getDeposits(
                new Yaghut.contextEntry[]
            { new Yaghut.contextEntry() { key = "SESSIONID", value =result.sessionId } }
                                        , new Yaghut.depositSearchRequestBean
                                        {
                                            offset = 0,
                                            length = 50,
                                            cif = "4808891"

                                        });


            var res = depositResponseBean.depositBeans[0].depositTitle;
            */
            //-------------
            try
            {
                YaghutService.ibanInformationBean getIbanInformation = yaghut.getIbanInformation(
                               new YaghutService.contextEntry[]
                               { new YaghutService.contextEntry() {key = "SESSIONID", value =loginStatic.sessionId }}
                               , new YaghutService.ibanInformationRequestBean
                               {
                                   // iban="IR310550014780006512325001"
                                   iban = "IR270550014780004808891001"


                               });
            }
            catch (Exception ex)
            {

                throw;
            }



            return null;
        }
        //[Route("Yaghut/GetDepositPost")]
        [HttpPost]
        public IHttpActionResult GetDeposit()
        {
            Models.Yaghut.yaghutDevelope yaghutDevelope = new Models.Yaghut.yaghutDevelope();
            var res = yaghutDevelope.getDeposits("4808891");
            return Ok(res.depositBeans[0]);
        }

        public IHttpActionResult getIbanInformation()
        {
            Models.Yaghut.yaghutDevelope yaghutDevelope = new Models.Yaghut.yaghutDevelope();
            var res = yaghutDevelope.getDeposits("4808891");
            return Ok(res.depositBeans[0]);
        }
    }
}
