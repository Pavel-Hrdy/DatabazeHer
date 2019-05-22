using DataHer.Dao;
using DataHer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatabazeHer.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {


            return View();
        }
        public ActionResult AjaxRequest()
        {
            return View();

        }
    }
}