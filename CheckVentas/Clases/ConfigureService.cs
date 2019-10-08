using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace CheckVentas.Clases
{
    class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(Configure =>
            {
                Configure.Service<MyService>(Service =>
                {
                    Service.ConstructUsing(s => new MyService());
                    Service.WhenStarted(s => s.Start());
                    Service.WhenStopped(s => s.Stop());
                });
                Configure.RunAsLocalService();
                Configure.SetServiceName("TopshelfService2");
                Configure.SetDisplayName("Topshelf2");
                Configure.SetDescription("Segundo servicio Topshelf");
            });
        }
    }
}
