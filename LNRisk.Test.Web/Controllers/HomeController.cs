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

                if (obj == null)
                    obj = Session["Myclass"] = new MyClass();

                return (MyClass)obj;
            }
        }

        public ActionResult Index()
        {
            // Creating First Data
            if (Session["Myclass"] == null)
            {
                MyClass.StoreData("1", "Teste");
                MyClass.StoreData("2", "Civic Noon Madam Kayak tattarrattat");
            }

            return View();
        }

        [HttpGet]
        public JsonResult List()
        {
            List<Item> itemList = MyClass.List();
            return Json(new { result = true, message = "Successfully listed.", data = itemList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(Item model)
        {
            var result = MyClass.StoreData(model.Id, model.Payload);
            var message = result ? "Saved!" : "This Id Already Exists.";
            return Json(new { result = result, message = message });
        }

        [HttpPost]
        public JsonResult Edit(Item model)
        {
            MyClass.EditData(model.Id, model.Payload);
            return Json(new { result = true, message = "Edited!" });
        }

        [HttpPost]
        public JsonResult Remove(Item model)
        {
            MyClass.RemoveData(model.Id);
            return Json(new { result = true, message = "Removed!" });
        }

        [HttpPost]
        public JsonResult Result(Item model)
        {
            try
            {
                int DateAmount = MyClass.CountDates(model.Id);
                var Letters = LettersToString(MyClass.CountLetters(model.Id));
                string Palindrome = MyClass.SearchBiggestPalindrome(model.Id);

                if (string.IsNullOrEmpty(Palindrome))
                {
                    Palindrome = "<b>No palindromes found.</b>";
                }

                return Json(new { result = true, message = "Success.", DateAmount = DateAmount, Letters = Letters, Palindrome = Palindrome });
            }
            catch (Exception e)
            {
                return Json(new { result = false, message = $"Error trying to get results. {e.Message}" });
            }
        }

        private string LettersToString(Dictionary<char, int> dict)
        {
            var html = string.Empty;

            foreach (char key in dict.Keys)
            {
                html += string.Format("{0} -> {1} </br> ", key, dict[key]);
            }

            if (string.IsNullOrEmpty(html))
            {
                html = "<b>No letters found.</b>";
            }

            return html;
        }

    }
}