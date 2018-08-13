using System.Collections.Generic;
using Model.Resouces;

namespace Application.Resouces
{
    public interface IResouceService<T> where T : ResouceBase
    {
        T AddResouce(T resouce);
        void DeleteResouce(string resouceId);
        T EditResouce(T resouce);
        T GetResouce(string id);
        IList<ResouceTag> QueryResouceTag(string orgId);
        void SaveResouceTag(IList<ResouceTag> tags);
        SearchResouceOutput<T> SearchResouce(SearchResouceInput input);

        IList<ResouceView> QueryResouceView(int resouceType, string title);
    }
}