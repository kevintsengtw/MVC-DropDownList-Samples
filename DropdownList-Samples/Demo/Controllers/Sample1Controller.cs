using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo.Controllers
{
    /// <summary>
    /// DropDownList 的基本操作.
    /// </summary>
    public class Sample1Controller : Controller
    {
        private NorthwindDbContext db = new NorthwindDbContext();

        private Dictionary<string, string> GetAllCategories()
        {
            var query = db.Categories.OrderBy(x => x.CategoryID);
            return query.ToDictionary(x => x.CategoryID.ToString(), x => x.CategoryName);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Method1()
        {
            //DropDownList(HtmlHelper, String, IEnumerable<SelectListItem>)
            //使用 Model

            var categories = this.GetAllCategories();

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in categories)
            {
                items.Add(new SelectListItem()
                {
                    Text = category.Value,
                    Value = category.Key
                });
            }
            return View(items);
        }

        public ActionResult Method2()
        {
            //DropDownList(HtmlHelper, String, IEnumerable<SelectListItem>)
            //使用 ViewBag

            var categories = this.GetAllCategories();

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in categories)
            {
                items.Add(new SelectListItem()
                {
                    Text = category.Value,
                    Value = category.Key
                });
            }
            ViewBag.Categories = items;

            return View();
        }

        public ActionResult Method3(string category = "")
        {
            //DropDownList(HtmlHelper, String, IEnumerable<SelectListItem>)
            //顯示以選取的項目

            var categories = this.GetAllCategories();

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in categories)
            {
                items.Add(new SelectListItem()
                {
                    Text = item.Value,
                    Value = item.Key,
                    Selected = !string.IsNullOrWhiteSpace(category)
                               &&
                               item.Key.Equals(category, StringComparison.OrdinalIgnoreCase)
                });
            }
            ViewBag.Categories = items;

            return View();
        }

        //=========================================================================================

        public ActionResult Method4()
        {
            //SelectList
            //使用 Model

            var categories = this.GetAllCategories();

            SelectList result = new SelectList(categories, "Key", "Value");

            return View(result);
        }

        public ActionResult Method5()
        {
            //SelectList
            //使用 ViewBag

            var categories = this.GetAllCategories();

            SelectList result = new SelectList(categories, "Key", "Value");

            ViewBag.Categories = result;

            return View();
        }

        public ActionResult Method6(string category = "")
        {
            //SelectList
            //顯示以選取的項目

            var categories = this.GetAllCategories();

            SelectList result = new SelectList(categories, "Key", "Value", category);
            ViewBag.Categories = result;

            return View();
        }

        //=========================================================================================

        public ActionResult Method7()
        {
            //DropDownList 與 ViewBag 同樣名稱

            var categories = this.GetAllCategories();
            SelectList result = new SelectList(categories, "Key", "Value");
            ViewBag.Categories = result;
            return View();
        }

        public ActionResult Method8(string category = "")
        {
            //DropDownList 與 ViewBag 同樣名稱
            //顯示已選取的項目

            var categories = this.GetAllCategories();
            SelectList result = new SelectList(categories, "Key", "Value", category);
            ViewBag.Category = result;
            return View();
        }

        public ActionResult Method9(string category = "")
        {
            //DropDownList 與 ViewBag 同樣名稱
            //顯示已選取的項目, 並設定 LabelOption 與 HtmlAttributes

            var categories = this.GetAllCategories();
            SelectList result = new SelectList(categories, "Key", "Value", category);
            ViewBag.Category = result;
            return View();
        }
    }
}