using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Resouces;
using Model;
using Model.Resouces;

namespace Application.Resouces
{
    public class ResouceService<T> : ResouceApplicationBaseService, IResouceService<T> where T : ResouceBase
    {
        //IRepository<T> _repository;
        IResouceRepository<T> _repository;
        IRepository<ResouceTag> _resouceTagRepository;


        public ResouceService(IResouceRepository<T> repository, IRepository<ResouceTag> resouceTagRepository)
        {
            _repository = repository;
            _resouceTagRepository = resouceTagRepository;

        }

        public T GetResouce(string id)
        {
            return _repository.GetInclude<ResouceCategory>(a=>a.Id==id, "Category");
        }

        public SearchResouceOutput<T> SearchResouce(SearchResouceInput input)
        {
            SearchResouceOutput<T> output = new SearchResouceOutput<T>();
            var pager = new Pager(input.PageIndex, input.PageSize);
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var items = _repository.All();
            var total = 0;
            var query = _repository.FindBy(a => a.OrgId == input.OrgId);

            if (!string.IsNullOrEmpty(input.Keywords))
            {
                query = query.Where(a => a.Title.Contains(input.Keywords));
            }
            if (!string.IsNullOrEmpty(input.CategoryId))
            {
                query = query.Where(a=>a.CategoryId==input.CategoryId);
            }
            if (input.Status != Model.ResouceAuditStatus.All)
            {
                query = query.Where(a => a.Status == input.Status);
            }
            items = _repository.FilterByQuery(out total, query, a => a.OrderByDescending(x => x.CreationTime), "", pager.CurrentPage, pager.ItemsPerPage);

            output.Resouces = items.ToList();
            output.Total = total;
            return output;
        }

        public T AddResouce(T resouce)
        {
            _repository.Add(resouce);
            return resouce;
        }

        public T EditResouce(T resouce)
        {
            if (resouce != null)
            {
                _repository.Update(resouce);
                return resouce;
            }
            return null;
        }
        public void DeleteResouce(string resouceId)
        {
            var resouce = _repository.Single(a => a.Id == resouceId);
            _repository.Delete(resouce);
        }
        public IList<ResouceTag> QueryResouceTag(string orgId)
        {
            return _resouceTagRepository.Query(a => a.OrgId == orgId).ToList();
        }

        public void SaveResouceTag(IList<ResouceTag> tags)
        {
            if (tags == null)
            {
                return;
            }
            var allList = _resouceTagRepository.Query(a => a.OrgId == tags.FirstOrDefault().OrgId).Select(a => a.Name).ToList();
            var leftList = tags.AsQueryable().Where(a => !allList.Contains(a.Name));
            if (leftList.Any())
            {
                _resouceTagRepository.AddRange(leftList);
            }
        }

        public IList<ResouceView> QueryResouceView(int resouceType, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                return _repository.QueryResouceView(a => a.Title.Contains(title) && a.ResouceType == resouceType).ToList();
            }
            else
            {
                return _repository.QueryResouceView(a => a.ResouceType == resouceType).ToList();
            }
        }

    }
}
