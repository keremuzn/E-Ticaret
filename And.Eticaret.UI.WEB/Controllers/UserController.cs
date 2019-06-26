using And.Eticaret.Core.Model;
using And.Eticaret.UI.WEB.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Controllers
{
    public class UserController : AndControllerBase
    {
        AndDB db = new AndDB();
        // GET: User
        [Route("Profil")]
        public ActionResult Index()
        {
            var user = db.Users.Find(LoginUserID);
            
            return View(user);
        }
    }
}