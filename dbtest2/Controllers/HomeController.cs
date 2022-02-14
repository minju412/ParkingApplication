using dbtest2.Context;
using dbtest2.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Security.Claims;
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

        [Authorize] // 로그인 해야 입차 가능
        public ActionResult TableInsert()
        {
            return View();
        }

        [Authorize]
        public ActionResult TableInsert_Input(string carnum)
        {
            var model = new Car();

            model.CarNum = carnum;
            //model.Owner = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.Owner_Name = User.Identity.Name;

            model.Insert();

            return Redirect("/home/tablelist");
        }

        [Authorize] 
        public ActionResult TableDelete()
        {
            return View();
        }

        [Authorize]
        public ActionResult TableDelete_Input(string carnum)
        {
            var model = Car.Get(carnum);

            // 권한 확인

            //var userSeq = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            //if (model.Owner != userSeq)
            //{
            //    throw new Exception("수정할 수 없습니다.");
            //}

            model.UpdateOutTime();
            model.UpdateFee();
            model.Delete();

            return Redirect("/home/TableList");

            throw new Exception("잘못된 요청입니다");
        }



    }
}