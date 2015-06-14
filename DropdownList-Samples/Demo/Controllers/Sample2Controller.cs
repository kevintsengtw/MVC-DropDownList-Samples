using Demo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo.Controllers
{
    /// <summary>
    /// DropDownListFor 的設定方式.
    /// </summary>
    public class Sample2Controller : Controller
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
            //DropDownListFor
            //ViewModel - IEnumerable<SelectListItem>

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

            Sample2ViewModel model = new Sample2ViewModel();
            model.CategoryList = items;

            return View(model);
        }

        public ActionResult Method2()
        {
            //DropDownListFor
            //ViewModel - SelectList

            var categories = this.GetAllCategories();
            SelectList items = new SelectList(categories, "Key", "Value");

            Sample2ViewModel result = new Sample2ViewModel();
            result.CategorySelectList = items;

            return View(result);
        }


        public ActionResult Method3()
        {
            //DropDownListFor
            //ViewModel - IEnumerable<SelectListItem> - POST

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

            SampleViewModel result = new SampleViewModel();
            result.CategoryList = items;

            return View(result);

        }

        [HttpPost]
        public ActionResult Method3(SampleViewModel model)
        {
            var categories = this.GetAllCategories();

            SampleViewModel result = new SampleViewModel();

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in categories)
            {
                items.Add(new SelectListItem()
                {
                    Text = category.Value,
                    Value = category.Key,
                    Selected = category.Value.Equals(model.CategoryList)
                });
            }
            result.CategoryList = items;

            return View(result);
        }
    }

    public class SampleViewModel
    {
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }

    public class Sample2ViewModel
    {
        public List<SelectListItem> CategoryList { get; set; }

        public SelectList CategorySelectList { get; set; }
    }
}