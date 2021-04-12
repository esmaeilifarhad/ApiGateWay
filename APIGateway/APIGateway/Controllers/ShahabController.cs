using APIGateway.Filter;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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
                    cmd.CommandText = string.Format(@"select * from shahab where cfcifno ={0} ", input.cif);
                    /// cmd.CommandText = string.Format(@"select * from shahab");


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

        [HttpPost]
        //شرایط برداشت 
        public IHttpActionResult GetWithDrawalCondition(APIGateway.ViewModel.Shahab.ParamWithDrawalCondition param)
        {
            try
            {
                List<ViewModel.Shahab.WithDrawalCondition> lstResult = new List<ViewModel.Shahab.WithDrawalCondition>();
                using (OracleConnection conn = new OracleConnection(APIGateway.Properties.Settings.Default.oradb))
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandTimeout = 60000;
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format(@"
select * from WITHDRAWAL_CONDITION t
where t.BRANCH_CODE={0} and t.DEPOSIT_TYPE={1} and t.CUSTOMER_NO={2} and t.DEPOSIT_SERIAL={3}
order by t.CONDITION_ID,t.PLACE_ID", param.BRANCH_CODE, param.DEPOSIT_TYPE, param.CUSTOMER_NO, param.DEPOSIT_SERIAL);

                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        ViewModel.Shahab.WithDrawalCondition withDrawalCondition = new ViewModel.Shahab.WithDrawalCondition()
                        {
                            ShareCustomerNo = dr["SHARE_CUSTOMER_NO"].ToString(),
                            PlaceId = dr["PLACE_ID"].ToString(),
                            ConditionId = dr["CONDITION_ID"].ToString(),
                            BRANCH_CODE = dr["BRANCH_CODE"].ToString(),
                            CUSTOMER_NO  = dr["CUSTOMER_NO"].ToString(),
                            DEPOSIT_SERIAL = dr["DEPOSIT_SERIAL"].ToString(),
                            DEPOSIT_TYPE = dr["DEPOSIT_TYPE"].ToString(),
                        };
                        lstResult.Add(withDrawalCondition);
                    }
                }
                return Ok(lstResult);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
