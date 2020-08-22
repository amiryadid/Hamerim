using System.Web.Mvc;
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
            return View();
        }
    }
}