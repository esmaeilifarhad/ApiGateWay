using APIGateway.Filter;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Web.Http;


namespace APIGateway.Controllers
{
    [Authorize(Roles = "Shahab")]
    [WebAPIActionFilter]
    public class ShahabController : ApiController
    {
        [HttpPost]
        //دریافت کد شهاب از طریق کد مشتری 
        public IHttpActionResult CallShahabCode(ParamIn input)
        {
            try
            {
                ParamOut paramOut = new ParamOut();
                using (OracleConnection conn = new OracleConnection(APIGateway.Properties.Settings.Default.oradb))
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandTimeout = 60000;
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format(@"select * from shahbcodeinfo where cfcifno ={0} ", input.cif);
                    //cmd.CommandText = string.Format(@"select * from customer_shahab_info");


                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        paramOut.CFCIFNO = dr["CFCIFNO"].ToString();
                        paramOut.C1034SHAHBCOD = dr["C1034SHAHBCOD"].ToString();
                    }
                }
                return Ok(paramOut);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
