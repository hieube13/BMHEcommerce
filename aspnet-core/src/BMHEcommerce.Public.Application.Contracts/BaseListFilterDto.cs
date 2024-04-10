using System;
using System.Collections.Generic;
using System.Text;

namespace BMHEcommerce.Public
{
    public class BaseListFilterDto : PagedResultRequestBase
    {
        public string Keyword { get; set; }
    }
}
