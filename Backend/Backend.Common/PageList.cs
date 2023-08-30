using System.Collections.Generic;

namespace Backend.Common
{
    public class PageList<T> where T : class
    {
        public List<T> Items { get; }
        public int TotalCount { get; }

        public PageList(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
