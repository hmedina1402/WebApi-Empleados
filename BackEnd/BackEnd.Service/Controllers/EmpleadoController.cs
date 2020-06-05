using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Logic;
using Microsoft.Extensions.Options;
using BackEnd.Util.Utils;
using BackEnd.Entity;

namespace BackEnd.Service.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]




    [Route("/Empleado")]
    public class EmpleadoController : ControllerBase
    {
        private Lo_Empleado lo_Empleado;
        private readonly IOptions<ConnectionStrings> options;

        public EmpleadoController(IOptions<ConnectionStrings> options)
        {
            this.options = options;
        }

        [HttpGet()]
        [Route("Listado")]
        public async Task<string> ListarEmpleado()
        {
            try
            {
                lo_Empleado = new Lo_Empleado(options);
                var LstEmpleados = await this.lo_Empleado.ListarEmpleado();
                return LstEmpleados;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost()]
        [Route("Inserta")]
        public async Task<string> RegistraEmpleado([FromBody] Ent_Empleado ent_Empleado)
        {
            try
            {
                lo_Empleado = new Lo_Empleado(options);
                var LstEmpleados = await this.lo_Empleado.RegistraEmpleado(ent_Empleado);
                return LstEmpleados;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
