using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CuentaHabitante
    {
        public static Dictionary<string, object> GetAll()
        {
            Modelo.CuentaHabitante cuentaHabitante = new Modelo.CuentaHabitante();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "CuentaHabitante", cuentaHabitante }, { "Respuesta", false }, { "Mensaje", "" } };

            try
            {
                using (Datos.BancoKokusaiContext context = new Datos.BancoKokusaiContext())
                {
                    var query = (from cuentaHabitanteT in context.Cuentahabientes

                                 select new
                                 {
                                     IdCuentaHabitante = cuentaHabitanteT.Id,
                                     Nombre = cuentaHabitanteT.NombreCompleto,
                                     Saldo = cuentaHabitanteT.Saldo

                                 }).ToList();

                    if (query.Count > 0)
                    {
                        cuentaHabitante.CuentaHabitantes = new List<object>();
                        foreach (var item in query)
                        {
                            Modelo.CuentaHabitante cuentaHabitanteObj = new Modelo.CuentaHabitante();
                            cuentaHabitanteObj.Id= item.IdCuentaHabitante;
                            cuentaHabitanteObj.NombreCompleto = item.Nombre;
                            cuentaHabitanteObj.Saldo = item.Saldo;


                            cuentaHabitante.CuentaHabitantes.Add(cuentaHabitanteObj);
                        }

                        diccionario["CuentaHabitante"] = cuentaHabitante;
                        diccionario["Respuesta"] = true;
                        diccionario["Mensaje"] = "Se cargaron todos los datos";
                    }
                    else
                    {
                        diccionario["Mensaje"] = "No se cargaron todos los datos";
                    }
                }


            }
            catch (Exception ex)
            {
                diccionario["Mensaje"] = "No se cargaron todos los datos";
            }

            return diccionario;

        }

        public static Dictionary<string, object> GetById(int idCuentaHabitante)
        {
            Modelo.CuentaHabitante cuentaHabitante = new Modelo.CuentaHabitante();

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "CuentaHabitante", cuentaHabitante }, { "Respuesta", false }, { "Mensaje", "" } };

            try
            {

                using (Datos.BancoKokusaiContext context = new Datos.BancoKokusaiContext())
                {

                    var query = (from cuentaT in context.Cuentahabientes
                                 where cuentaT.Id == idCuentaHabitante
                                 select new
                                 {
                                     IdCuentaHabitante = cuentaT.Id,
                                     Nombre = cuentaT.NombreCompleto,
                                     Saldo = cuentaT.Saldo

                                 }).SingleOrDefault();

                    if (query != null)
                    {

                        cuentaHabitante.Id = query.IdCuentaHabitante;
                        cuentaHabitante.NombreCompleto = query.Nombre;
                        cuentaHabitante.Saldo = query.Saldo;


                        diccionario["CuentaHabitante"] = cuentaHabitante;
                        diccionario["Respuesta"] = true;
                        diccionario["Mensaje"] = "Se cargo el registro";

                    }
                    else
                    {
                        diccionario["Mensaje"] = "No se cargo el registro";

                    }

                }
            }
            catch (Exception e)
            {

                diccionario["Mensaje"] = "Se cargo el registro" + e;
            }

            return diccionario;

        }


        public static Dictionary<string, object> Add(Modelo.CuentaHabitante cuentaHabitante)
        {

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Respuesta", false }, { "Mensaje", "" } };

            try
            {

                using (Datos.BancoKokusaiContext context = new Datos.BancoKokusaiContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"AgregarCuentahabiente '{cuentaHabitante.NombreCompleto}', { cuentaHabitante.Saldo}");

                    if (query > 0)
                    {


                        diccionario["Respuesta"] = true;
                        diccionario["Mensaje"] = "Se agrego el registro";

                    }
                    else
                    {
                        diccionario["Mensaje"] = "No se agrego el registro";

                    }

                }
            }
            catch (Exception e)
            {

                diccionario["Mensaje"] = "No Se agrego el registro" + e;
            }

            return diccionario;

        }


        public static Dictionary<string, object> Update(Modelo.CuentaHabitante cuentaHabitante)
        {

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Respuesta", false }, { "Mensaje", "" } };

            try
            {

                using (Datos.BancoKokusaiContext context = new Datos.BancoKokusaiContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"ActualizarCuentahabiente  {cuentaHabitante.Id},'{cuentaHabitante.NombreCompleto}', {cuentaHabitante.Saldo}");

                    if (query > 0)
                    {


                        diccionario["Respuesta"] = true;
                        diccionario["Mensaje"] = "Se actualizo el registro";

                    }
                    else
                    {
                        diccionario["Mensaje"] = "No se actualizo el registro";

                    }

                }
            }
            catch (Exception e)
            {

                diccionario["Mensaje"] = "No Se actualizo el registro" + e;
            }

            return diccionario;


        }

        public static Dictionary<string, object> Delete(int idCuentaHabitante)
        {

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Respuesta", false }, { "Mensaje", "" } };

            try
            {

                using (Datos.BancoKokusaiContext context = new Datos.BancoKokusaiContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"EliminarCuentahabiente  {idCuentaHabitante}");

                    if (query > 0)
                    {


                        diccionario["Respuesta"] = true;
                        diccionario["Mensaje"] = "Se elimino el registro";

                    }
                    else
                    {
                        diccionario["Mensaje"] = "No se elimino el registro";

                    }

                }
            }
            catch (Exception e)
            {

                diccionario["Mensaje"] = "no se elimino  el registro" + e;
            }

            return diccionario;


        }


        public static Dictionary<string, object> RegistarTransaccion(int IdCuentaHabitante, decimal CantidadRetirada)
        {

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Respuesta", false }, { "Mensaje", "" } };

            try
            {

                using (Datos.BancoKokusaiContext context = new Datos.BancoKokusaiContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"RegistrarTransaccion '{IdCuentaHabitante}', {CantidadRetirada}");

                    if (query > 0)
                    {


                        diccionario["Respuesta"] = true;
                        diccionario["Mensaje"] = "Se realizo la transaccion";

                    }
                    else
                    {
                        diccionario["Mensaje"] = "No Se realizo la transaccion";

                    }

                }
            }
            catch (Exception e)
            {

                diccionario["Mensaje"] = "No Se realizo la transaccion" + e;
            }

            return diccionario;

        }


        public static Dictionary<int, int> CalcularBilletesYMonedas(decimal cantidad)
        {


            Dictionary<int, int> resultado = new Dictionary<int, int>();

            Dictionary<int, int> denominacionesBilletes;
            Dictionary<int, int> denominacionesMonedas;

            denominacionesBilletes = new Dictionary<int, int>
            {
             
                { 500, 0 },
                { 200, 0 },
                { 100, 0 },
                { 50, 0 },
                { 20, 0 }
            };

            denominacionesMonedas = new Dictionary<int, int>
            {
                { 10, 0 },
                { 5, 0 },
                { 2, 0 },
                { 1, 0 }
            };


           
            int cantidadCentavos = (int)cantidad;

           
            foreach (var denominacion in denominacionesBilletes.Keys)
            {
                int cantidadBilletes = cantidadCentavos / denominacion;
                if (cantidadBilletes > 0 && denominacionesBilletes.ContainsKey(denominacion))
                {
                    resultado[denominacion] = cantidadBilletes;
                    cantidadCentavos -= cantidadBilletes * denominacion;
                }
            }
            foreach (var denominacion in denominacionesMonedas.Keys)
            {
                int cantidadMonedas = cantidadCentavos / denominacion;
                if (cantidadMonedas > 0 && denominacionesMonedas.ContainsKey(denominacion))
                {
                    resultado[denominacion] = cantidadMonedas;
                    cantidadCentavos -= cantidadMonedas * denominacion;
                }
            }

            return resultado;
        }



    }
}
