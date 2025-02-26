using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PRACTICA_AEAE_3_Juan_Pablo_AG
{
    public partial class Form2 : Form
    {
        private AccesoADatos accesoADatos;

        public Form2()
        {
            InitializeComponent();
            accesoADatos = new AccesoADatos();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Para cargar los datos de la tabla TBLCLIENTES al DataGridView
            CargarClientes();
        }

        private DataTable dtClientes;

        private void CargarClientes()
        {
            try
            {
               
                accesoADatos.AbrirConexion();

                // Consulta SQL para obtener todos los clientes
                string query = "SELECT * FROM TBLCLIENTES";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, accesoADatos.GetConexion());
                dtClientes = new DataTable();
                adaptador.Fill(dtClientes);

             
                dataGridView1.DataSource = dtClientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               
                accesoADatos.CerrarConexion();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            CargarClientes();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                int idCliente = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_CLIENTE"].Value);

               
                CargarClientes();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               
                int idCliente = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_CLIENTE"].Value);

            
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        //  para abrir la conexión a la base de datos
                        accesoADatos.AbrirConexion();

                        // Consulta SQL para eliminar el cliente
                        string query = $"DELETE FROM TBLCLIENTES WHERE ID_CLIENTE={idCliente}";
                        int filasAfectadas = accesoADatos.EjecutarComando(query);

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // modificar los datos en el DataGridView
                            CargarClientes();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        
                        accesoADatos.CerrarConexion();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 principal = new Form5();
            principal.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                accesoADatos.AbrirConexion();

                // Recorre las filas del DataTable para detectar cambios
                foreach (DataRow row in dtClientes.Rows)
                {
                    // Verificar si la fila ha sido modificada
                    if (row.RowState == DataRowState.Modified)
                    {
                        // Obtener los valores de la fila
                        int idCliente = Convert.ToInt32(row["IdCliente"]);
                        string nombre = row["StrNombre"].ToString();
                        string direccion = row["StrDireccion"].ToString();
                        string telefono = row["StrTelefono"].ToString();
                        string correo = row["StrEmail"].ToString();
                        string documento = row["NumDocumento"].ToString();

                        // Consulta SQL para actualizar el cliente
                        string query = "UPDATE TBLCLIENTES SET StrNombre = @StrNombre, NumDocumento = @NumDocumento, StrEmail = @StrEmail, StrDireccion = @StrDireccion, StrTelefono = @StrTelefono WHERE IdCliente = @IdCliente";
                        SqlCommand comando = new SqlCommand(query, accesoADatos.GetConexion());

                        // Agregar parámetros 
                        comando.Parameters.AddWithValue("@StrNombre", nombre);
                        comando.Parameters.AddWithValue("@StrDireccion", direccion);
                        comando.Parameters.AddWithValue("@StrTelefono", telefono);
                        comando.Parameters.AddWithValue("@IdCliente", idCliente);
                        comando.Parameters.AddWithValue("@StrEmail", correo);
                        comando.Parameters.AddWithValue("@NumDocumento", documento);

                        
                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cambios actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                accesoADatos.CerrarConexion();
            }
        }
    }
}