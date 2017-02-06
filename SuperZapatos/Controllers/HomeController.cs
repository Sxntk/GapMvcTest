namespace SuperZapatos.Controllers
{
    using System.Web.Mvc;

    using Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error(ErrorModel error)
        {
            return View(error);
        }
    }
}