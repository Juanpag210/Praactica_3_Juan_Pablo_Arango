using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PRACTICA_AEAE_3_Juan_Pablo_AG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Para obtener el usuario y la contraseña al ser ingresados
            string usuario = textBox2.Text;
            string password = textBox1.Text;

            // Validar que los campos no esten vacios
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          
            AccesoADatos accesoADatos = new AccesoADatos();

            try
            {
                
                accesoADatos.AbrirConexion();

                // Consulta SQL para validar el usuario y contraseña
                string query = $"SELECT * FROM TBLSEGURIDAD WHERE StrUsuario='{usuario}' AND StrClave='{password}'";
                SqlDataReader reader = accesoADatos.EjecutarConsulta(query);

               
                if (reader.HasRows)
                {
                    MessageBox.Show("Login exitoso. Bienvenido al sistema.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form5 principal = new Form5();
                    principal.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

               
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                accesoADatos.CerrarConexion();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}