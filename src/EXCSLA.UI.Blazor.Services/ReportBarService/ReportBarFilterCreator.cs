using System;
using System.Collections.Generic;
using System.Linq;

namespace EXCSLA.UI.Blazor.Services
{
    public class ReportBarFilterCreator : IReportBarFilterCreator
    {
        public ReportBarFilterCreator() { }

        public Dictionary<string, int> CreateFilters<TItem>(List<TItem> itemsToFilter, List<string> filters, string field)
        {
            if (itemsToFilter == null) throw new ArgumentNullException(nameof(itemsToFilter));
            if (filters == null) throw new ArgumentNullException(nameof(filters));
            if (string.IsNullOrWhiteSpace(field)) throw new ArgumentNullException(nameof(field));

            var filterList = new Dictionary<string, int>();

            foreach (var filter in filters)
            {
                var key = filter;
                var value = (key == "All" ? itemsToFilter.Count : itemsToFilter.Where(i => i.GetType().GetProperty(field).GetValue(i, null).Equals(filter)).Count());

                filterList.Add(key, value);
            }

            return filterList;
        }

    }
}
