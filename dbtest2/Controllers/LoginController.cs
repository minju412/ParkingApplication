using dbtest2.Context;
using dbtest2.Models;
using dbtest2.Models.Login;
using dbtest2.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace dbtest2.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Logout()
        {
            //HttpContext.Session.Remove("USER_LOGIN_KEY");

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            HttpContext.Response.Cookies.Add(cookie);
            //HttpContext.Current.Response.Cookies.Add(cookie);


            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            return Redirect("/login/login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            // ID, 비밀번호 - 필수
            if (ModelState.IsValid)
            {
                //using (var db = new CarDb())
                //{
                    model.ConvertPassword(); //비밀번호 암호화
                    var user = model.GetLoginUser();
                    // Linq - 메서드 체이닝
                    //var user = db.Users
                    //    .FirstOrDefault(u => u.Email.Equals(model.Email) &&
                    //                    u.Password.Equals(model.Password));

                    //로그인에 성공했을 때
                    if (user != null)
                    {
                        // 세션에 로그인 정보 담음
                        //HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.User_Seq);


                        // 세션에 로그인 정보 담음
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
                        {
                            Domain = "localhost",
                            Expires = DateTime.Now.AddHours(4),
                            Value = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(model.Email, false, 60))
                        };
                        //HttpContext.Current.Response.Cookies.Add(cookie);
                        HttpContext.Response.Cookies.Add(cookie);

                        return Redirect("/");
                    }
                //}
                //로그인에 실패했을 때
                //ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다."); // asp-validation-summary 사용 불가 (Core용)
            }
            //return View(model);
            return View();
        }


        public ActionResult Register(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel input)
        {
            try
            {
                string password2 = Request.Form["password2"];

                if (input.Password != password2)
                {
                    throw new Exception("password와 password2가 다릅니다");
                }

                // 비밀번호 암호화
                input.ConvertPassword();

                // db에 insert
                input.Register();

                // 회원가입 성공
                return Redirect("/login/login");
            }
            catch (Exception ex)
            {
                // 실패
                return Redirect($"/login/register?msg={HttpUtility.UrlEncode(ex.Message)}");
            }
        }

        public ActionResult UserProfile()
        {
            return View();
        }

    }
}