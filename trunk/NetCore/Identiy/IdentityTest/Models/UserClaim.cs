﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Models
{
    public class UserClaim : IdentityUserClaim<string>
    {
         public string TempPropery { get; set; }

    }
}
