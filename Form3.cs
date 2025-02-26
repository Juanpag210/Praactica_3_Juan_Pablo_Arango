using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PRACTICA_AEAE_3_Juan_Pablo_AG
{
    public partial class Form3 : Form
    {
        private AccesoADatos accesoADatos;

        public Form3()
        {
            InitializeComponent();
            accesoADatos = new AccesoADatos();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // para obtener el nuevo usuario y la nueva contraseña ingresados
            string nuevoUsuario = textBox1.Text;
            string nuevaPassword = textBox2.Text;

            
            if (string.IsNullOrEmpty(nuevoUsuario) || string.IsNullOrEmpty(nuevaPassword))
            {
                MessageBox.Show("Por favor, ingrese el nuevo usuario y la nueva contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                
                accesoADatos.AbrirConexion();

                // Consulta SQL para actualizar el usuario y la contraseña
                string query = $"UPDATE TBLSEGURIDAD SET StrUsuario='{nuevoUsuario}', StrClave='{nuevaPassword}' WHERE StrUsuario='{nuevoUsuario}'";
                int filasAfectadas = accesoADatos.EjecutarComando(query);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Usuario y contraseña actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el usuario y la contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el usuario y la contraseña: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                accesoADatos.CerrarConexion();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 principal = new Form5();
            principal.Show();
            this.Hide();
        }
    }
}