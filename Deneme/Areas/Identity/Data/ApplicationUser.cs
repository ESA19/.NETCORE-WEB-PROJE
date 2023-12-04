﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Deneme.Models;
using Microsoft.AspNetCore.Identity;

namespace Deneme.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
  

    [PersonalData]
    [Column(TypeName ="nvarchar(100)")]
    public string FirstAndLastName { get;set; }

    
}

