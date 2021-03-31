using APIGateway.Filter;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Web.Http;

namespace APIGateway.Controllers
{
    [Authorize(Roles = "Pichak")]
    [WebAPIActionFilter]
 
    public class PichakController : ApiController
    {
        Services.PichakService pichakService = new Services.PichakService();

        public PichakController()
        {
         
        }
  
   
        //[HttpPost]
        ////دریافت کد شهاب از طریق کد مشتری 
        //public IHttpActionResult CallShahabCode(ParamIn input)
        //{
        //    try
        //    {
        //        ParamOut paramOut = new ParamOut();
        //        using (OracleConnection conn = new OracleConnection(APIGateway.Properties.Settings.Default.oradb))
        //        {
        //            conn.Open();
        //            OracleCommand cmd = new OracleCommand();
        //            cmd.CommandTimeout = 600;
        //            cmd.Connection = conn;
        //            cmd.CommandText = string.Format(@"select * from Customer_Shahab where cfcifno ={0} ", input.cif);
        //            //cmd.CommandText = string.Format(@"select * from customer_shahab_info");


        //            cmd.CommandType = CommandType.Text;
        //            OracleDataReader dr = cmd.ExecuteReader();

        //            while (dr.Read())
        //            {
        //                paramOut.CFCIFNO = dr["CFCIFNO"].ToString();
        //                paramOut.C1034SHAHBCOD = dr["C1034SHAHBCOD"].ToString();
        //            }
        //        }
        //        return Ok(paramOut);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}
        #region Pichak
        /// <summary>
        /// سرویس ثبت چک
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cheque_issue(ViewModel.Pichak.cheque_issue_Root root)
        {
           
            try
            {
                var res = pichakService.cheque_issue(root);
                var list = JsonConvert.DeserializeObject<object>(res);

                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// سرویس تایید چک توسط گیرنده
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cheque_accept(ViewModel.Pichak.Cheque_accept_Param root)
        {
            var res = pichakService.cheque_accept(root);
            var list = JsonConvert.DeserializeObject<object>(res);
            return Ok(list);
        }
        /// <summary>
        /// سرویس لغو ثبت توسط شعبه
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cheque_cancel(ViewModel.Pichak.cheque_cancel_Param root)
        {
            var res = pichakService.cheque_cancel(root);
            var list = JsonConvert.DeserializeObject<object>(res);
            return Ok(list);
        }
        /// <summary>
        /// سرویس استعلام چک توسط دارنده
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult inquiry_cheque(ViewModel.Pichak.inquiry_cheque_Param root)
        {
            var res = pichakService.inquiry_cheque(root);
            var list = JsonConvert.DeserializeObject<object>(res);
            return Ok(list);
        }
        /// <summary>
        ///سرویس استعلام وضعیت انتقال چک
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult inquiry_transfer(ViewModel.Pichak.inquiry_transfer_Param root)
        {
            var res = pichakService.inquiry_transfer(root);
            var list = JsonConvert.DeserializeObject<object>(res);
            return Ok(list);
        }
        /// <summary>
        /// سرویس انتقال چک
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cheque_transfer(ViewModel.Pichak.cheque_transfer_root root)
        {
            var res = pichakService.cheque_transfer(root);
            var list = JsonConvert.DeserializeObject<object>(res);
            return Ok(list);
        }

        /// <summary>
        /// سرویس لاک کردن  چک برای نقد کردن
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cashing_lock_cheque_for_cashing(ViewModel.Pichak.cashing_lock_cheque_Root root)
        {
            string username = User.Identity.Name;
            var res = pichakService.cashing_lock_cheque_for_cashing(root);
            var list = JsonConvert.DeserializeObject<object>(res);
            return Ok(list);
        }
        /// <summary>
        ///  سرویس  unlock کردن چک
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cashing_unlock_cheque(ViewModel.Pichak.cashing_unlock_cheque_Root root)
        {
            string username = User.Identity.Name;
            var res = pichakService.cashing_unlock_cheque(root);
            var list = JsonConvert.DeserializeObject<object>(res);
            return Ok(list);
        }
        /// <summary>
        /// سرویس استعلام نقد شوندگی
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cashing_inquiry(ViewModel.Pichak.cashing_inquiry_Root root)
        {
            string username = User.Identity.Name;
            var res = pichakService.cashing_inquiry(root);
            var list = JsonConvert.DeserializeObject<object>(res);
            return Ok(list);
        }
        /// <summary>
        /// سرویس نقد کردن درون بانکی
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cashing_inner_bank_cashing(ViewModel.Pichak.inner_bank_cashing_Root root)
        {
            string username = User.Identity.Name;
            var res = pichakService.cashing_inner_bank_cashing(root);
            var list = JsonConvert.DeserializeObject<object>(res);
            return Ok(list);
        }
        /// <summary>
        /// سرویس ابطال چک
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cheque_revoke(ViewModel.Pichak.cheque_revoke_Root root)
        {
            try
            {
                string username = User.Identity.Name;
                var res = pichakService.cheque_revoke(root);
                var list = JsonConvert.DeserializeObject<object>(res);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }

        }

        /// <summary>
        /// سرویس استعلام چک توسط صادرکننده
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult inquiry_issuer_inquiry(ViewModel.Pichak.inquiry_issuer_inquiry_Root root)
        {
            try
            {
                string username = User.Identity.Name;
                var res = pichakService.inquiry_issuer_inquiry(root);
                var list = JsonConvert.DeserializeObject<object>(res);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }

        }
        /// <summary>
        /// سرویس تغییر وضعیت )مخصوص شعبه(
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cheque_branch_status(ViewModel.Pichak.cheque_branch_status root)
        {
            try
            {
                string username = User.Identity.Name;
                var res = pichakService.cheque_branch_status(root);
                var list = JsonConvert.DeserializeObject<object>(res);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }

        }
        /// <summary>
        /// سرویس مسدودی چک
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cheque_block(ViewModel.Pichak.cheque_block_Root root)
        {
            try
            {
                string username = User.Identity.Name;
                var res = pichakService.cheque_block(root);
                var list = JsonConvert.DeserializeObject<object>(res);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }

        }
        /// <summary>
        /// سرویس رفع مسدودی
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult cheque_unblock(ViewModel.Pichak.cheque_unblock_Root root)
        {
            try
            {
                string username = User.Identity.Name;
                var res = pichakService.cheque_unblock(root);
                var list = JsonConvert.DeserializeObject<object>(res);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }

        }
        /// <summary>
        /// استعلام نام دریافت کننده چک
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IHttpActionResult receiver_inquiry(ViewModel.Pichak.receiver_inquiry_Root root)
        {
            try
            {
                string username = User.Identity.Name;
                var res = pichakService.receiver_inquiry(root);
                var list = JsonConvert.DeserializeObject<object>(res);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }

        }

        #endregion
    }
    public class ParamIn
    {
        public string cif { get; set; }
    }
    public class ParamOut
    {
        public string CFCIFNO { get; set; }
        public string C1034SHAHBCOD { get; set; }
    }
}
