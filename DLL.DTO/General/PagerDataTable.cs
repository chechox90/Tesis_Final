using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.DTO.General
{
    public class PagerDataTable<T>
    {

        public PagerDataTableOptions PagerDataTableOp { get; private set; }


        public PagerDataTable(IQueryable<T> list, string search, string keySortBy, string orderBy, int currentPage = 1, int pageSize = 10, int maxPages = 10)
        {
            _list = list;
            PagerDataTableOp = new PagerDataTableOptions(TotalItemCount, search, keySortBy, orderBy, currentPage, pageSize, maxPages);
            
        }

        private IQueryable<T> _list;

        public IQueryable<T> Items
        {
            get
            {
                if (_list == null) return null;
                return _list.Skip((PagerDataTableOp.CurrentPage - 1) * PagerDataTableOp.PageSize).Take(PagerDataTableOp.PageSize);
            }
        }

        public int TotalItemCount
        {
            get
            {
                return _list == null ? 0 : _list.Count();
            }
        }



    }
}