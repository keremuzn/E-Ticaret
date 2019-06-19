using And.Eticaret.Core.Model;
using And.Eticaret.Core.Model.Entity;
using And.Eticaret.UI.WEB.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Controllers
{
    public class OrderController : AndControllerBase
    {
	

	
        AndDB db = new AndDB();
        // GET: Order
        [Route("SiparisVer")]
        public ActionResult AddressList()
        {
            var data = db.UserAddresses.Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }

        public ActionResult CreateUserAdress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUserAdress(UserAddress entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.CreateUserID = LoginUserID;
            entity.IsActive = true;
            entity.UserID = LoginUserID;
            db.UserAddresses.Add(entity);
            db.SaveChanges();
            return RedirectToAction("AddressList");
        }

        public ActionResult CreateOrder(int id)
        {
            var sepet = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            Order order = new Order();
            order.CreateDate = DateTime.Now;
            order.CreateUserID = LoginUserID;
            order.StatusID = 4;
            order.TotalProductPrice = sepet.Sum(x => x.Product.Price); // spetteki ürün fiyatlarını toplamak
            order.TotalTaxPrice = sepet.Sum(x => x.Product.Tax); // vergilerin toplamı
            order.TotalDiscount = sepet.Sum(x => x.Product.Discount); // indirimlerin toplamı
            order.TotalPrice = order.TotalProductPrice + order.TotalTaxPrice + order.TotalDiscount;
            order.UserAddressID = id;
            order.UserID = LoginUserID;
            order.OrderProducts = new List<OrderProduct>();

            foreach (var item in sepet)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    CreateDate = DateTime.Now,
                    CreateUserID = LoginUserID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity
                });
                db.Baskets.Remove(item);
            }
            db.Orders.Add(order);

            db.SaveChanges();
            // var orderid = db.Orders.Where(x => x.UserID == LoginUserID).LastOrDefault().ID;
            return RedirectToAction("Detail", new { id =  order.ID});
        }

        public ActionResult Detail(int id)
        {
            var data = db.Orders.Include("OrderProducts")
                .Include("OrderProducts.Product")
                .Include("OrderPayments")
                .Include("Status")
                .Include("UserAddress")
                .Where(x => x.ID == id).FirstOrDefault();

            return View(data);

        }


        [Route("Siparislerim")]
        public ActionResult Index()
        {
            var data = db.Orders.Include("Status").Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }
        public ActionResult Pay(int id)
        {
            var order = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            order.StatusID = 13;
            db.SaveChanges();
            return RedirectToAction("Detail", new { id = order.ID });
        }
    }
}