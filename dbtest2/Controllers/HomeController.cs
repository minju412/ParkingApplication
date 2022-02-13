using dbtest2.Context;
using dbtest2.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Web.Mvc;


namespace dbtest2.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TableList(string search)
        {
            return View(Car.GetList(search));
        }

        //[Authorize] // 로그인 해야 입차 가능
        public ActionResult TableInsert()
        {
            return View();
        }
    
        //[Authorize]
        public ActionResult TableInsert_Input(string carnum)
        {
            var model = new Car();

            model.CarNum = carnum;
            //model.Reg_User = Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //model.Reg_Username = User.Identity.Name;

            model.Insert();

            return Redirect("/home/tablelist");
        }


    }
}