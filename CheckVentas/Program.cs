using CheckVentas.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckVentas
{
    class Program
    {
        
        static void Main()
        {
            Timer tmr = new Timer(Tick, "tick...", 5000, 1000);
            Console.ReadLine();
            // Libera todos los recursos utilizados por la instancia actual de System.Threading.Timer.
            tmr.Dispose();
        }
        static void Tick(object data)
        {
            ConfigureService.Configure();
            // Esto se ejecuta en un hilo combinado
            Console.WriteLine(data); // Escribe "tick..."
        }
    }
}
