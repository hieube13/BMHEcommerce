﻿using BMHEcommerce.Admin.Catalog.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BMHEcommerce.Admin.System.User
{
   
    public interface IUsersAppService : ICrudAppService<
    UserDto,
    Guid,
    PagedResultRequestDto,
    CreateUserDto,
    UpdateUserDto>
    {
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<PagedResultDto<UserInListDto>> GetListWithFilterAsync(BaseListFilterDto input);

        Task<List<UserInListDto>> GetListAllAsync(string filterKeyword);
        Task AssignRolesAsync(Guid userId, string[] roleNames);

        Task SetPasswordAsync(Guid userId, SetPasswordDto input);

    }

}
