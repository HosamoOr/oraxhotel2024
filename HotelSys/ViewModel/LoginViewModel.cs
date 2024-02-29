using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "username")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "password")]
        [Required]
        public string Password { get; set; }
    }
  
    
}
