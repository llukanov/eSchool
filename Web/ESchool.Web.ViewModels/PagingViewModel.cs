namespace ESchool.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int PageIndex { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.ItemsCount / this.ItemsPerPage);

        public bool HasPreviousPage => this.PageIndex > 1;

        public int PreviousPageIndex => this.PageIndex - 1;

        public bool HasNextPage => this.PageIndex < this.PagesCount;

        public int NextPageIndex => this.PageIndex + 1;

        public int ItemsCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
