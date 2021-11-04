using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.UI.Blazor.Services
{
    public static class ReportBarFilterCreatorExtensions
    {
        public static void AddReportBarFilterCreator(this IServiceCollection services)
        {
            services.AddSingleton<IReportBarFilterCreator, ReportBarFilterCreator>();
        }
    }
}
