using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EfGetStart2.Services
{
    public class PagedList<T>: List<T>
    {
        public int TotalPages{get;private set;}
        public bool HasNextPage{get; private set;}
        public bool HasPrevPage{get; private set;}
        public int PageIndex{get; private set;}

        public PagedList()
        {           
        }

        public async Task<IQueryable<T>> Paging(IQueryable<T> collection, int pageIndex, int pageSize)
        {
            int count = await collection.CountAsync();
            TotalPages = (int)Math.Ceiling(count/(double)pageSize);
            if (pageIndex <= 0) { pageIndex = 1;}
            if (pageIndex > TotalPages) { pageIndex = TotalPages; }
            PageIndex = pageIndex;            
            HasNextPage =  (pageIndex < TotalPages) ? true : false;
            HasPrevPage =  (pageIndex > 1) ? true : false;
            
            collection =  collection.Skip(( pageIndex - 1 ) * pageSize).Take(pageSize);
            return collection;            
        }

    }
}