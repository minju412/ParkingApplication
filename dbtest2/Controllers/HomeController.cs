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
using System;
using System.Collections.Generic;
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}