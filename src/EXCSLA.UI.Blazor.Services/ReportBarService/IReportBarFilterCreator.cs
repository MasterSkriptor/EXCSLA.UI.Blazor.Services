using System.Collections.Generic;

namespace EXCSLA.UI.Blazor.Services
{
    public interface IReportBarFilterCreator
    {
        Dictionary<string, int> CreateFilters<TItem>(List<TItem> itemsToFilter, List<string> filters, string field);
    }
}