using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    //2. Adding a paged list class

    // T could be anything
    public class PagedList<T> : List<T>
    {
        /*
        - Now, this could be all of our users and we don't do any paging or it could be based on our query,
        let's say we just wanted to get all the female users, then it would be how many total female users
        are available.
        - And that's what goes in the total count, And we work out the total pages 
        and which page we're on and how big the pages as well.
        */
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize) 
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } 
        public int TotalCount { get; set; }

        //create static method can call from anywhere
        //IQueryable<T>: this is going to receive our query where we work out the pagination information predicate 
        //source: this's could be the source data and pass into this page 
        /*
        - we're creating a new instance of PagedList class because that's what we're return
        */
        //that's going to execute the 2 list async 
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            //work with count, how many items are left from this query 
            //this is make a database call
            var count = await source.CountAsync();
            // skip over a certain number of records and we need to calculate this as well
            /*
            -   1: 
                if pageNumber = 1
                1-1 = 0 give us 0
                pageSize = 5
                => Skip 0 record
                => take 5
                2: 
                if pageNumber = 2
                2-1 = 1 give us 1
                pageSize = 5
                => Skip 5 record 
                => Take 5
                => will be on the second page of the next 5 record 
            */
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}