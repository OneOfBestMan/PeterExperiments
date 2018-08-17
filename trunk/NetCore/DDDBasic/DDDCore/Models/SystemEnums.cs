using System.ComponentModel.DataAnnotations;

namespace LC.SDK.Core.Models
{
 
    /// <summary>
    /// 用户操作类型
    /// </summary>
    public enum OperationType
    {
        [Display(Name = "添加")]
        Add = 1,
        [Display(Name = "删除")]
        Delete = 2,
        [Display(Name = "编辑")]
        Edit = 3,
        [Display(Name = "查询")]
        Query = 4
    }
 
 
}
