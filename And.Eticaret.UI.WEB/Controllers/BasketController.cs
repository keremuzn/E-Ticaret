using And.Eticaret.Core.Model;
using And.Eticaret.UI.WEB.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Controllers
{
    public class BasketController : AndControllerBase
    {
        AndDB db = new AndDB();
        // GET: Basket
        [HttpPost]
        public JsonResult AddProduct(int productID, int quantity)
        {
            db.Baskets.Add(new Core.Model.Entity.Basket
            {
                CreateDate=DateTime.Now,
                CreateUserID=LoginUserID,
                ProductID=productID,
                Quantity=quantity,
                UserID=LoginUserID
            });
            var rt = db.SaveChanges();
            return Json(rt,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {

           
            return View();
        }
    }
}