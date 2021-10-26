using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.General
{
    public class PagerDataTableOptions
    {
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }
        public string KeySortBy { get; private set; }
        public string OrderBy { get; private set; }
        public string Search { get; private set; }
        public int TotalItemCount { get; private set; }
        public IEnumerable<string> PageSizeOptions { get; set; }
        public IEnumerable<int> Pages { get; private set; }

        public PagerDataTableOptions(int totalItemCount, string search, string keySortBy, string orderBy, int currentPage, int pageSize, int maxPages)
        {
            TotalItemCount = totalItemCount;
            Search = search;
            KeySortBy = keySortBy;
            OrderBy = orderBy;

            List<string> _pageSizeOptions = new List<string>();
            _pageSizeOptions.Add("10");
            _pageSizeOptions.Add("20");
            _pageSizeOptions.Add("30");
            _pageSizeOptions.Add("40");
            PageSizeOptions = _pageSizeOptions;

            // calculate total pages
            var totalPages = (int)Math.Ceiling((decimal)TotalItemCount / (decimal)pageSize);

            // ensure current page isn't out of range
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            int startPage, endPage;
            if (totalPages <= maxPages)
            {
                // total pages less than max so show all pages
                startPage = 1;
                endPage = totalPages;
            }
            else
            {
                // total pages more than max so calculate start and end pages
                var maxPagesBeforeCurrentPage = (int)Math.Floor((decimal)maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling((decimal)maxPages / (decimal)2) - 1;
                if (currentPage <= maxPagesBeforeCurrentPage)
                {
                    // current page near the start
                    startPage = 1;
                    endPage = maxPages;
                }
                else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
                {
                    // current page near the end
                    startPage = totalPages - maxPages + 1;
                    endPage = totalPages;
                }
                else
                {
                    // current page somewhere in the middle
                    startPage = currentPage - maxPagesBeforeCurrentPage;
                    endPage = currentPage + maxPagesAfterCurrentPage;
                }
            }

            // calculate start and end item indexes
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize - 1, TotalItemCount - 1);

            // create an array of pages that can be looped over
            var pages = Enumerable.Range(startPage, (endPage + 1) - startPage);

            // update object instance with all pager properties required by the view
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            StartIndex = startIndex;
            EndIndex = endIndex;
            Pages = pages;
        }

        
    }
}
