using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BMHEcommerce.Admin.System.Roles
{
    public class RoleListFilterDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
