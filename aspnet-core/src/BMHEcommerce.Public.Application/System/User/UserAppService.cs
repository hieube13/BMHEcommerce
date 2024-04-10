﻿using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Volo.Abp;
using BMHEcommerce.Public.Catalog.ProductCategories;
using Volo.Abp.Account;
using Microsoft.AspNetCore.Authorization;

namespace BMHEcommerce.Public.System.User
{
    [Authorize(IdentityPermissions.Users.Default, Policy = "AdminOnly")]
    public class UsersAppService : ReadOnlyAppService<IdentityUser, UserDto, Guid, PagedResultRequestDto
                    >, IUsersAppService
    {
        private readonly IdentityUserManager _identityUserManager;

        public UsersAppService(IRepository<IdentityUser, Guid> repository,
            IdentityUserManager identityUserManager) : base(repository)
        {
            _identityUserManager = identityUserManager;
        }

        public async Task<List<UserInListDto>> GetListAllAsync(string filterKeyword)
        {
            var query = await Repository.GetQueryableAsync();
            if (!string.IsNullOrEmpty(filterKeyword))
            {
                query = query.Where(o => o.Name.ToLower().Contains(filterKeyword)
                || o.Email.ToLower().Contains(filterKeyword)
                || o.PhoneNumber.ToLower().Contains(filterKeyword));
            }

            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<IdentityUser>, List<UserInListDto>>(data);
        }

        //public async Task<PagedResultDto<UserInListDto>> GetListWithFilterAsync(BaseListFilterDto input)
        //{
        //    var query = await Repository.GetQueryableAsync();

        //    if (!input.Keyword.IsNullOrWhiteSpace())
        //    {
        //        input.Keyword = input.Keyword.ToLower();
        //        query = query.Where(o => o.Name.ToLower().Contains(input.Keyword)
        //        || o.Email.ToLower().Contains(input.Keyword)
        //        || o.PhoneNumber.ToLower().Contains(input.Keyword));
        //    }
        //    query = query.OrderByDescending(x => x.CreationTime);

        //    var totalCount = await AsyncExecuter.CountAsync(query);

        //    query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
        //    var data = await AsyncExecuter.ToListAsync(query);
        //    var users = ObjectMapper.Map<List<IdentityUser>, List<UserInListDto>>(data);
        //    return new PagedResultDto<UserInListDto>(totalCount, users);
        //}


        public async override Task<UserDto> GetAsync(Guid id)
        {
            var user = await _identityUserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(IdentityUser), id);
            }
            var userDto = ObjectMapper.Map<IdentityUser, UserDto>(user);

            //get roles from users
            var roles = await _identityUserManager.GetRolesAsync(user);
            userDto.Roles = roles;
            return userDto;
        }

        public async Task AssignRolesAsync(Guid userId, string[] roleNames)
        {
            var user = await _identityUserManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(IdentityUser), userId);
            }
            var currentRoles = await _identityUserManager.GetRolesAsync(user);
            var removedResult = await _identityUserManager.RemoveFromRolesAsync(user, currentRoles);
            var addedResult = await _identityUserManager.AddToRolesAsync(user, roleNames);
            if (!addedResult.Succeeded || !removedResult.Succeeded)
            {
                List<Microsoft.AspNetCore.Identity.IdentityError> addedErrorList = addedResult.Errors.ToList();
                List<Microsoft.AspNetCore.Identity.IdentityError> removedErrorList = removedResult.Errors.ToList();
                var errorList = new List<Microsoft.AspNetCore.Identity.IdentityError>();
                errorList.AddRange(addedErrorList);
                errorList.AddRange(removedErrorList);
                string errors = "";

                foreach (var error in errorList)
                {
                    errors = errors + error.Description.ToString();
                }
                throw new UserFriendlyException(errors);
            }
        }

    }
    
}
