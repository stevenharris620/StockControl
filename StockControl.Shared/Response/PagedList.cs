namespace StockControl.Shared.Response
{
    public class PagedList<T>
    {
        public PagedList(IEnumerable<T> data, int page = 1, int pageSize = 10)
        {
            Page = page;
            PageSize = pageSize;
            PrepareData(data, page, pageSize);
        }

        public PagedList()
        {
            Page = 1;
            PageSize = 12;
        }

        public int TotalPages { get; set; }
        public int Page { get; }
        public int PageSize { get; }
        public int ItemsCount { get; private set; }
        public List<T> Records { get; set; }

        private void PrepareData(IEnumerable<T> data, int page, int pageSize)
        {
            Records ??= new List<T>();
            Records.Clear();

            var enumerable = data as T[] ?? data.ToArray();
            var pageData = enumerable.Skip((page - 1) * pageSize).Take(pageSize);
            Records.AddRange(pageData);

            ItemsCount = enumerable.Count();
            TotalPages = ItemsCount / PageSize;
            if (ItemsCount % PageSize > 0)
                TotalPages++;
        }
    }
}
