using System;
using System.Collections.Generic;
using System.Text;

namespace BMHEcommerce.Admin.System.User
{
    public class SetPasswordDto
    {
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
