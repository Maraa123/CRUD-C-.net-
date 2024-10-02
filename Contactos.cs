using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;


namespace Libreta_Contactos
{
    internal class Contactos
    {
        private int id;
        private string nombre;
        private string correo;
        private string fono;
        private string tipo;

        SqlConnection cn = new SqlConnection("Data Source=MARA-LLORET\\MSSQLSERVER02;Initial Catalog=BD_Contactos;Integrated Security=True");

        public Contactos(int id, string nombre, string correo, string fono, string tipo)
        {
            this.id = id;
            this.nombre = nombre;
            this.correo = correo;
            this.fono = fono;
            this.tipo = tipo;
        }

        public Contactos() { }


        public Contactos(int id) {
            this.id = id;
        
        }

        public int AgregarContacto() {
            cn.Open();
            SqlCommand consulta = new SqlCommand("INSERT INTO tb_contactos VALUES (@nombre, @correo, @fono, @tipo)", cn);

            consulta.Parameters.AddWithValue("nombre", nombre);
            consulta.Parameters.AddWithValue("correo", correo);
            consulta.Parameters.AddWithValue("fono", fono);
            consulta.Parameters.AddWithValue("tipo", tipo);

            int filasAfectadas = consulta.ExecuteNonQuery();
            cn.Close();

            return filasAfectadas;
        
        }

        public void CargarContacos(DataGridView dtg) {

            string consulta = "SELECT * FROM tb_contactos";
            cn.Open();

            //ejecutar consulta en un adaptador, se guarda la info
            SqlDataAdapter data = new SqlDataAdapter(consulta, cn);

            //tabla virtual
            DataTable dt = new DataTable();

            //llena la tabla con la info
            data.Fill(dt);

            //fuente de datos sea igual que la tabla virtual
            dtg.DataSource = dt;


        
        
        }

        public int EliminarContacto() {

            cn.Open();
            SqlCommand consulta = new SqlCommand("DELETE FROM tb_contactos WHERE id = @codigo", cn);
            consulta.Parameters.AddWithValue("codigo", id);
            int filasAfectadas = consulta.ExecuteNonQuery();
            cn.Close();


            return filasAfectadas;

        }

        public int EditarContacto() { 
        
        cn.Open();
            SqlCommand consulta = new SqlCommand("UPDATE tb_Contactos SET nombre = @nombreContacto, correo = @email, fono = @tele, tipo = @tipoCliente WHERE id = @cod", cn);
            consulta.Parameters.AddWithValue("cod", id);
            consulta.Parameters.AddWithValue("nombreContacto", nombre);
            consulta.Parameters.AddWithValue("email", correo);
            consulta.Parameters.AddWithValue("tele", fono);
            consulta.Parameters.AddWithValue("tipoCliente", tipo);

            int filasAfectadas = consulta.ExecuteNonQuery();
            cn.Close();
            return filasAfectadas;

        }


    }
}
