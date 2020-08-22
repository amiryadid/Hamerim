using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hamerim.Controllers
{
    public class OrderController : Controller
    {
        List<Hamerim.Models.Club> allClubs = new List<Hamerim.Models.Club>() {
            new Hamerim.Models.Club(){ Id=1, Name="פורום1", Cost=2500, Address=new Hamerim.Models.ClubAddress(){ City="באר שבע", Street="אחד העם", HouseNumber=20 } },
            new Hamerim.Models.Club(){ Id=2, Name="פורום2", Cost=2500, Address=new Hamerim.Models.ClubAddress(){ City="באר שבע", Street="אחד העם", HouseNumber=20 } },
            new Hamerim.Models.Club(){ Id=3, Name="פורום3", Cost=2500, Address=new Hamerim.Models.ClubAddress(){ City="באר שבע", Street="אחד העם", HouseNumber=20 } },
            new Hamerim.Models.Club(){ Id=4, Name="פורום4", Cost=2500, Address=new Hamerim.Models.ClubAddress(){ City="באר שבע", Street="אחד העם", HouseNumber=20 } },
            new Hamerim.Models.Club(){ Id=5, Name="פורום5", Cost=2500, Address=new Hamerim.Models.ClubAddress(){ City="באר שבע", Street="אחד העם", HouseNumber=20 } },
            new Hamerim.Models.Club(){ Id=6, Name="פורום1", Cost=2500, Address=new Hamerim.Models.ClubAddress(){ City="באר שבע", Street="אחד העם", HouseNumber=20 } },
            new Hamerim.Models.Club(){ Id=7, Name="פורום2", Cost=2500, Address=new Hamerim.Models.ClubAddress(){ City="באר שבע", Street="אחד העם", HouseNumber=20 } },
            new Hamerim.Models.Club(){ Id=8, Name="פורום3", Cost=2500, Address=new Hamerim.Models.ClubAddress(){ City="באר שבע", Street="אחד העם", HouseNumber=20 } },
            new Hamerim.Models.Club(){ Id=9, Name="פורום4", Cost=2500, Address=new Hamerim.Models.ClubAddress(){ City="באר שבע", Street="אחד העם", HouseNumber=20 } }
        };

        // GET: Order
        public ActionResult NewOrder()
        {
            ViewBag.Clubs = allClubs;
            return View();
        }

        public ActionResult AfterChooseClub(int Id)
        {
            ViewBag.chosenClub = allClubs.Where(cl => cl.Id == Id).First();
            return View();
        }
    }
}