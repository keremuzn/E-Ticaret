using And.Eticaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace And.Eticaret.UI.WEB.Controllers.Base
{
    public class AndControllerBase : Controller
    {
        // Kullanıcı Login Kontrolü
        public bool IsLogin { get; private set; }


        // Giriş yapmış küşünün Id si
        public int LoginUserId { get; set; }

        // Giriş yapan kullanıcının bilgileri
        public User LoginUserEntity { get; private set; }
        protected override void Initialize(RequestContext requestContext)
        {   
            //Todo : Üye Giriş işlemleri sonrası değişecek
            
            base.Initialize(requestContext);
        }
    }
}