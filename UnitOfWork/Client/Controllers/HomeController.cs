using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitOfWork.Model;
using UnitOfWork.Infrastructure;
using UnitOfWork.Repository;
using StructureMap;
using UnitOfWork.Repository.Xml;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            //IUnitOfWork uof = new UnitOfWork.Infrastructure.UnitOfWork();
            //IRacunRepository repo = new RacunRepository(uof);
            //RacunService RacunService = new RacunService(repo, uof);

            RacunService RacunService = new RacunService(ObjectFactory.GetInstance<IRacunRepository>(), ObjectFactory.GetInstance<IUnitOfWork>());

            IEnumerable<Racun> racuni = RacunService.GetRacuni();
            List<SelectListItem> dropdown = new List<SelectListItem>();
            foreach (Racun racun in racuni)
	        {
                dropdown.Add(new SelectListItem { Value = racun.Id.ToString(), Text = "Racun " + racun.Id.ToString() + " ->>" + racun.StanjeRacuna  });
	        }

            ViewBag.ListaRacuna1 = dropdown;
            ViewBag.ListaRacuna2 = dropdown;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Transfer(int ListaRacuna1, int ListaRacuna2, decimal Vrednost)
        {

            //IUnitOfWork uof = new UnitOfWork.Infrastructure.UnitOfWork();
            //IRacunRepository repo = new RacunRepositoryXml(uof);
            //RacunService RacunService = new RacunService(repo, uof);

            RacunService RacunService = new RacunService(ObjectFactory.GetInstance<IRacunRepository>(), ObjectFactory.GetInstance<IUnitOfWork>());


            Racun Racun1 = RacunService.getRacun(ListaRacuna1);
            Racun Racun2 = RacunService.getRacun(ListaRacuna2);
           

            RacunService.Transfer(Racun1, Racun2, Vrednost);

            return RedirectToAction("Index");
        }
    }
}