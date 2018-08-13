using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Organizations;
using Data.Repository;
using Model.Organizations;
using Model.Resouces;
using Newtonsoft.Json;

namespace Application.Organizations
{
    public class ConfigService : ApplicationBaseService, IConfigService
    {
        IOrganizationRepository _organizationRepository;
        IDomainRepository _domainRepository;
        IAdminMenuRepository _adminMenuRepository;
        IRepository<AttachConfig> _attachConfigRepository;
        IRepository<OtherConfig> _otherConfigRepository;

        public ConfigService(IOrganizationRepository organizationRepository,
            IDomainRepository domainRepository,
            IAdminMenuRepository adminMenuRepository,
             IRepository<AttachConfig> attachConfigRepository,
             IRepository<OtherConfig> otherConfigRepository)
        {
            _organizationRepository = organizationRepository;
            _domainRepository = domainRepository;
            _adminMenuRepository = adminMenuRepository;
            _attachConfigRepository = attachConfigRepository;
            _otherConfigRepository = otherConfigRepository;
        }

        public AttachConfig GetByOrgId(string orgId)
        {
            //return _attachConfigRepository.Single(a=>a.OrgId==orgId);
            AttachConfig config = null;
            OtherConfig ac = _otherConfigRepository.Single(a => a.OrgId == orgId && a.ConfigName == "attach");
            if (ac != null)
            {
                if (!string.IsNullOrEmpty(ac.ConfigValue))
                {
                    config = JsonConvert.DeserializeObject<AttachConfig>(ac.ConfigValue);
                }
                else
                {
                    config = new AttachConfig();
                }
            }
            else
            {
                config = new AttachConfig();
                OtherConfig newac = new OtherConfig();
                newac.ConfigName = "attach";
                newac.ConfigValue = JsonConvert.SerializeObject(config);
                newac.LastUpdateTime = DateTime.Now;
                _otherConfigRepository.Add(newac);
            }
            return config;
        }

        public OtherConfig AddOrUpdateOtherConfig(AddOrUpdateOtherConfigInput input)
        {
            OtherConfig oc = _otherConfigRepository.Single(a => a.OrgId == input.OrgId && a.ConfigName == "attach");
            if (oc == null)//如果不存在
            {
                oc = new OtherConfig();
                oc.OrgId = input.OrgId;
                oc.CreationTime = DateTime.Now;
                oc.ConfigName = "attach";
                oc.ConfigValue = input.ConfigValue;
                oc.LastUpdateTime = DateTime.Now;
                _otherConfigRepository.Add(oc);
            }
            else
            {
                oc.ConfigValue = input.ConfigValue;
                oc.LastUpdateTime = DateTime.Now;
                _otherConfigRepository.Update(oc);
            }
            return oc;
        }
    }
}
