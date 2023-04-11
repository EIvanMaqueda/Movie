using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        public IActionResult Cine()
        {
            ML.Cine cine=new ML.Cine();
            ML.Result result = BL.Cine.GetAll();
            cine.Cines = result.Objects;
            return View(cine);
        }
        public IActionResult Form()
        {
            ML.Result resultzona=BL.Zona.GetAll();
            ML.Cine cine=new ML.Cine(); 
            cine.Zona=new ML.Zona();
            cine.Zona.Zonas = resultzona.Objects;
            return View(cine);
        }
    }
}
