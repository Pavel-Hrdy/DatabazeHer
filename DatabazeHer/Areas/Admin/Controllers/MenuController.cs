using DataHer.Dao;
using DataHer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatabazeHer.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        [ChildActionOnly]
        // GET: Admin/Menu
        public ActionResult Index()
        {
            HernaUserDao hernaUserDao = new HernaUserDao();
            HernaUser hernaUser = hernaUserDao.GetByLogin(User.Identity.Name);



            return View(hernaUser);
        }
    }
}