using System.Collections.Generic;

namespace EXCSLA.UI.Blazor.Services
{
    public class ColorPicker : IColorPicker
    {
        private readonly Dictionary<string, string> _enumColors;

        public ColorPicker()
        {
            _enumColors = new Dictionary<string, string>
            {
                { "All", "secondary" },
                { "New", "success" },
                { "Open", "primary" },
                { "InProgress", "secondary" },
                { "Waiting", "info" },
                { "Finished", "warning" },
                { "Closed", "danger" },
                { "Active", "success" },
                { "Inactive", "secondary" },
                { "Banned", "danger" },
                { "Tank", "warning" },
                { "Tankless", "info" },
                { "Accepted", "info" },
                { "Rejected", "danger" },
                { "Count", "success" },
                { "Needs Service", "danger" }
            };
        }

        public string GetTextCss(string enumValue, bool isActive = false)
        {
            if (isActive) return "text-white";

            return "text-" + _enumColors.GetValueOrDefault(enumValue);
        }

        public string GetBackgroundCss(string enumValue, bool isActive = false)
        {
            if (!isActive) return "bg-white";
            return "bg-" + _enumColors.GetValueOrDefault(enumValue);
        }

        public string GetBorderCss(string enumValue, bool isActive = false)
        {
            if (isActive) return "border-white";
            return "border-" + _enumColors.GetValueOrDefault(enumValue);
        }
    }
}
