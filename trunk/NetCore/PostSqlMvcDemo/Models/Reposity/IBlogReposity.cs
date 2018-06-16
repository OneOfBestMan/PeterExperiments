using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NetCoreTest.Models.DataModel;

namespace NetCoreTest.Models.Reposity
{
    public interface IBlogReposity : IRepository<Blog>
    {
        IEnumerable<Blog> Query(Expression<Func<Blog, bool>> predicate, Pager pager);
    }
}
