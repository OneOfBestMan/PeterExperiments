using System.Linq;
using Core;
using Model.Logs;

namespace Application.Logs
{
    /// <summary>
    /// 记录后台日志服务
    /// </summary>
    public class LogService : ApplicationBaseService, ILogService
    {
        IRepository<SystemLog> _repository;
        public LogService(IRepository<SystemLog> repository)
        {
            _repository = repository;
        }

        public void Record(SystemLog log)
        {
            _repository.AddVoid(log);
        }

        public SearchLogOutput SearchLog(SearchLogInput input)
        {
            SearchLogOutput output = new SearchLogOutput();
            var pager = new Pager(input.PageIndex, input.PageSize);
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var items = _repository.All();
            var total = 0;
            if (!string.IsNullOrEmpty(input.Keywords))
            {
                items = _repository.Filter(out total, a => a.UserName.Contains(input.Keywords) || a.Actions.Contains(input.Keywords), a => a.OrderByDescending(x => x.CreationTime), null, pager.CurrentPage, pager.ItemsPerPage);
            }
            else
            {
                items = _repository.Filter(out total, null, a => a.OrderByDescending(x => x.CreationTime), null, pager.CurrentPage, pager.ItemsPerPage);
            }
            output.Logs = items.ToList();
            output.Total = total;
            return output;
        }
    }
}
