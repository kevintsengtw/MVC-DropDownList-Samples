using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Demo.Controllers
{
    /// <summary>
    /// 強型別 EnumDropDownListFor 的設定方式 (ASP.NET MVC 5.1 新功能).
    /// </summary>
    public class Sample6Controller : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }

    public class Person
    {
        public int Id { get; set; }

        public Salutation Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }

    public enum Salutation
    {
        [Display(Name = "Mr.")]
        Mr,

        [Display(Name = "Mrs.")]
        Mrs,

        [Display(Name = "Ms.")]
        Ms,

        [Display(Name = "Dr.")]
        Doctor,

        [Display(Name = "Prof.")]
        Professor,

        Sir,
        Lady,
        Lord
    }

}