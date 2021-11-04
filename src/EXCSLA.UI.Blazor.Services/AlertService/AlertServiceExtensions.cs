using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXCSLA.UI.Blazor.Services.AlertService
{
    public static class AlertServiceExtensions
    {
        public static void AddAlertService(this IServiceCollection services)
        {
            services.AddSingleton<IAlertService, AlertService>();
        }
    }
}
