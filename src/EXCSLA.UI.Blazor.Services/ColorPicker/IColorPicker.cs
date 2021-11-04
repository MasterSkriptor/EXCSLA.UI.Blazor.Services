namespace EXCSLA.UI.Blazor.Services
{
    public interface IColorPicker
    {
        string GetBackgroundCss(string enumValue, bool isActive = false);
        string GetTextCss(string enumValue, bool isActive = false);
        string GetBorderCss(string enumValue, bool isActive = false);
    }
}