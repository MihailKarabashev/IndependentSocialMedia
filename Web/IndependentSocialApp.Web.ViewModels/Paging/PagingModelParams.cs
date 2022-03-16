namespace IndependentSocialApp.Web.ViewModels.Paging
{
    public abstract class PagingModelParams
    {
        const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;

        public int PageSize
        {
            get
            {
                return this.pageSize;
            }

            set
            {
                this.pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

    }
}
