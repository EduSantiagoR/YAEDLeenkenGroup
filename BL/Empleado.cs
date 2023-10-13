using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetById(int IdEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Todo lo que se eje cute dnetro de using se libera al final, los recursos
                using (DL.YAEDLeenkedGroupEntities context = new DL.YAEDLeenkedGroupEntities())
                {
                    var empleadosLINQ = (from objEmpleado in context.Empleadoes
                                        join Estado in context.CatEntidadFederativas on objEmpleado.IdEstado equals Estado.IdEstado
                                        where objEmpleado.Id == IdEmpleado
                                        select new
                                        {
                                            IdEmpleado   = objEmpleado.Id,
                                            NumeroNomina   = objEmpleado.NumeroNomina,
                                            NombreUsuario = objEmpleado.Nombre,
                                            ApellidoPaterno = objEmpleado.ApellidoPaterno,
                                            ApellidoMaterno = objEmpleado.ApellidoMaterno,
                                            Estado = Estado.Estado,
                                            IdEstado = Estado.IdEstado
                                        }).Single();

                    result.Objects = new List<object>();
                    if (empleadosLINQ != null)
                    {


                        ML.Empleado empleado = new ML.Empleado();


                        empleado.Id = empleadosLINQ.IdEmpleado;
                        empleado.Nombre = empleadosLINQ.NombreUsuario;
                        empleado.NumeroNomina = empleadosLINQ.NumeroNomina;
                        empleado.ApellidoPaterno = empleadosLINQ.ApellidoPaterno;
                        empleado.ApellidoMaterno = empleadosLINQ.ApellidoMaterno;
                        empleado.Estado = new ML.Estado();
                        empleado.Estado = empleado.Estado;
                       
                        result.Object = empleado;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La tabla empleado no contiene registros";
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                //Todo lo que se eje cute dnetro de using se libera al final, los recursos
                using (DL.YAEDLeenkedGroupEntities context = new DL.YAEDLeenkedGroupEntities())
                {
                    var empleadosLINQ = (from objEmpleado in context.Empleadoes
                                        join Estado in context.Empleadoes on objEmpleado.IdEstado equals Estado.IdEstado
                                        select new
                                        {
                                            IdEmpleado = objEmpleado.Id,
                                            NumeroNomina = objEmpleado.NumeroNomina,
                                            NombreUsuario = objEmpleado.Nombre,
                                            ApellidoPaterno = objEmpleado.ApellidoPaterno,
                                            ApellidoMaterno = objEmpleado.ApellidoMaterno,
                                            Estado = Estado.IdEstado
                                        }).ToList();

                    result.Objects = new List<object>();
                    if (empleadosLINQ != null && empleadosLINQ.ToList().Count > 0)
                    {

                        foreach (var obj in empleadosLINQ)
                        {
                            ML.Empleado empleado = new ML.Empleado();


                            empleado.Id = obj.IdEmpleado;
                            empleado.Nombre = obj.NombreUsuario;
                            empleado.NumeroNomina = obj.NumeroNomina;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Estado = new ML.Estado();
                            empleado.Estado = empleado.Estado;
                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La tabla empleados no contiene registros";
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

    }
}
