using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        [HttpGet]
        public IActionResult Cine()
        {
            ML.Cine cine=new ML.Cine();
            ML.Result result = BL.Cine.GetAll();
            cine.Cines = result.Objects;
            return View(cine);
        }
        [HttpGet]
        public IActionResult Form(int? IdCine)
        {
            ML.Result resultzona = BL.Zona.GetAll();
            ML.Cine cine = new ML.Cine();
            if (IdCine==null)
            {
                
               
                cine.Zona = new ML.Zona();
                cine.Zona.Zonas = resultzona.Objects;
                return View(cine);

            }
            else
            {
                ML.Result result = BL.Cine.GetById(IdCine.Value);
                cine = (ML.Cine)result.Object;
                cine.Zona.Zonas = resultzona.Objects;
                return View(cine);
            }
            
        }

        [HttpPost]

        public ActionResult Form(ML.Cine cine)
        {
            ML.Result result=new ML.Result();
            if (cine.IdCine == 0)
            {
                result = BL.Cine.Add(cine);
                ViewBag.Message = result.Message;
                return View("Modal");



            }
            else
            {
                result = BL.Cine.Update(cine);;
                ViewBag.Message = result.Message;
                return View("Modal");

            }
          

        }
        [HttpGet]
        public ActionResult Delete(int IdCine) {
            ML.Result result=BL.Cine.Delete(IdCine);
            ViewBag.Message = result.Message;
            return PartialView("Modal");
        }
    }
}
