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
        int ultVentaInt;

        public void Start()
        {            
            Console.WriteLine("En el Start");
            try
            {
                // Abro Con 0
                MySqlConnection conexion0 = new MySqlConnection("Server=localhost;Database=choppin_db;Uid=root;Pwd=;");
                conexion0.Open();                

                // Sql cmd 0 -> Trae tabla ultventa
                MySqlCommand cmd0 = new MySqlCommand("select * from ultventa order by ultventa desc limit 1;", conexion0);
                MySqlDataReader reader0 = cmd0.ExecuteReader();

                while (reader0.Read()) { 
                    String UltVentaString = reader0.GetString("UltVenta");
                    ultVentaInt = int.Parse(UltVentaString);
                }
                // Cierro con 0
                conexion0.Close();

                // Abro con 1
                MySqlConnection conexion1 = new MySqlConnection("Server=localhost;Database=choppin_db;Uid=root;Pwd=;");
                conexion1.Open();                 

                // Sql cmd 1 -> Trae todas las ventas
                MySqlCommand cmd1 = new MySqlCommand("select * from ventas", conexion1);
                MySqlDataReader reader1 = cmd1.ExecuteReader();

                while (reader1.Read())
                {
                    String IdVentaString = reader1.GetString("IdVenta");

                    int IdVentaInt = int.Parse(IdVentaString);
                    if (ultVentaInt <= IdVentaInt)
                    {
                        // Abro Con 3
                        MySqlConnection conexion3 = new MySqlConnection("Server=localhost;Database=choppin_db;Uid=root;Pwd=;");
                        conexion3.Open();

                        // Sql cmd 3 -> Actualizo ultVenta con el IdVenta
                        MySqlCommand cmd4 = new MySqlCommand("INSERT INTO `ultventa` (`UltVenta`) VALUES ('"+ IdVentaInt +"');", conexion3);
                        MySqlDataReader reader4 = cmd4.ExecuteReader();
                        conexion3.Close();

                        Console.WriteLine("UltVentaInt: " + ultVentaInt);
                        Console.WriteLine("IdVentaInt: " + IdVentaInt);
                        //obtener IdProductoVenta y cantidad del IdVenta
                        String IdProductoVenta = reader1.GetString("IdProductoVenta");
                        String CantidadVenta = reader1.GetString("Cantidad");
                        int CantVenta = int.Parse(CantidadVenta);

                        // Vuelvo a abrir con 0
                        conexion0.Open();

                        // cmd 2 -> trae la receta segun el IdProductoVenta                        
                        MySqlCommand cmd2 = new MySqlCommand("Select * from recetas where IdProductoVenta = " + IdProductoVenta + "", conexion0);
                        MySqlDataReader read = cmd2.ExecuteReader();
                        while (read.Read())
                        {
                            String Cantidad = read.GetString("Cantidad");
                            int Cant = int.Parse(Cantidad);
                            DateTime hoy = DateTime.Now;

                            // Defino Fecha, IdProducto, CantTotal, IdTipoMov, Nota
                            string fecha = hoy.ToString("yyyy/MM/dd");
                            String IdProducto = read.GetString("IdProducto");
                            int CantTotal = Cant * CantVenta;
                            String idTipoMov = "3";
                            String nota = "Insertado desde C#";

                            Console.WriteLine("fecha: " + fecha);
                            Console.WriteLine("IdProducto: " + IdProducto);
                            Console.WriteLine("Cantidad: " + CantTotal);

                            // Abro con 2
                            MySqlConnection conexion2 = new MySqlConnection("Server=localhost;Database=choppin_db;Uid=root;Pwd=;");
                            conexion2.Open();

                            MySqlCommand cmd3 = new MySqlCommand("INSERT INTO `movimientos` (`IdMovimiento`, `IdProducto`, `Fecha`, `Cantidad`, `Nota`, `IdTipoMovimiento`) VALUES (NULL, '" + IdProducto + "', '" + fecha + "', '-" + CantTotal + "', '" + nota + "', '" + idTipoMov + "')", conexion2);
                            MySqlDataReader dr = cmd3.ExecuteReader();
                            conexion2.Close();

                        }
                        conexion0.Close();
                    }
                }
                conexion1.Close();
            }
                // Volver a Comparar en 15'
            
            catch (MySqlException e)
            {
                Console.WriteLine("MySqlException: " + e);
            }
        }
        public void Stop()
        {

        }
    }
}
