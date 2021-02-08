using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.Models.Security
{
    public class Users
    {
        //DataAccess.Context.MyContext _context = new DataAccess.Context.MyContext();
        //DataAccess.Repository.GenericRepository<DomainClass.Models.User> _db;
        //DataAccess.Repository.GenericRepository<DomainClass.Models.UserRole> _dbUserRole;
        DataAccess.Context.MyContext _db = new DataAccess.Context.MyContext();
        public Users()
        {

            //_db= new DataAccess.Repository.GenericRepository<DomainClass.Models.User>();
        }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public String[] Roles { get; set; }
        public void FillRoles(string username, string password)
        {
          //var s=  _db.UserRoles.Include("User").ToList();

            //var res=  _db.GetById(new Guid("8F7A6FA3-E391-4F94-9FE2-B1CF5133A7B3"));

            /*
            var oldUser = _db.Users.SingleOrDefault(q => q.Username == username);
            if (oldUser == null) return;

            string pass = UtilityAndServices.Utility.Utility.RSADecryption(oldUser.Password, APIGateway.Properties.Settings.Default.PrivateKey);
            if (pass != password) return;
            if (oldUser != null)
                {
                List<string> lstString = new List<string>();
                var result = (from r in _db.Roles join ur in _db.UserRoles
                              on r.RoleId equals ur.RoleId
                              join u in _db.Users
                              on ur.UserId equals u.UserId
                              where (u.UserId == oldUser.UserId)
                              select r
                            ).ToList();
                foreach (var item in result)
                {
                    lstString.Add(item.Rolename.ToString());
                }
                Roles = lstString.ToArray();
                this.UserName = oldUser.Username;
                this.UserId = oldUser.UserId;
                */
            //var res=  _db.UserRoles.Where(q=>q.UserId== oldUser.UserId).ToList();
            if (username == "Pichak" && password == "P!CH@K")
            {
                //Roles = new string[] { "Admin", "SendSMS", "Pichak" };
                Roles = new string[] { "Pichak" , "Shahab" };
                this.UserName = username;
            }
            else
            {
                return;
            }

        }




    }
}





