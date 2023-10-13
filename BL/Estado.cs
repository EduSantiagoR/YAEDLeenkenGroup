using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.YAEDLeenkedGroupEntities context = new DL.YAEDLeenkedGroupEntities())
                {
                    var query = (from Estado in context.CatEntidadFederativas
                                 select new
                                 {
                                     IdEstado = Estado.IdEstado,
                                     Nombre = Estado.Estado
                                 });
                    result.Objects = new List<object>();
                    if(query != null && query.ToList().Count > 0)
                    {
                        foreach(var item in query)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = item.IdEstado;
                            estado.Nombre = item.Nombre;
                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al consultar los registros.";
                    }
                }
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
