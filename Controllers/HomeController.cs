using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OwYO.Models;
using OwYO.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OwYO.Controllers
{
    public class HomeController : Controller
    {
        private IDBAccess __servicioSQLServer;
        private IHttpContextAccessor __contextoHttp;

        private readonly ILogger<HomeController> _logger;

        public HomeController(IDBAccess servicioInyectado,
                                ILogger<HomeController> logger,
                                IHttpContextAccessor contextoHttp)
        {
            this.__contextoHttp = contextoHttp;
            this.__servicioSQLServer = servicioInyectado;
            _logger = logger;
        }

        public IActionResult HeroeSession()
        {
            List<Heroe> _Heroes = this.__servicioSQLServer.RecuperarListaHeroes();
            ViewData["ListaHeroes"] = _Heroes;

            return View();
        }


        [HttpPost]
        public IActionResult SeleccionarHeroe(Heroe _heroe)
        {

            //se rcogen los datos del heroe con el que haremos la sesión
            Heroe _Heroe = this.__servicioSQLServer.RecuperarHeroe(_heroe.IdHeroe);

            this.__contextoHttp.HttpContext.Session.SetString("heroe", JsonConvert.SerializeObject(_Heroe)); //Ya hay sesión!

            return View("Indice");
        }
    }
}
