
using DomainClass;
using DomainClass.Models;
using System.Data.Entity;

namespace DataAccess.Context
{
    public class MyContext:DbContext
    {
        public MyContext():base("Data Source=.;Initial Catalog=ApiGateway;Integrated Security=true;")
        {

        }
        public DbSet<User> Users { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<UserRole> UserRoles { set; get; }
        public DbSet<Log> Logs { set; get; }

    }
}
