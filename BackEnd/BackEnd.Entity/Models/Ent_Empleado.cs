using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Entity
{
    public class Ent_Empleado
    {
        public int id { get; set; }
        public string dni { get; set; }
        public string nombre { get; set; }
        public string apellido_p { get; set; }
        public string apellido_m { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string sexo { get; set; }
        public bool estado { get; set; }
        public string Area { get; set; }
        public DateTime fecha_registro { get; set; }
        public string usuario_registro { get; set; }
    }
}
