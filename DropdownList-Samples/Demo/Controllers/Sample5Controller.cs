using Demo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo.Controllers
{
    /// <summary>
    /// 使用 jQuery 的 Ajax 來實作連動下拉選單 - 三層式連動下拉選單.
    /// </summary>
    public class Sample5Controller : Controller
    {
        private NorthwindDbContext db = new NorthwindDbContext();

        public ActionResult Index()
        {
            ViewBag.Message = "選擇客戶、訂單、產品後顯示產品資訊";

            SelectList selectList = new SelectList(this.GetCustomers(), "CustomerID", "ContactName");
            ViewBag.SelectList = selectList;

            return View();
        }

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Customer> GetCustomers()
        {
            var query = db.Customers.OrderBy(x => x.CustomerID);
            return query.ToList();
        }

        [HttpPost]
        public JsonResult Orders(string customerID)
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(customerID))
            {
                var orders = this.GetOrders(customerID);
                if (!orders.Any())
                {
                    return this.Json(items);
                }
                foreach (var order in orders)
                {
                    items.Add(
                        new KeyValuePair<string, string>(
                            order.OrderID.ToString(),
                            string.Format("{0} ({1:yyyy-MM-dd})", order.OrderID, order.OrderDate)));
                }
            }
            return this.Json(items);
        }

        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <param name="customerID">The customer ID.</param>
        /// <returns></returns>
        private IEnumerable<Order> GetOrders(string customerID)
        {
            var query = db.Orders.Where(x => x.CustomerID == customerID)
                          .OrderBy(x => x.OrderDate);
            return query.ToList();
        }

        [HttpPost]
        public JsonResult Products(string orderID)
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();

            int id = 0;

            if (!string.IsNullOrWhiteSpace(orderID) &&
                int.TryParse(orderID, out id))
            {
                var products = this.GetProducts(id);
                if (!products.Any())
                {
                    return this.Json(items);
                }
                foreach (var product in products)
                {
                    items.Add(
                        new KeyValuePair<string, string>(
                            product.ProductID.ToString(),
                            product.ProductName));
                }
            }
            return this.Json(items);
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="orderID">The order ID.</param>
        /// <returns></returns>
        private IEnumerable<Product> GetProducts(int orderID)
        {
            var query = db.Order_Details.Where(x => x.OrderID == orderID)
                          .Select(x => x.Product);
            return query.ToList();
        }

        [HttpPost]
        public ActionResult ProductInfo(string productID)
        {
            int id = 0;

            if (!string.IsNullOrWhiteSpace(productID) && int.TryParse(productID, out id))
            {
                var product = this.GetProduct(id);
                ViewData.Model = product;
            }
            return PartialView("_ProductInfo");
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="productID">The product ID.</param>
        /// <returns></returns>
        private Product GetProduct(int productID)
        {
            return db.Products.FirstOrDefault(x => x.ProductID == productID);
        }
    }
}