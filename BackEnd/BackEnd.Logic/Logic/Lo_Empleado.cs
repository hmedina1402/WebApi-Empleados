using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Entity;
using BackEnd.Util.Utils;
using Microsoft.Extensions.Options;

namespace BackEnd.Logic
{
    public class Lo_Empleado
    {
        private Da_Empleado DAEmpleado;
        private readonly IOptions<ConnectionStrings> options;
        public Lo_Empleado(IOptions<ConnectionStrings> options)
        {
            this.options = options;
        }

        public async Task<string> ListarEmpleado()
        {
            DAEmpleado = new Da_Empleado(options);
            return await DAEmpleado.ListarEmpleado();
        }

        public async Task<string> RegistraEmpleado(Ent_Empleado ent_Empleado)
        {
            DAEmpleado = new Da_Empleado(options);
            return await DAEmpleado.RegistraEmpleado(ent_Empleado);

        }

    }
}
