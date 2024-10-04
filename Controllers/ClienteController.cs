using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwYO.Models;
using OwYO.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OwYO.Controllers
{
    public class ClienteController : Controller
    {
        private IDBAccess __servicioSQLServer;
        private IHttpContextAccessor __contextoHttp;


        public ClienteController(IDBAccess servicioInyectado,
                        IHttpContextAccessor contextoHttp)
        {
            this.__contextoHttp = contextoHttp;
            this.__servicioSQLServer = servicioInyectado;
        }

        // GET: ClienteController
        public ActionResult Index()
        {
            return View();
        }

        #region //LOGIN
        /*
         pruebas:  email: trinity@gmail.com
                   password: trinity1234
         */
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Cliente_Login clienteLogin)
        {

            //evito fallo de modelo invalido por estar prop.login de objeto Credenciales
            //sin mapear en formulario login...los looser hacen un ViewModel aparte

            //if (ModelState.GetValidationState("Login") == ModelValidationState.Valid &&
            //    ModelState.GetValidationState("Password") == ModelValidationState.Valid
            //    )
            if (ModelState.IsValid)
            {
                //comprobar si esas credenciales son correctas en la BD...
                Cliente _cliente = this.__servicioSQLServer.ComprobarLogin(clienteLogin.Login, clienteLogin.Password);

                if (_cliente != null)
                {
                    // almacenar datos en variable sesion....
                    HttpContext.Session.SetString("datoscliente", JsonSerializer.Serialize(_cliente)); 


                    //redireccionamos a vista de tienda
                    return RedirectToAction("HeroeSession", "Home");
                }
                else
                {
                    //ha habido un fallo en la comprobacion de credenciales
                    ModelState.AddModelError("", "Nickname o password invalidos...intentelo de nuevo");
                    return View(clienteLogin);
                }

            }
            else
            {
                return View(clienteLogin);
            }
        }

        #endregion
    }
}
