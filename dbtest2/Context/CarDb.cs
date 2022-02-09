using dbtest2.Models;
using dbtest2.Models.Login;
using System.Data.Entity;

namespace dbtest2.Context
{
    public class CarDb : DbContext
    {
        public CarDb() : base("name=DBCS") { } //Web.config에 정의

        public DbSet<Car> Cars { get; set; }

        public DbSet<UserModel> Users { get; set; }
    }
}