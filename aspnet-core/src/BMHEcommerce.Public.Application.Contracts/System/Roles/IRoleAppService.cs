using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.PermissionManagement;

namespace BMHEcommerce.Admin.System.Roles
{
    public interface IRoleAppService : ICrudAppService
    <
        RoleDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateRoleDto,
        CreateUpdateRoleDto
    >
    {
        Task<PagedResultDto<RoleDto>> GetListFilterAsync(RoleListFilterDto input);
        Task<List<RoleDto>> GetListAllAsync();
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<GetPermissionListResultDto> GetPermissionsAsync(string providerName, string providerKey);
        Task UpdatePermissionsAsync(string providerName, string providerKey, UpdatePermissionsDto input);
    }
}
