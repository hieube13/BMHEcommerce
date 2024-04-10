using BMHEcommerce.Public.Catalog.ProductCategories;
using BMHEcommerce.Public;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BMHEcommerce.Public.System.User
{
   
    public interface IUsersAppService : IReadOnlyAppService
    <
        UserDto,
        Guid,
        PagedResultRequestDto
    >
    {

        //Task<PagedResultDto<UserInListDto>> GetListWithFilterAsync(BaseListFilterDto input);

        Task<List<UserInListDto>> GetListAllAsync(string filterKeyword);
        Task AssignRolesAsync(Guid userId, string[] roleNames);

    }

}
