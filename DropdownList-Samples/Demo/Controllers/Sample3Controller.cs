using Demo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class Sample3Controller : Controller
    {
        private NorthwindDbContext db = new NorthwindDbContext();

        private Dictionary<string, string> GetAllCategories()
        {
            var query = db.Categories.OrderBy(x => x.CategoryID);
            return query.ToDictionary(x => x.CategoryID.ToString(), x => x.CategoryName);
        }

        /// <summary>
        /// Gets the product by category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        private Dictionary<string, string> GetProductByCategory(int categoryId)
        {
            var query = db.Products
                          .Where(x => x.CategoryID == categoryId)
                          .Select(
                              x => new
                                   {
                                       ProductID = x.ProductID,
                                       ProductName = x.ProductName
                                   })
                          .OrderBy(x => x.ProductID);

            return query.ToDictionary(x => x.ProductID.ToString(), x => x.ProductName);
        }

        //---------------------------------------------------------------------------------
        // 連動下拉選單 - 基本
        // 使用基本的操作方式實作連動下拉選單

        public ActionResult Index()
        {
            var categories = this.GetAllCategories();
            SelectList items = new SelectList(categories, "Key", "Value");

            Sample3ViewModel model = new Sample3ViewModel();
            model.Categories = items;
            model.Products = new List<SelectListItem>();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Sample3ViewModel model)
        {
            Sample3ViewModel result = new Sample3ViewModel();

            //分類
            SelectList categories = new SelectList(
                this.GetAllCategories(),
                "Key",
                "Value",
                model.Category);

            result.Categories = categories;

            //產品
            int categoryId = 0;
            if (int.TryParse(model.Category, out categoryId))
            {
                SelectList counties = new SelectList(
                    this.GetProductByCategory(categoryId),
                    "Key",
                    "Value",
                    model.Product);
                result.Products = counties;
            }
            else
            {
                result.Products = new List<SelectListItem>();
            }

            return View(result);
        }
    }

    public class Sample3ViewModel
    {
        public string Category { get; set; }

        public string Product { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }
    }
}