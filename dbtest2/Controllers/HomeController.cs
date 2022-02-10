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

        [Authorize]
        public ActionResult TableWrite()
        {
            return View();
        }
    
        [Authorize]
        public ActionResult TableWrite_Input(string carnum, DateTime intime)
        {
            var model = new Car();

            model.CarNum = carnum;
            model.InTime = intime;
            //model.Reg_User = Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //model.Reg_Username = User.Identity.Name;

            model.Insert();

            return Redirect("/home/tablelist");
        }

        //public ActionResult TableList()
        //{
        //    var dt = new DataTable();

        //    string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";


        //    // 오라클 연결
        //    using (var conn = new OracleConnection(_strConn))
        //    {
        //        // 연결
        //        conn.Open();

        //        // 명령 객체 생성
        //        //OracleCommand cmd = new OracleCommand();
        //        using (var cmd = new OracleCommand())
        //        {
        //            string carnum = "1234";

        //            cmd.Connection = conn;

        //            // 파라미터 바인딩
        //            cmd.CommandText = "SELECT * FROM c_table ORDER BY car_id ASC";

        //            cmd.Parameters.Add(new OracleParameter("@carnum", carnum));

        //            // 결과 리더 객체를 리턴
        //            var reader = cmd.ExecuteReader();

        //            dt.Load(reader);
        //        }
        //    }
        //    ViewData["dt"] = dt;

        //    return View();
        //}


        //public ActionResult Create()
        //{
        //    //var dt = new DataTable();
        //    string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
        //    using (var conn = new OracleConnection(_strConn))
        //    {
        //        conn.Open();

        //        using (var cmd = new OracleCommand())
        //        {
        //            cmd.Connection = conn;

        //            cmd.CommandText = "INSERT INTO c_table VALUES (1,'1333','10:00','16:00')";
        //            //cmd.CommandText = "INSERT INTO c_table (car_id,carnum,intime,outtime) VALUES (C_TABLE_SEQ.NEXTVAL,:carnum,:intime,:outtime)";

        //            cmd.ExecuteNonQuery();

        //            //cmd.Parameters.Add(new OracleParameter("@carnum", carnum));
        //            //var reader = cmd.ExecuteReader();
        //            //dt.Load(reader);
        //        }
        //    }
        //    //ViewData["dt"] = dt;

        //    //return Json(new { msg = "OK" }, JsonRequestBehavior.AllowGet);
        //    return View();
        //}

    }
}