using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NetCoreTest.Models.DataModel;

namespace NetCoreTest.Models.Reposity
{
    public class BlogReposity : Repository<Blog>, IBlogReposity
    {
        private BlogDbContext _db;

        public BlogReposity(BlogDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Blog> Query(Expression<Func<Blog, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var drafts = _db.Blogs
                .Where(predicate)
                .OrderByDescending(p => p.CreationTime).ToList();
            var items = drafts.ToList();
            pager.Configure(items.Count);
            return items.Skip(skip).Take(pager.ItemsPerPage).ToList();
        }
    }
}
