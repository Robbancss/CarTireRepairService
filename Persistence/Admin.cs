using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistence
{
    public class Admin : IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }
    }
}
