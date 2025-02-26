using System;
using System.Data.SqlClient;

public class AccesoADatos
{
    private SqlConnection conexion;

    public AccesoADatos()
    {
        // Cadena de conexión a la base de datos BD_FACTURAS
        string connectionString = "Server=DESKTOP-ANVO7E8\\SQLEXPRESS;Database=BD_FACTURAS;Integrated Security=True;";
        conexion = new SqlConnection(connectionString);
    }

    public void AbrirConexion()
    {
        if (conexion.State != System.Data.ConnectionState.Open)
        {
            conexion.Open();
        }
    }

    public void CerrarConexion()
    {
        if (conexion.State != System.Data.ConnectionState.Closed)
        {
            conexion.Close();
        }
    }

    public SqlConnection GetConexion()
    {
        return conexion;
    }

    public SqlDataReader EjecutarConsulta(string query)
    {
        SqlCommand comando = new SqlCommand(query, conexion);
        return comando.ExecuteReader();
    }

    public int EjecutarComando(string query)
    {
        SqlCommand comando = new SqlCommand(query, conexion);
        return comando.ExecuteNonQuery();
    }
}