using dbtest2.Context;
using dbtest2.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Web.Mvc;


namespace dbtest2.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return Redirect("/login/login");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

    }
}