using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.UI.Blazor.Services
{
    public static class ColorPickerExtensions
    {
        public static void AddColorPicker(this IServiceCollection services, ColorPickerOptions options)
        {
            services.AddSingleton<IColorPicker, ColorPicker>(o => new ColorPicker(options));
        }
    }
}
