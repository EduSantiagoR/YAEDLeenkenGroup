using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                //Todo lo que se eje cute dnetro de using se libera al final, los recursos
                using (DL.YAEDLeenkedGroupEntities context = new DL.YAEDLeenkedGroupEntities())
                {
                    var empleados = (from objEmpleados in context.Empleadoes
                                     join Estado in context.CatEntidadFederativas on objEmpleados.IdEstado equals Estado.IdEstado
                                     select new
                                     {
                                            Id = objEmpleados.Id,
                                            NumeroNomina = objEmpleados.NumeroNomina,
                                            Nombre = objEmpleados.Nombre,
                                            ApellidoPaterno = objEmpleados.ApellidoPaterno,
                                            ApellidoMaterno = objEmpleados.ApellidoMaterno,
                                            IdEstado = Estado.IdEstado,
                                            NombreEstado = Estado.Estado
                                            
                                        }).ToList();

                    result.Objects = new List<object>();
                    if (empleados != null && empleados.ToList().Count > 0)
                    {

                        foreach (var obj in empleados)
                        {
                            ML.Empleado empleado = new ML.Empleado();

                            empleado.Id = obj.Id;
                            empleado.NumeroNomina = obj.NumeroNomina;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            
                            empleado.Estado = new ML.Estado();
                            empleado.Estado.IdEstado = obj.Id;
                           
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
        public static ML.Result GetById(int Id)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Todo lo que se eje cute dnetro de using se libera al final, los recursos
                using (DL.YAEDLeenkedGroupEntities context = new DL.YAEDLeenkedGroupEntities())
                {
                    var empleadosLINQ = (from objEmpleados in context.Empleadoes
                                        join Estado in context.CatEntidadFederativas on objEmpleados.IdEstado equals Estado.IdEstado
                                        where objEmpleados.Id == Id
                                        select new
                                        {
                                            Id = objEmpleados.Id,
                                            NumeroNomina = objEmpleados.NumeroNomina,
                                            Nombre = objEmpleados.Nombre,
                                            ApellidoPaterno = objEmpleados.ApellidoPaterno,
                                            ApellidoMaterno = objEmpleados.ApellidoMaterno,
                                            IdEstado = Estado.IdEstado,
                                            NombreEstado = Estado.Estado

                                        }).Single();

                    result.Objects = new List<object>();
                    if (empleadosLINQ != null)
                    {


                        ML.Empleado empleado1 = new ML.Empleado();
                        empleado1.Id = empleadosLINQ.Id;
                        empleado1.NumeroNomina = empleadosLINQ.NumeroNomina;
                        empleado1.Nombre = empleadosLINQ.Nombre;
                        empleado1.ApellidoPaterno=empleadosLINQ.ApellidoPaterno;
                        empleado1.ApellidoMaterno = empleadosLINQ.ApellidoMaterno;
                        empleado1.Estado = new ML.Estado();
                        empleado1.Estado.IdEstado = empleadosLINQ.IdEstado;
                        empleado1.Estado.Nombre = empleadosLINQ.Nombre;                     
                        result.Object = empleado1;

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
