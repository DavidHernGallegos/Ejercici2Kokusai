using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Modelo;
using System.Drawing.Printing;

namespace Presentacion.Controllers
{
    public class CuentaHabitanteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            Dictionary<string, object> result = Negocio.CuentaHabitante.GetAll();
            bool respuesta = (bool)result["Respuesta"];
            string mensaje = (string)result["Mensaje"];
            if (respuesta)
            {
                Modelo.CuentaHabitante cuentaHabitante = (Modelo.CuentaHabitante)result["CuentaHabitante"];

                return View(cuentaHabitante);
            }
            else
            {
                return View();
            }
        }




        [HttpGet]
        public IActionResult Form(int? IdCuentaHabitante)
        {

            
            Modelo.CuentaHabitante cuentaHabitante = new Modelo.CuentaHabitante();

            if (IdCuentaHabitante == null)
            {



                return View(cuentaHabitante);
            }
            else
            {

                Dictionary<string, object> diccionario = Negocio.CuentaHabitante.GetById(IdCuentaHabitante.Value);
                cuentaHabitante = (Modelo.CuentaHabitante)diccionario["CuentaHabitante"];


                return View(cuentaHabitante);
            }

        }

        [HttpPost]
        public IActionResult Form(Modelo.CuentaHabitante cuentaHabitante)
        {


            

            if (cuentaHabitante.Id > 0)
            {

                Dictionary<string, object> diccionario = Negocio.CuentaHabitante.Update(cuentaHabitante);
                bool respuesta = (bool)diccionario["Respuesta"];
                string mensaje = (string)diccionario["Mensaje"];

                if (respuesta)
                {
                    ViewBag.Mensaje = mensaje;
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = mensaje;
                    return PartialView("Modal");
                }
                
            }
            else
            {

                Dictionary<string, object> diccionario = Negocio.CuentaHabitante.Add(cuentaHabitante);
                bool respuesta = (bool)diccionario["Respuesta"];
                string mensaje = (string)diccionario["Mensaje"];

                if (respuesta)
                {
                    ViewBag.Mensaje = mensaje;
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = mensaje;
                    return PartialView("Modal");
                }


            }

        }


        public  IActionResult Delete(int IdCuentaHabitante)
        {
            Dictionary<string, object> diccionario = Negocio.CuentaHabitante.Delete(IdCuentaHabitante);
            bool respuesta = (bool)diccionario["Respuesta"];
            string mensaje = (string)diccionario["Mensaje"];

            if (respuesta)
            {
                ViewBag.Mensaje = mensaje;
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return PartialView("Modal");
            }
        }

        public IActionResult CajeroAutomatico()
        {
            return View();
        }

       
        public IActionResult RetirarEfectivo(int? idCuentaHabitante, decimal? cantidadRetirada)
        {
            //Dictionary<string, object> diccionario = Negocio.CuentaHabitante.GetById(idCuentaHabitante.Value);
            //Modelo.CuentaHabitante cuentaHabitante = (Modelo.CuentaHabitante)diccionario["CuentaHabitante"];
            Modelo.Moneda moneda = new Modelo.Moneda();
            if (idCuentaHabitante == null &&  cantidadRetirada == null)
            {

                ViewBag.Saldo = "";
                return View();
            }
            else
            {
                Dictionary<string, object> diccionario = Negocio.CuentaHabitante.RegistarTransaccion(idCuentaHabitante.Value, cantidadRetirada.Value);
                Dictionary<int, int> diccionarioMonedads = Negocio.CuentaHabitante.CalcularBilletesYMonedas(cantidadRetirada.Value);
                
                moneda.Monedas = new List<object>();

                foreach (var item in diccionarioMonedads)
                {
                    Modelo.Moneda monedaObj = new Modelo.Moneda();
                    if (item.Key >= 100)
                    {
                        
                        Console.WriteLine($"Billete {item.Key} = {item.Value}");
                        monedaObj.Cantidad = item.Value;
                        monedaObj.MonedaTipo = item.Key;

                        moneda.Monedas.Add(monedaObj);
                    }
                    else
                    {
                        Console.WriteLine($"Moneda {item.Key} = {item.Value}");
                        monedaObj.Cantidad = item.Value;
                        monedaObj.MonedaTipo = item.Key;

                        moneda.Monedas.Add(monedaObj);
                    }
                }


                bool respuesta = (bool)diccionario["Respuesta"];
                string mensaje = (string)diccionario["Mensaje"];

                if (respuesta)
                {
                    ViewBag.Mensaje = mensaje;
                    return View(moneda);
                }
                else
                {
                    ViewBag.Mensaje = mensaje;
                    return PartialView("Modal");
                }

               
            }
        }

        
        public IActionResult ConsulatarSaldo(int? idCuenta)
        {
            if(idCuenta == null)
            {
                return View();
            }

            Dictionary<string, object> diccionario=  Negocio.CuentaHabitante.GetById(idCuenta.Value);
            Modelo.CuentaHabitante cuentaHabitante = (Modelo.CuentaHabitante)diccionario["CuentaHabitante"];
            bool respuesta = (bool)diccionario["Respuesta"];
            string mensaje = (string)diccionario["Mensaje"];

            if (respuesta)
            {
                ViewBag.Mensaje = mensaje;
                return View(cuentaHabitante);
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return PartialView("Modal");
            }
        }

        
        
        
       }
        
    }

