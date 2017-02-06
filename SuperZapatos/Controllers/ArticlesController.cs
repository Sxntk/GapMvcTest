namespace SuperZapatos.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Exceptions;
    using Models;
    public class ArticlesController : Controller
    {
        // GET: Articles
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            try
            {
                return View(ArticleModel.GetAllArticles());
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

        public ActionResult DetailsStore(Guid id)
        {
            try
            {
                return View(ArticleModel.GetArticleByStore(id));
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
            IEnumerable<StoreModel> stores = null;
            try
            {
                 stores = StoreModel.GetAllAStores();
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
            
            ViewData["stores"] = stores;
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArticleModel article)
        {
            try
            {
                Guid storeId = StoreModel.IsValidStore(article.StoreName);
                if (storeId == Guid.Empty)
                {
                    return RedirectToAction("Error", "Home", new ErrorModel() { ErrorCode = "404", ErrorMessage = string.Format("Store {0} was not found", article.StoreName) });
                }

                article.StoreId = storeId;
                return View("SingleDetail", ArticleModel.CreateArticle(article));
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