import type { EntityDto, PagedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateRoleDto {
  name?: string;
  description?: string;
}

export interface RoleDto extends EntityDto<string> {
  name?: string;
  description?: string;
}

export interface RoleListFilterDto extends PagedResultRequestDto {
  keyword?: string;
}
