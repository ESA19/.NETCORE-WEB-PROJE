using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Deneme.Models;
using Microsoft.AspNetCore.Identity;

namespace Deneme.Areas.Identity.Data;


public class ApplicationUser : IdentityUser
{
  

    [PersonalData]
    [Column(TypeName ="nvarchar(100)")]
    [Display(Name ="Hastanın Adı Soyadı")]
    public string FirstAndLastName { get;set; }

    public List<Appointment> Appointments { get; set; } = new List<Appointment>();




}

