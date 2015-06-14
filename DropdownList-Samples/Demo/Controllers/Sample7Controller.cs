using Demo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class Sample7Controller : Controller
    {
        private NorthwindDbContext db = new NorthwindDbContext();


        public ActionResult Demo1()
        {
            // Sample1

            var result = db.Products.OrderBy(x => x.CategoryID)
                           .ThenBy(x => x.ProductID)
                           .Select(
                               x => new
                                    {
                                        ProductID = x.ProductID,
                                        ProductName = x.ProductName,
                                        CategoryID = x.CategoryID,
                                        CategoryName = x.Category.CategoryName
                                    });

            var items = new SelectList
                (
                items: result,
                dataValueField: "ProductID",
                dataTextField: "ProductName",
                dataGroupField: "CategoryName",
                selectedValue: (object)null
                );

            ViewBag.ProductItems = items;
            return View();
        }


        private List<SelectListGroup> groups = new List<SelectListGroup>();

        private HashSet<string> disabledGroups = new HashSet<string>();

        public ActionResult Demo2()
        {
            // Sample2, 使用 List<SelectListItem>

            var result = db.Products.OrderBy(x => x.CategoryID)
                           .ThenBy(x => x.ProductID)
                           .ToList();

            var items = new List<SelectListItem>();

            foreach (var category in result.Select(x => x.Category).Distinct())
            {
                groups.Add(
                    new SelectListGroup()
                    {
                        Name = category.CategoryName
                    });
            }

            foreach (var product in result)
            {
                items.Add(
                    new SelectListItem()
                    {
                        Text = string.Format(
                            "{0} {1}",
                            product.ProductID.ToString(),
                            product.ProductName),
                        Value = product.ProductID.ToString(),
                        Group = GetGroup(product.Category.CategoryName)
                    });
            }

            ViewBag.ProductItems = items;
            return View();
        }

        private SelectListGroup GetGroup(string groupName)
        {
            SelectListGroup group =
                groups.FirstOrDefault(g => string.Equals(g.Name, groupName));

            if (group == null &&
                !string.IsNullOrWhiteSpace(groupName))
            {
                var isContains = disabledGroups.Contains(groupName);
                if (!isContains)
                {
                    disabledGroups.Add(groupName);
                }

                group = new SelectListGroup()
                        {
                            Name = groupName,
                            Disabled = isContains
                        };
                groups.Add(group);
            }
            return group;
        }
    }
}