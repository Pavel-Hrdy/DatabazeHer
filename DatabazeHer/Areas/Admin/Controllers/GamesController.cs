using DataHer.Dao;
using DataHer.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatabazeHer.Areas.Admin.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        // GET: Games
        public ActionResult Index(int? page)
        {
            int itemsOnPage = 4;
            int pg = page.HasValue ? page.Value : 1;
            int totalGames;

            GameDao gameDao = new GameDao();
            IList<Game> games = gameDao.GetGamesPaged(itemsOnPage, pg, out totalGames);

            ViewBag.Pages = (int)Math.Ceiling((double)totalGames / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;

            ViewBag.Categories = new GameCategoryDao().GetAll();

            HernaUser user = new HernaUserDao().GetByLogin(User.Identity.Name);

            if (user.Role.Identificator == "hrac")
                return View("IndexHrac", games);

            if(Request.IsAjaxRequest())
            {
                return PartialView(games);
            }

            return View(games);

        }
        public ActionResult Category(int id)
        {
            IList<Game> games = new GameDao().GetGamesInCategoryId(id);
            ViewBag.Categories = new GameCategoryDao().GetAll();
            return View("IndexHrac", games);
        }
        public ActionResult Category2(int id)
        {
            IList<Game> games = new GameDao().GetGamesInCategoryId(id);
            ViewBag.Categories = new GameCategoryDao().GetAll();
            return View("Index", games);
        }


        public ActionResult Search(string phrase)
        {
            GameDao gameDao = new GameDao();
            IList<Game> games = gameDao.SearchGames(phrase);

            if (Request.IsAjaxRequest())
            {
                return PartialView("IndexHrac", games);
            }



            return View("IndexHrac",games);
        }

        public ActionResult Detail(int id)
        {
            GameDao gameDao = new GameDao();
            Game g = gameDao.GetById(id);

            if (Request.IsAjaxRequest())
            {
                return PartialView(g);
            }

            return View(g);
        }
     

        public ActionResult Video(int id)
        {
            GameDao gameDao = new GameDao();
            Game g = gameDao.GetById(id);

            return View(g);
        }

        public ActionResult DetailDeveloper(int id)
        {
            GameDao gameDao = new GameDao();
            Game g = gameDao.GetById(id);

            if (Request.IsAjaxRequest())
            {
                return PartialView(g);
            }

            return View(g);
        }

        [Authorize(Roles = "spravce")]
        public ActionResult Create()
        {
            GameCategoryDao gameCategoryDao = new GameCategoryDao();
            IList<GameCategory> categories = gameCategoryDao.GetAll();
            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "spravce")]
        public ActionResult Add(Game game, HttpPostedFileBase picture, int categoryId)
        {
            if (ModelState.IsValid)
            {


                if (picture != null)
                {
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);
                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = Class.ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap b = new Bitmap(smallImage);

                            Guid guid = Guid.NewGuid();
                            string imageName = guid.ToString() + ".jpg";

                            b.Save(Server.MapPath("~/uploads/game/" + imageName), ImageFormat.Jpeg);

                            smallImage.Dispose();
                            b.Dispose();

                            game.ImageName = imageName;
                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/uploads/game/" + picture.FileName));
                        }
                    }
                }

                GameCategoryDao gameCategoryDao = new GameCategoryDao();
                GameCategory gameCategory = gameCategoryDao.GetById(categoryId);

                game.Category = gameCategory;

                GameDao gameDao = new GameDao();
                gameDao.Create(game);

                TempData["message-success"] = "Hra byla úspěšně vytvořena";

            }
            else
            {
                return View("Create", game);
            }



            return RedirectToAction("Index");
        }
        [Authorize(Roles = "spravce")]
        public ActionResult Edit(int id)
        {
            GameDao gameDao = new GameDao();
            GameCategoryDao gameCategoryDao = new GameCategoryDao();

            Game g = gameDao.GetById(id);
            ViewBag.Categories = gameCategoryDao.GetAll();

            return View(g);
        }
   
        [Authorize(Roles = "spravce")]
        [HttpPost]
        public ActionResult Update(Game game, HttpPostedFileBase picture, int categoryId)
        {
            try
            {
                GameDao gameDao = new GameDao();
                GameCategoryDao gameCategoryDao = new GameCategoryDao();

                GameCategory gameCategory = gameCategoryDao.GetById(categoryId);

                game.Category = gameCategory;

                if (picture != null)
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);

                        Guid guid = Guid.NewGuid();
                        string imageName = guid.ToString() + ".jpg";

                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = Class.ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap b = new Bitmap(smallImage);



                            b.Save(Server.MapPath("~/uploads/game/" + imageName), ImageFormat.Jpeg);

                            smallImage.Dispose();
                            b.Dispose();


                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/uploads/game/" + picture.FileName));
                        }


                        game.ImageName = imageName;
                    }

                gameDao.Update(game);

                TempData["message-success"] = "Hra" + game.Name + "byla upravena";

            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Index", "Games");
        }

        [Authorize(Roles = "spravce")]
        public ActionResult Delete(int id)
        {
            try
            {
                GameDao gameDao = new GameDao();
                Game game = gameDao.GetById(id);


                gameDao.Delete(game);

                TempData["message-success"] = "Hra " + game.Name + " byla smazána";

            }
            catch (Exception exception)
            {
                throw;
            }

            return RedirectToAction("Index");

        }
    }
}