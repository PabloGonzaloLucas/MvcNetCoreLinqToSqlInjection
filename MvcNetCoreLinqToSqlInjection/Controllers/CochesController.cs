using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;
namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class CochesController : Controller
    {
        private ICoche car;
        public CochesController(ICoche car)
        {
            this.car = car;
        }
        public IActionResult Index()
        {
            return View(this.car);
        }

        [HttpPost]
        public IActionResult Index(string accion)
        {
            if (accion.ToLower() == "acelerar")
            {
                this.car.Acelerar();
            }
            else if (accion.ToLower() == "frenar")
            {
                this.car.Frenar();
            }
            return View(this.car);
        }
    }
}
