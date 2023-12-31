﻿using System;
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
                    var usuariosLINQ = (from objEmpleado in context.Empleadoes
                                        join CatEntidadFederativa in context.CatEntidadFederativas on objEmpleado.IdEstado equals CatEntidadFederativa.IdEstado
                                        where objEmpleado.Id == IdEmpleado
                                        select new
                                        {
                                            IdEmpleado = objEmpleado.Id,
                                            NumeroNomina = objEmpleado.NumeroNomina,
                                            Nombre = objEmpleado.Nombre,
                                            ApellidoPaterno = objEmpleado.ApellidoPaterno,
                                            ApellidoMaterno = objEmpleado.ApellidoMaterno,
                                            IdEstado = CatEntidadFederativa.IdEstado
                                        }).Single();

                    result.Objects = new List<object>();
                    if (usuariosLINQ != null)
                    {


                        ML.Empleado empleado1 = new ML.Empleado();


                        empleado1.Id = usuariosLINQ.IdEmpleado;
                        empleado1.NumeroNomina = usuariosLINQ.NumeroNomina;
                        empleado1.Nombre = usuariosLINQ.Nombre;
                        empleado1.ApellidoPaterno = usuariosLINQ.ApellidoPaterno;
                        empleado1.ApellidoMaterno = usuariosLINQ.ApellidoMaterno;
                        empleado1.Estado = new ML.Estado();
                        empleado1.Estado.IdEstado = usuariosLINQ.IdEstado;

                        result.Objects.Add(empleado1);

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La tabla usuario no contiene registros";
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
                                        join Estado in context.CatEntidadFederativas on objEmpleado.IdEstado equals Estado.IdEstado
                                        select new
                                        {
                                            IdEmpleado = objEmpleado.Id,
                                            NumeroNomina = objEmpleado.NumeroNomina,
                                            NombreUsuario = objEmpleado.Nombre,
                                            ApellidoPaterno = objEmpleado.ApellidoPaterno,
                                            ApellidoMaterno = objEmpleado.ApellidoMaterno,
                                            IdEstado = Estado.IdEstado,
                                            NombreEstado = Estado.Estado
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
                            empleado.Estado.IdEstado = obj.IdEstado;
                            empleado.Estado.Nombre = obj.NombreEstado;
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

        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.YAEDLeenkedGroupEntities context = new DL.YAEDLeenkedGroupEntities())
                {
                    DL.Empleado empleadoNuevo = new DL.Empleado();
                    empleadoNuevo.Id = empleado.Id;
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
