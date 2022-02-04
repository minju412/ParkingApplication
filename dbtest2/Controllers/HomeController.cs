//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Oracle.ManagedDataAccess.Client;
//using ParkingLot.Models;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dbtest2.Controllers
{
    public class HomeController : Controller
    {
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
            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=" +
              "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
              "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            //string _strConn = "Data Source=localhost;User ID=ann;Password=111111;Unicode=True";



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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}