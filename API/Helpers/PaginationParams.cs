namespace API.Helpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;
        //so trang
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        //so luong user
        public int PageSize 
        {
            get => _pageSize;
            // we'll set it to the Max Page Size and if it's not we going to set it to value
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}