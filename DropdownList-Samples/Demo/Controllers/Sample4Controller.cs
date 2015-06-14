using Demo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class Sample4Controller : Controller
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
        // 連動下拉選單 - jQuery
        // 使用 jQuery 的 Ajax 來實作連動下拉選單.

        public ActionResult Index()
        {
            SelectList result = new SelectList(this.GetAllCategories(), "Key", "Value");
            ViewBag.Category = result;
            return View();
        }

        [HttpPost]
        public JsonResult Products(string category)
        {
            var items = new List<KeyValuePair<string, string>>();

            //產品
            int categoryId = 0;
            if (int.TryParse(category, out categoryId))
            {
                var couties = this.GetProductByCategory(categoryId);
                foreach (var county in couties)
                {
                    items.Add(new KeyValuePair<string, string>(county.Key, county.Value));
                }
            }

            return this.Json(items);
        }
    }
}