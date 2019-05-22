using DatabazeHer.Class;
using DataHer.Dao;
using DataHer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DatabazeHer.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrace()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(HernaUser hernaUser)
        {
            if (ModelState.IsValid)
            {
               
                HernaRoleDao hernaRoleDao = new HernaRoleDao();
                HernaRole role = hernaRoleDao.GetById(2);
                hernaUser.Role = role;

                string hasher = BCrypt.HashPassword(hernaUser.Password, BCrypt.GenerateSalt(12));
                hernaUser.Password = hasher;

                HernaUserDao hernaUserDao = new HernaUserDao();
                if (hernaUserDao.GetByLogin(hernaUser.Login) == null)
                {
                    hernaUserDao.Create(hernaUser);
                    TempData["message-success"] = "Registrace proběhla úspěšně, nyní se můžete přihlásit";
                }
                else
                {
                    TempData["error"] = "Uživatel s tímto uživatelským jmenem již existuje";
                    return View("Registrace", hernaUser);
                }
            }
            else
            {
                TempData["error"] = "Nějaká pole nejsou správně vyplněna";
                return View("Registrace", hernaUser);
            }

            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public ActionResult SignIn(string login, string password)
        {
            if (Membership.ValidateUser(login, password))
            {
                HernaUserDao uzivatelDao = new HernaUserDao();
                FormsAuthentication.SetAuthCookie(login, false);
                Session["prava"] = User.Identity.Name;
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "Login nebo heslo není správné";
            return RedirectToAction("Index", "Login");
        }
     


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}