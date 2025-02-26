using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PRACTICA_AEAE_3_Juan_Pablo_AG
{
    public partial class Form4 : Form
    {
        private AccesoADatos accesoADatos;

        public Form4()
        {
            InitializeComponent();
            accesoADatos = new AccesoADatos();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // para cargar los datos de la tabla TBLPRODUCTO al DataGridView
            CargarProductos();
        }

        private DataTable dtProductos;

        private void CargarProductos()
        {
            try
            {
               
                accesoADatos.AbrirConexion();

                // Consulta SQL para obtener todos los productos
                string query = "SELECT * FROM TBLPRODUCTO";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, accesoADatos.GetConexion());
                dtProductos = new DataTable();
                adaptador.Fill(dtProductos);

                
                dataGridView1.DataSource = dtProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
            CargarProductos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
             
                int idProducto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_PRODUCTO"].Value);

                
                CargarProductos();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado una fila en el DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el ID del producto seleccionado
                int idProducto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_PRODUCTO"].Value);

                // Confirmar la eliminación
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        
                        accesoADatos.AbrirConexion();

                        // Consulta SQL para eliminar el producto
                        string query = $"DELETE FROM TBLPRODUCTO WHERE ID_PRODUCTO={idProducto}";
                        int filasAfectadas = accesoADatos.EjecutarComando(query);

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Recargar los datos en el DataGridView
                            CargarProductos();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        
                        accesoADatos.CerrarConexion();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                // Recorrer las filas del DataTable para detectar cambios
                foreach (DataRow row in dtProductos.Rows)
                {
                    // Verificar si la fila ha sido modificada
                    if (row.RowState == DataRowState.Modified)
                    {
                        // Obtener los valores de la fila
                        int idProducto = Convert.ToInt32(row["IdProducto"]);
                        string nombre = row["StrNombre"].ToString();
                        string codigo = row["StrCodigo"].ToString();
                        string detalle = row["StrDetalle"].ToString();
                        decimal precioc = Convert.ToDecimal(row["NumPrecioCompra"]);
                        decimal preciov = Convert.ToDecimal(row["NumPrecioVenta"]);
                        int stock = Convert.ToInt32(row["NumStock"]);

                        // Consulta SQL para actualizar el producto
                        string query = "UPDATE TBLPRODUCTO SET StrNombre = @StrNombre, StrCodigo = @StrCodigo, NumPrecioCompra = @NumPrecioCompra, NumPrecioVenta = @NumPrecioVenta, NumStock = @NumStock, StrDetalle = @StrDetalle WHERE IdProducto = @IdProducto";
                        SqlCommand comando = new SqlCommand(query, accesoADatos.GetConexion());

                        // Agregar parámetros 
                        comando.Parameters.AddWithValue("@StrNombre", nombre);
                        comando.Parameters.AddWithValue("@NumPrecioCompra", precioc);
                        comando.Parameters.AddWithValue("@NumPrecioVenta", preciov);
                        comando.Parameters.AddWithValue("@NumStock", stock);
                        comando.Parameters.AddWithValue("@IdProducto", idProducto);
                        comando.Parameters.AddWithValue("@StrCodigo", codigo);
                        comando.Parameters.AddWithValue("@StrDetalle", detalle);

                        
                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cambios actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               
                accesoADatos.CerrarConexion();
            }
        }
    }
}