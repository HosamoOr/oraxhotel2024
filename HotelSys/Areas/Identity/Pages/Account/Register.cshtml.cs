using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.ViewModel;
//using LinqToDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace HotelSys.Areas.Identity.Pages.Account
{
    // [AllowAnonymous]
    [Authorize]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        protected HotelAlkheerDB _db { get; set; }

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            HotelAlkheerDB db
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _db = db;
            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            //[EmailAddress]
            [Display(Name = "اسم الموظف")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
           [DataType(DataType.Text)]
            [Display(Name = "كلمة المرور")]
            public string Password { get; set; }

            // [DataType(DataType.Password)]
            [DataType(DataType.Text)]
            [Display(Name = "تاكيد كلمة المرور")]
            [Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقة.")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "اسم المستخدم")]
            public string UserName { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "رقم التلفون للموظف")]
            public string PhoneNumber { get; set; } 
             
            
            public string FirstName { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = " اللقب")]
            public string LastName { get; set; }

            [Display(Name = " رقم ح/الصندوق")]
            public int IdBoxAccDef { get; set; }

           

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.UserName, Email = Input.Email,PhoneNumber=Input.PhoneNumber,
                
                };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                   
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                       //await _signInManager.SignInAsync(user, isPersistent: false);

                     // var ss=  await _signInManager.PasswordSignInAsync(user, Input.Password, false, false);

                        
                        var mo=_db.AspNetUsers.Where(x=>x.Email == Input.Email && x.UserName == Input.UserName).FirstOrDefault();   

                        if(mo != null)
                        {
                            BoxViewModel newbox = new BoxViewModel
                            {
                                Name= "صندوق "+ Input.UserName,
                                IsPrivate= false


                            };
                            BoxService bs = new BoxService(_db);
                            int idbox= await bs.AddAsync(newbox);

                            await bs.AddBoxToUser(mo.Id, idbox, true);

                        }

                        return RedirectToAction("Index", "Users");
                       // return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
