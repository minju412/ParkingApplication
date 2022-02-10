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


namespace dbtest2.Controllers
{
    public class LoginController : Controller
    {

        // [개발토끼]

          //public ActionResult Logout()
        //{
        //    HttpContext.Session.Remove("USER_LOGIN_KEY");

        //    return RedirectToAction("Index", "Home");
        //}

      
        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////


        // [bluepope]

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
                using (var db = new CarDb())
                {
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

                        return RedirectToAction("LoginSuccess", "Home");
                    }
                }
                //로그인에 실패했을 때
                //ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다."); // asp-validation-summary 사용 불가 (Core용)
            }
            //return View(model);
            return View();
        }




        //[HttpPost]
        //[Route("/login/login")]
        //public ActionResult LoginProc(UserModel input) //fromform
        //{
        //    try
        //    {
        //        input.ConvertPassword(); //비밀번호 암호화
        //        var user = input.GetLoginUser();

        //        // 로그인 작업

        //        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
        //        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.User_Seq.ToString()));
        //        identity.AddClaim(new Claim(ClaimTypes.Name, user.User_Name));
        //        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        //        identity.AddClaim(new Claim("LastCheckDateTime", DateTime.UtcNow.ToString("yyyyMMddHHmmss")));

        //        var principal = new ClaimsPrincipal(identity);

        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
        //        {
        //            IsPersistent = false,
        //            ExpiresUtc = DateTime.UtcNow.AddHours(4),
        //            AllowRefresh = true
        //        });

        //        return Redirect("/");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Redirect($"/login/login?msg={HttpUtility.UrlEncode(ex.Message)}");
        //    }

        //    return View();
        //}

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

    }
}