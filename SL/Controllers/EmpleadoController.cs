using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/empleado")]
    
    public class EmpleadoController : ApiController
    {
        [Route("{idEmpleado}")]
        [HttpPost]
        public IHttpActionResult Delete(int idEmpleado)
        {
            ML.Result result = BL.Empleado.Delete(idEmpleado);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
           
            ML.Empleado empleado = new ML.Empleado();
        

            ML.Result result = BL.Empleado.GetAllLINQ();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idEmpleado}")]
        [HttpGet]
        public IHttpActionResult GetById(int IdEmpleado)
        {
            ML.Result result = BL.Empleado.GetById(IdEmpleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idUsuario}")]
        [HttpPut]

        public IHttpActionResult Update(int idUsuario, [FromBody] ML.Empleado empleado)
        {
            empleado.Id = idUsuario;
            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
