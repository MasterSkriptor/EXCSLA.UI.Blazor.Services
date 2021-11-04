using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXCSLA.UI.Blazor.Services
{
    public class ColorPickerOptions
    {
        public Dictionary<string, string> EnumColors { get; set; } = new();
        public string IsActiveColor { get; set; } = "white";

        public ColorPickerOptions() { }
    }
}
