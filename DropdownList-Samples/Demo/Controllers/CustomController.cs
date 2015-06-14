using Demo.Infrastructure.Helpers;
using Demo.Models;
using System.Linq;
using System.Web.Mvc;

namespace Demo.Controllers
{
    /// <summary>
    /// 使用自定義的下拉選單產生方法.
    /// </summary>
    public class CustomController : Controller
    {
        private NorthwindDbContext db = new NorthwindDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sample1()
        {
            var categories = this.db.Categories
                .OrderBy(x => x.CategoryID)
                .ToDictionary(x => x.CategoryID.ToString(), y => y.CategoryName);

            ViewBag.Categories = WebSiteHelper.GetDropdownList("Category", categories);

            return View();
        }

        public ActionResult Sample2()
        {
            var categories = this.db.Categories
                .OrderBy(x => x.CategoryID)
                .ToDictionary(x => x.CategoryID.ToString(), y => y.CategoryName);

            ViewBag.Categories = WebSiteHelper.GetDropdownList(
                "Category", 
                categories,
                new { @class = "form-control" });

            return View();
        }

        public ActionResult Sample3()
        {
            var categories = this.db.Categories
                .OrderBy(x => x.CategoryID)
                .ToDictionary(x => x.CategoryID.ToString(), y => y.CategoryName);

            var selectedItem = categories.Skip(2).Take(1).FirstOrDefault();

            ViewBag.Categories = WebSiteHelper.GetDropdownList(
                "Category",
                categories,
                new { @class = "form-control" },
                selectedItem.Key);

            return View();
        }

        public ActionResult Sample4()
        {
            var categories = this.db.Categories
                .OrderBy(x => x.CategoryID)
                .ToDictionary(x => x.CategoryID.ToString(), y => y.CategoryName);

            ViewBag.Categories = WebSiteHelper.GetDropdownList(
                "Category",
                categories,
                new { @class = "form-control" },
                "",
                true);

            return View();
        }

        public ActionResult Sample5()
        {
            var categories = this.db.Categories
                .OrderBy(x => x.CategoryID)
                .ToDictionary(x => x.CategoryID.ToString(), y => y.CategoryName);

            ViewBag.Categories = WebSiteHelper.GetDropdownList(
                "Category",
                categories,
                new { @class = "form-control" },
                "",
                true,
                "Please Select Item.");

            return View();
        }

    }
}