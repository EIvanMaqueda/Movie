using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EstadisticaController : Controller
    {
        public IActionResult Index()
        {
            ML.Porcentajes porcentaje=new ML.Porcentajes();
            ML.Result result = BL.Cine.Porcentajes();
            ML.Result resultCines = BL.Cine.GetAll();
            porcentaje = (ML.Porcentajes)result.Object;
            porcentaje.Cines = new List<object>();
            porcentaje.Cines = resultCines.Objects;
            return View(porcentaje);
        }
    }
}
