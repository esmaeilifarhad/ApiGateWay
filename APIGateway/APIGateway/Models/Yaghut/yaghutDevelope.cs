using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.Models.Yaghut
{
    public class yaghutDevelope
    {
        YaghutService.SoapServicesClient yaghut = new YaghutService.SoapServicesClient("soapPort");

        internal YaghutService.ibanInformationBean getIbanInformation()
        {
            try
            {
                YaghutService.ibanInformationBean getIbanInformation = yaghut.getIbanInformation(
                               new YaghutService.contextEntry[]
                               { new YaghutService.contextEntry() {key = "SESSIONID", value = Models.Yaghut.InitialParam.SessionId  }}
                               , new YaghutService.ibanInformationRequestBean
                               {
                                   // iban="IR310550014780006512325001"
                                   iban = "IR270550014780004808891001"


                               });
                return getIbanInformation;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public YaghutService.depositResponseBean getDeposits(string cif)
        {


            YaghutService.depositResponseBean depositResponseBean = yaghut.getDeposits(
           new YaghutService.contextEntry[]
           { new YaghutService.contextEntry() { key = "SESSIONID", value = Models.Yaghut.InitialParam.SessionId } }
                                   , new YaghutService.depositSearchRequestBean
                                   {
                                       offset = 0,
                                       length = 50,
                                       cif = cif

                                   });


            var res = depositResponseBean;
            return res;
        }

    }
}
















