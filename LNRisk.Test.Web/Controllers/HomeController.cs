using LNRisk.Test.Business;
using LNRisk.Test.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LNRisk.Test.Web.Controllers
{
    public class HomeController : Controller
    {
        public MyClass MyClass
        {
            get
            {
                var obj = Session["Myclass"];

                if(obj == null)
                    obj = Session["Myclass"] = new MyClass();

                return (MyClass)obj;
            }
        }

        public ActionResult Index()
        {
            // Creating First Data
            MyClass.StoreData("1", "Teste");
            MyClass.StoreData("2", "Civic Noon Madam Kayak tattarrattat");

            return View();
        }

        [HttpGet]
        public JsonResult List()
        {
            List<Item> itemList = MyClass.List();
            return Json(new {result = true, message = "Successfully listed.", data = itemList}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(Item model)
        {
            MyClass.StoreData(model.Id, model.Payload);
            return Json(new {result = true, message = "stored"});
        }
    }
}