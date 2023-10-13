using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.YAEDLeenkedGroupEntities context = new DL.YAEDLeenkedGroupEntities())
                {
                    DL.Empleado empleadoNuevo = new DL.Empleado();
                    empleadoNuevo.Nombre = empleado.Nombre;
                    empleadoNuevo.NumeroNomina = empleado.NumeroNomina;
                    empleadoNuevo.ApellidoPaterno = empleado.ApellidoPaterno;
                    empleadoNuevo.ApellidoMaterno = empleado.ApellidoMaterno;
                    empleadoNuevo.IdEstado = empleado.Estado.IdEstado;
                    context.Empleadoes.Add(empleadoNuevo);
                    context.SaveChanges();
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.YAEDLeenkedGroupEntities context = new DL.YAEDLeenkedGroupEntities())
                {
                    var query = (from a in context.Empleadoes where a.Id == empleado.Id select a).SingleOrDefault();
                    if(query != null)
                    {
                        query.NumeroNomina = empleado.NumeroNomina;
                        query.Nombre = empleado.Nombre;
                        query.ApellidoPaterno = empleado.ApellidoPaterno;
                        query.ApellidoMaterno = empleado.ApellidoMaterno;
                        query.IdEstado = empleado.Estado.IdEstado;
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar "+empleado.Id;
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
        public static ML.Result Delete(int idEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.YAEDLeenkedGroupEntities context = new DL.YAEDLeenkedGroupEntities())
                {
                    var query = (from a in context.Empleadoes where a.Id == idEmpleado select a).First();
                    context.Empleadoes.Remove(query);
                    context.SaveChanges();
                }
                result.Correct = true;
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
