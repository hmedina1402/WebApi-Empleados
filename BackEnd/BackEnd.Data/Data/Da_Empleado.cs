using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Entity;
using BackEnd.Util.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BackEnd.Data
{
    public class Da_Empleado
    {
        private readonly IOptions<ConnectionStrings> options;

        public Da_Empleado(IOptions<ConnectionStrings> options)
        {
            this.options = options;
        }

        public async Task<string> ListarEmpleado()
        {
            var ResultJson = string.Empty;
            using (var cn = new SqlConnection(options.Value.BDConnectionSQL.ToString()))
            {
                try
                {
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.Connection = cn;
                    objCommand.CommandText = "SPS_Empleados";
                    objCommand.CommandType = CommandType.StoredProcedure;

                    await cn.OpenAsync();

                    List<Ent_Empleado> lstEnt_Empleado = new List<Ent_Empleado>();

                    using (DbDataReader objReader = await objCommand.ExecuteReaderAsync())
                    {

                        while (await objReader.ReadAsync())
                        {
                            Ent_Empleado ent_Empleado = new Ent_Empleado();
                            ent_Empleado.id = Int32.Parse(objReader["id"].ToString());
                            ent_Empleado.dni = objReader["dni"].ToString();
                            ent_Empleado.nombre = objReader["nombre"].ToString();
                            ent_Empleado.apellido_p = objReader["apellido_p"].ToString();
                            ent_Empleado.apellido_m = objReader["apellido_m"].ToString();
                            ent_Empleado.fecha_nacimiento = DateTime.Parse(objReader["fecha_nacimiento"].ToString());
                            ent_Empleado.sexo = objReader["sexo"].ToString();
                            ent_Empleado.estado = bool.Parse(objReader["estado"].ToString());
                            ent_Empleado.Area = objReader["Area"].ToString();
                            ent_Empleado.fecha_registro = DateTime.Parse(objReader["fecha_registro"].ToString());
                            ent_Empleado.usuario_registro = objReader["usuario_registro"].ToString();
                            lstEnt_Empleado.Add(ent_Empleado);
                        }
                    }

                    ResultJson = JsonConvert.SerializeObject(lstEnt_Empleado == null ? null : lstEnt_Empleado);

                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return ResultJson;
        }

        public async Task<string> RegistraEmpleado(Ent_Empleado ent_Empleado)
        {
            ResponseMensaje responseMensaje;
            var ResultJson = string.Empty;

            using (var cn = new SqlConnection(options.Value.BDConnectionSQL.ToString()))
            {
                try
                {
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.Connection = cn;
                    objCommand.CommandText = "SPI_Empleados";
                    objCommand.CommandType = CommandType.StoredProcedure;

                    await cn.OpenAsync();

                    objCommand.Parameters.AddWithValue("@dni", ent_Empleado.dni);
                    objCommand.Parameters.AddWithValue("@nombre", ent_Empleado.nombre);
                    objCommand.Parameters.AddWithValue("@apellido_p", ent_Empleado.apellido_p);
                    objCommand.Parameters.AddWithValue("@apellido_m", ent_Empleado.apellido_m);
                    objCommand.Parameters.AddWithValue("@fecha_nacimiento", ent_Empleado.fecha_nacimiento);
                    objCommand.Parameters.AddWithValue("@sexo", ent_Empleado.sexo);
                    objCommand.Parameters.AddWithValue("@estado", ent_Empleado.estado);
                    objCommand.Parameters.AddWithValue("@Area", ent_Empleado.Area);
                    objCommand.Parameters.AddWithValue("@usuario_registro", ent_Empleado.usuario_registro);

                    await objCommand.ExecuteNonQueryAsync();

                    responseMensaje = new ResponseMensaje
                    {
                        codigo = "01",
                        mensaje = "Transacción Exitosa"
                    };
                }
                catch (Exception ex)
                {
                    responseMensaje = new ResponseMensaje
                    {
                        codigo = "00",
                        mensaje = ex.Message.ToString()
                    };
                }
            }

            return JsonConvert.SerializeObject(responseMensaje);
        }

    }
}
