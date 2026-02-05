using Microsoft.Data.SqlClient;
using MvcNetCoreLinqToSqlInjection.Models;
using System.Data;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresSQLServer
    {
        private SqlConnection cn;
        private SqlCommand com;
        private DataTable tablaDoctor;

        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;TrustServerCertificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            string sql = "select * from DOCTOR";

            SqlDataAdapter ad = new SqlDataAdapter(sql, this.cn);
            this.tablaDoctor = new DataTable();
            ad.Fill(this.tablaDoctor);

        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor
                {
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                    IdHospital = row.Field<int>("HOSPITAL_COD")
                };
                doctores.Add(doc);
            }
            return doctores;
        }

        public async Task CreateDoctorAsync(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values (@idHospital,@id,@apellido, @especialidad, @salario ) ";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.com.Parameters.AddWithValue("@id", idDoctor);
            this.com.Parameters.AddWithValue("@idHospital", idHospital);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
        public async Task UpdateDoctorAsync(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "update DOCTOR set HOSPITAL_COD = @idHospital , APELLIDO = @apellido, ESPECIALIDAD = @especialidad, SALARIO = @salario " +
                "where DOCTOR_NO = @id)";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.com.Parameters.AddWithValue("@id", idDoctor);
            this.com.Parameters.AddWithValue("@idHospital", idHospital);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

    }
}
