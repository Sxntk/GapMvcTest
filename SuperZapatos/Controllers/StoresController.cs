namespace SuperZapatos.Controllers
{
    using System;
    using System.Web.Mvc;

    using Exceptions;
    using Models;
    public class StoresController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            try
            {
                return View(StoreModel.GetAllAStores());
            }
            catch (RecordNotFoundException ex)
            {
                return RedirectToAction("Error", "Home", new ErrorModel() { ErrorCode = "404", ErrorMessage = ex.Message });
            }
            catch (UnauthorizedException ex)
            {
                return RedirectToAction("Error", "Home", new ErrorModel() { ErrorCode = "401", ErrorMessage = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return RedirectToAction("Error", "Home", new ErrorModel() { ErrorCode = "400", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorModel() { ErrorCode = "500", ErrorMessage = ex.Message });
            }
        }

        public ActionResult SingleDetail(Guid id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StoreModel article)
        {
            try
            {
                return View("SingleDetail", StoreModel.CreateStore(article));
            }
            catch (RecordNotFoundException ex)
            {
                return RedirectToAction("Error", "Home", new ErrorModel() { ErrorCode = "404", ErrorMessage = ex.Message });
            }
            catch (UnauthorizedException ex)
            {
                return RedirectToAction("Error", "Home", new ErrorModel() { ErrorCode = "401", ErrorMessage = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return RedirectToAction("Error", "Home", new ErrorModel() { ErrorCode = "400", ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorModel() { ErrorCode = "500", ErrorMessage = ex.Message });
            }
        }
    }
}