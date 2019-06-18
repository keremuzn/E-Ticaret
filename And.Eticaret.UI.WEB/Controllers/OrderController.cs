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
            }
            db.Orders.Add(order);
            db.SaveChanges();
            return View();
        }
    }
}