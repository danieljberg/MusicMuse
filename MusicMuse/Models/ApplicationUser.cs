﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }
        
        public string RoleString { get; set; }

    }
}
