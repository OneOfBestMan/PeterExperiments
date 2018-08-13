using Model.Resouces;

namespace Application.Organizations
{
    public interface IConfigService
    {
        AttachConfig GetByOrgId(string orgId);

        OtherConfig AddOrUpdateOtherConfig(AddOrUpdateOtherConfigInput input);
    }
}