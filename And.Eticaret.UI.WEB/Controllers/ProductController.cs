using And.Eticaret.Core.Model;
using And.Eticaret.UI.WEB.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Controllers
{
    public class ProductController : AndControllerBase
    {
        AndDB db = new AndDB();
        // GET: Product
        [Route("Urun/{title}/{id}")]
        public ActionResult Detail(string title,int id)
        {
            var prod = db.Products.Where(x => x.ID == id).FirstOrDefault();
            return View(prod);
        }
    }
}