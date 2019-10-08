using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckVentas.Clases
{
    class MyService
    {
        public void Start()
        {
            // Conectarme a la base de 
            Console.WriteLine("En el Start");
            String datos = "";
            try
            {
                MySqlConnection conexion = new MySqlConnection("Server=localhost;Database=choppin_db;Uid=root;Pwd=;");
                conexion.Open();
                Console.WriteLine("Conectadooo");
                MySqlDataReader reader = null;
                MySqlCommand cmd = new MySqlCommand("select * from productoventa", conexion);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    datos += reader.GetString("IdVenta") + "\n";
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("MySqlException: " + e);
            }

            Console.WriteLine(datos);

        }
        public void Stop()
        {

        }
    }
}
