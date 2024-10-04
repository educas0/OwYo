using Microsoft.AspNetCore.Mvc;
using OwYO.Models;
using OwYO.Models.Interfaces;
using System.Collections.Generic;

namespace OwYO.Controllers
{
    public class HeroeController : Controller
    {
        #region "Config y Constructor"

        private IDBAccess __servicioSQLServer;

        public HeroeController(IDBAccess servicioInyectado)
        {
            this.__servicioSQLServer = servicioInyectado;
        }

        #endregion

        #region "Metodos registro"

        [HttpGet]
        [Route("Heroe/Registro")]
        public IActionResult CrearHeroe()
        {
            return View("Registro");
        }

        [HttpPost]
        [Route("Heroe/Registro")]
        public IActionResult CrearHeroe(Heroe NuevoHeroe)
        {
            //se inserta el heroe
            bool Resultado = __servicioSQLServer.CrearHeroe(NuevoHeroe);

            if (Resultado)
            {
                return View("RegistroOK");
            }
            else
            {
                ErrorViewModel _ErrorModel = new ErrorViewModel();
                return View("Error", _ErrorModel);
            }
        }

        #endregion

        #region "Metodos Borrado"

        [HttpGet]
        [Route("Heroe/Borrar")]
        public IActionResult BorrarHeroe()
        {
            List<Heroe> _Heroes = this.__servicioSQLServer.RecuperarListaHeroes();
            ViewData["ListaHeroes"] = _Heroes;

            return View("Borrado");
           // return View( _Heroes);
        }

        [HttpPost]
        [Route("Heroe/Borrar")]
        public IActionResult BorrarHeroe(Heroe HeroeBorrar)
        {
            //se borra el heroe
            bool Resultado = __servicioSQLServer.BorrarHeroe(HeroeBorrar.IdHeroe);

            if (Resultado)
            {
                return View("BorradoOk");
            }
            else
            {
                ErrorViewModel _ErrorModel = new ErrorViewModel();
                return View("Error", _ErrorModel);
            }
        }

        #endregion

        #region "Metodos Actualizado"
        //seleccionarHeroe get post, actualizarHeroe get post, actualizacionOK get

        /// <summary>
        /// Get pantalla de seleccion heroe a editar
        /// </summary>
        /// <returns>Vista con modelo de lista de Heroe</returns>
        [HttpGet]               
        [Route("Heroe/SeleccionarHeroe")]
        public IActionResult SeleccionarHeroe()  // Accedo por 1ª vez a ActualizarHeroe
        {
            List<Heroe> _Heroes = this.__servicioSQLServer.RecuperarListaHeroes();
            ViewData["ListaHeroes"] = _Heroes;

            return View("UpdateList");
            // return View( _Heroes);
        }

        /// <summary>
        /// Post pantalla de selecion de heroe a editar
        /// </summary>
        /// <param name="Heroe"></param>
        /// <returns>Redireccion a get de edicion de heroe</returns>
        [HttpPost]
        [Route("Heroe/SeleccionarHeroe")]
        public IActionResult SeleccionarHeroe(int IdHeroe)
        {
            //se rcogen los datos del heroe a editar
            Heroe _Heroe = this.__servicioSQLServer.RecuperarHeroe(IdHeroe);

            return View("UpdateForm", _Heroe);
        }

        /// <summary>
        /// Post pantalla edicion con heroe editado
        /// </summary>
        /// <param name="HeroeEditado"></param>
        /// <returns>pantalla de ok o pantalla de error</returns>
        [HttpPost]
        [Route("Heroe/EdicionHeroe")]
        public IActionResult EdicionHeroe(Heroe HeroeEditado)             ////esto sólo debe redireccionar entregando el id 
        {
            //se actualizan los datos del heroe
            bool _resAct = this.__servicioSQLServer.ActualizarHeroe(HeroeEditado);

            if (_resAct)
                return View("UpdateOK");
            else
                return View("Error");
        }

        //[HttpGet]
        //[Route("Heroe/Editar")]
        //public IActionResult actualizacionOK(Heroe HeroeActualizar)
        //{
        //    //se borra el heroe
        //    bool Resultado = __servicioSQLServer.ActualizarHeroe(HeroeActualizar.IdHeroe);

        //    if (Resultado)
        //    {
        //        return View("UpdateOK");
        //    }
        //    else
        //    {
        //        ErrorViewModel _ErrorModel = new ErrorViewModel();
        //        return View("Error", _ErrorModel);
        //    }
        //}




        /// <summary>
        /// 
        /// </summary>
        /// <param name="NuevoHeroe"></param>
        /// <returns></returns>
        /// 

        #endregion

        #region "Métodos Visionado"
        [HttpGet]
        [Route("Heroe/Ver")]
        public IActionResult VerHeroe()
        {
            List<Heroe> _Heroes = this.__servicioSQLServer.RecuperarListaHeroes();
            ViewData["ListaHeroes"] = _Heroes;

            return View("Ver");
            // return View( _Heroes);
        }
        #endregion

    }



}
