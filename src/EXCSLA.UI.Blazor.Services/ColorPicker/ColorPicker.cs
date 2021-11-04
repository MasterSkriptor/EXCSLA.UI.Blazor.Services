using System.Collections.Generic;

namespace EXCSLA.UI.Blazor.Services
{
    public class ColorPicker : IColorPicker
    {
        private readonly Dictionary<string, string> _enumColors;
        private readonly string _isActiveColor = "white";

        public ColorPicker(ColorPickerOptions options)
        {
            _enumColors = options.EnumColors;
            _isActiveColor = options.IsActiveColor;

            //_enumColors = new Dictionary<string, string>
            //{
            //    { "All", "secondary" },
            //    { "New", "success" },
            //    { "Open", "primary" },
            //    { "InProgress", "secondary" },
            //    { "Waiting", "info" },
            //    { "Finished", "warning" },
            //    { "Closed", "danger" },
            //    { "Active", "success" },
            //    { "Inactive", "secondary" },
            //    { "Banned", "danger" },
            //    { "Tank", "warning" },
            //    { "Tankless", "info" },
            //    { "Accepted", "info" },
            //    { "Rejected", "danger" },
            //    { "Count", "success" },
            //    { "Needs Service", "danger" }
            //};
        }

        public string GetTextCss(string enumValue, bool isActive = false)
        {
            if (isActive) return $"text-{_isActiveColor}";

            return "text-" + _enumColors.GetValueOrDefault(enumValue);
        }

        public string GetBackgroundCss(string enumValue, bool isActive = false)
        {
            if (!isActive) return $"bg-{_isActiveColor}";
            return "bg-" + _enumColors.GetValueOrDefault(enumValue);
        }

        public string GetBorderCss(string enumValue, bool isActive = false)
        {
            if (isActive) return $"border-{_isActiveColor}";
            return "border-" + _enumColors.GetValueOrDefault(enumValue);
        }
    }
}
