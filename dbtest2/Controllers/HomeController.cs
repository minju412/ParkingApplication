using dbtest2.Context;
using dbtest2.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Web.Mvc;


namespace dbtest2.Controllers
{
    public class HomeController : Controller
    {

        //private CarDb db = new CarDb();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TableList()
        {
            var dt = new DataTable();

            // 오라클 연결 문자열        
            //string _strConn = "Data Source=parkingLot;Integrated Security=yes;";
            //string _strConn = "Data Source=parkingLot;User Id=ann;Password=111111;Integrated Security=no;";
            //string _strConn = "Data Source=localhost;User ID=ann;Password=111111;Unicode=True";
            //string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=" +
            //  "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
            //  "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";



            // 오라클 연결
            // 오라클 서버 연결 객체 생성
            //using (OracleConnection conn = new OracleConnection(_strConn))
            using (var conn = new OracleConnection(_strConn))
            {
                // 연결
                conn.Open();

                // 명령 객체 생성
                //OracleCommand cmd = new OracleCommand();
                using (var cmd = new OracleCommand())
                {
                    string carnum = "1234";

                    cmd.Connection = conn;

                    // 파라미터 바인딩
                    cmd.CommandText = "SELECT * FROM c_table ORDER BY car_id ASC";
                    //cmd.CommandText = @"
                    //SELECT 
                    //    C.carnum
                    //    ,C.intime
                    //    ,C.outtime
                    //FROM
                    //    c_table C
                    //WHERE
                    //    C.carnum = @carnum
                    //";


                    cmd.Parameters.Add(new OracleParameter("@carnum", carnum));


                    // 결과 리더 객체를 리턴
                    //OracleDataReader reader = cmd.ExecuteReader();
                    var reader = cmd.ExecuteReader();

                    dt.Load(reader);
                }
            }
            ViewData["dt"] = dt;

            return View();
        }



        public ActionResult Create()
        {
            //var dt = new DataTable();
            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            using (var conn = new OracleConnection(_strConn))
            {
                conn.Open();

                using (var cmd = new OracleCommand())
                {

                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO c_table VALUES (6,'5555','13:00','17:00')"; // (@car_id,@carnum,@intime,@outtime)";

                    cmd.ExecuteNonQuery();

                    //cmd.Parameters.Add(new OracleParameter("@carnum", carnum));
                    //var reader = cmd.ExecuteReader();
                    //dt.Load(reader);
                }
            }
            //ViewData["dt"] = dt;

            //return Json(new { msg = "OK" }, JsonRequestBehavior.AllowGet);
            return View();
        }



        //public ActionResult TableChange(int car_id, string carnum)
        //{
        //    var dt = new DataTable();

        //    string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";

        //    using (var conn = new OracleConnection(_strConn))
        //    {
        //        conn.Open();

        //        using (var cmd = new OracleCommand())
        //        {
        //            cmd.Connection = conn;

        //            cmd.CommandText = "UPDATE c_table SET carnum=@carnum WHERE car_id=@car_id";

        //            cmd.Parameters.Add(new OracleParameter("@car_id", car_id));
        //            cmd.Parameters.Add(new OracleParameter("@carnum", carnum));

        //            cmd.ExecuteNonQuery();
        //        }

        //    }
        //    return Json(new { msg = "OK" });
        //}




        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Create([Bind(Include = "Car_U,CarNum,InTime,OutTime")] Car car)
        //public ActionResult Create(Car car)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    db.Cars.Add(car);
        //    //    db.SaveChanges(); //commit

        //    //    return RedirectToAction("Index");
        //    //}

        //    if (ModelState.IsValid)
        //    {
        //        using (var db = new CarDb())
        //        {
        //            db.Cars.Add(car);

        //            if (db.SaveChanges() > 0) //Commit
        //            {
        //                return Redirect("Index"); //동일한 컨트롤러 내의 Index 페이지로 이동
        //                //return RedirectToAction("Index", "Note");
        //            }
        //        }
        //        ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다. ");
        //    }

        //    return View(car);
        //}




        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}