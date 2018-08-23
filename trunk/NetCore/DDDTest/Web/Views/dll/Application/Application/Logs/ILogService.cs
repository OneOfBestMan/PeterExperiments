using Model.Logs;

namespace Application.Logs
{
    public interface ILogService
    {
        void Record(SystemLog log);

        SearchLogOutput SearchLog(SearchLogInput input);
    }
}