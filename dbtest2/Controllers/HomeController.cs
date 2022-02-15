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

        // 원래 버전
        //[Authorize]
        //public ActionResult TableDelete()
        //{
        //    return View();
        //}

        // 실행 가능!!! - (*)
        //[Authorize]
        //public ActionResult TableDelete()
        //{
        //    var model = new Car()
        //    {
        //        Car_ID = 1,
        //        CarNum = "8888",
        //        Owner_Name = "test",
        //        Parking_Fee = 5000
        //    };
        //    ViewBag.Car = model;
        //    return View();
        //}

        [Authorize]
        public ActionResult TableDelete()
        {
            return View();
        }

        // 차량 번호는 Input에서 받음..
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


            model.UpdateOutTime(); // db update - 출차시각 
            model = Car.Get(carnum); // 출차 시각 업데이트된 모델 받음


            // 주차 시간 계산 (분으로 환산)
            //int min = (model.OutTime.Day * 24 * 60 + model.OutTime.Hour * 60 + model.OutTime.Minute) - (model.InTime.Day * 24 * 60 + model.InTime.Hour * 60 + model.InTime.Minute);

            // 주차 요금 계산
            int fee=2000;
            //if (min >= 30) // 30분 기본요금 3000원
            //{
            //    fee = 3000;
            //    min -= 30;
            //}
            //else
            //{
            //    if (min % 10 == 0) // 10분당 추가요금 1000원
            //    {
            //        fee = 1000 * (min / 10);
            //    }
            //    else
            //    {
            //        fee = 1000 * (min / 10) + 1000;
            //    }
            //}
            model.UpdateFee(fee); // db update - 주차요금 
            model = Car.Get(carnum); // 주차요금 업데이트된 모델 받음


            // 뷰에 모델 전달
            //ViewBag.Car = model;







            model.Delete(); // db delete

            return Redirect("/home/TableList");

            throw new Exception("잘못된 요청입니다");
        }

    }
}