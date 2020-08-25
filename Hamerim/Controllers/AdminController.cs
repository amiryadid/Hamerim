using System.Data.Entity;
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
            User currentUser = (User)this.Session["User"];

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
                ViewBag.Clubs = ctx.Clubs.Include(club => club.Address).ToList();
                ViewBag.Services = ctx.Services.ToList();
                ViewBag.Categories = ctx.ServiceCategories.ToList();
                ViewBag.Orders = ctx.Orders.ToList();
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddClub(string name, int cost, string city, string street, int houseNumber)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.Clubs.Add(new Club()
                {
                    Name = name,
                    Cost = cost,
                    Address = new ClubAddress()
                    {
                        City = city,
                        Street = street,
                        HouseNumber = houseNumber
                    }
                });
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddCategory(string title)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.ServiceCategories.Add(new ServiceCategory()
                {
                    Title = title
                });

                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddService(string title, int cost, int serviceId)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.Services.Add(new Service()
                {
                    Title = title,
                    Cost = cost,
                    Category = ctx.ServiceCategories.Find(serviceId)
                });
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditClub(int id, string name, int cost)
        {
            using (var ctx = new HamerimDbContext())
            {
               Club club = ctx.Clubs.Find(id);
               club.Name = name;
               club.Cost = cost;
               ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPut]
        public ActionResult EditCategory(int id, string title)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.ServiceCategories.Find(id).Title = title;
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPut]
        public ActionResult EditService(int id, string title, int cost)
        {
            using (var ctx = new HamerimDbContext())
            {
                Service service = ctx.Services.Find(id);
                service.Title = title;
                service.Cost = cost;
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult DeleteOrder(int id)
        {
            using (var ctx = new HamerimDbContext())
            {
                ctx.Orders.Remove(ctx.Orders.Find(id));
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
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
    }
}