namespace CloupardTask.Mvc.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int PreviousPageNumber => HasPreviousPage ? PageNumber - 1 : 1;
        public int NextPageNumber => HasNextPage ? PageNumber + 1 : TotalPages;

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
    }
}
