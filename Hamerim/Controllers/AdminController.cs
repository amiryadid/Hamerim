using System.Linq;
using System.Web.Mvc;
using Hamerim.Data;
using Hamerim.Models;
using Hamerim.Services;

namespace Hamerim.Controllers
{
    public class AdminController : Controller
    {
        readonly IStatisticsService statisticsService;
        readonly IPermissionsService permissionsService;

        public AdminController(IStatisticsService statisticsService, IPermissionsService permissionsService)
        {
            this.statisticsService = statisticsService;
            this.permissionsService = permissionsService;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User currentUser = (User) this.Session["User"];

            // Validate any request to this controller
            if (currentUser == null ||
                !this.permissionsService.ValidateAdmin(currentUser.Username, currentUser.Password))
                filterContext.Result = RedirectToAction("Index", "Login");
        }

        // GET: Admin
        public ActionResult Index()
        {
            using (var ctx = new HamerimDbContext())
            {
                ViewBag.Clubs = ctx.Clubs.ToList();
                ViewBag.Services = ctx.Services.ToList();
                ViewBag.Categories = ctx.ServiceCategories.ToList();
                ViewBag.Orders = ctx.Orders.ToList();
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddClub(Club club)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.Clubs.Add(club);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddCategory(ServiceCategory category)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.ServiceCategories.Add(category);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddService(Service service)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.Services.Add(service);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPut]
        public ActionResult EditClub(Club club)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.Entry(ctx.Clubs.Find(club.Id)).CurrentValues.SetValues(club);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPut]
        public ActionResult EditCategory(ServiceCategory category)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.Entry(ctx.ServiceCategories.Find(category.Id)).CurrentValues.SetValues(category);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPut]
        public ActionResult EditService(Service service)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.Entry(ctx.Services.Find(service.Id)).CurrentValues.SetValues(service);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult DeleteOrder(Order order)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.Orders.Remove(order);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}