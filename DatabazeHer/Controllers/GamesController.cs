using DataHer.Dao;
using DataHer.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DatabazeHer.Controllers
{
   
    public class GamesController : Controller
    {
        // GET: Games
        public ActionResult Index()
        {
            GameDao gameDao = new GameDao();
            IList<Game> games = gameDao.GetAll();

            return View(games);

        }

        public ActionResult Detail(int id)
        {
            GameDao gameDao = new GameDao();
            Game g = gameDao.GetById(id);
         
         

            return View(g);
        }

        public ActionResult DetailDeveloper(int id)
        {
            GameDao gameDao = new GameDao();
            Game g = gameDao.GetById(id);

            return View(g);
        }

        public ActionResult Video(int id)
        {
            GameDao gameDao = new GameDao();
            Game g = gameDao.GetById(id);



            return View(g);
        }
    }
}