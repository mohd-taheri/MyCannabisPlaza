using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Infrastructure.Identity;

namespace Microsoft.eShopWeb.Web.Areas.Identity.Pages.Account
{
    [Authorize]
    public class VerifyPhoneModel : PageModel
    {
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public VerifyPhoneModel(UserManager<ApplicationUser> userManager)
        {
            //_settings = settings.Value;
            _userManager = userManager;
        }

        public string PhoneNumber { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            await LoadPhoneNumber();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await LoadPhoneNumber();

            try
            {
               // var verification = await VerificationResource.CreateAsync(
               //     to: PhoneNumber,
               //     channel: "sms",
              //      pathServiceSid: _settings.VerificationServiceSID
              //  );

                if ( true ) //verification.Status == "pending")
                {
                    return RedirectToPage("ConfirmPhone");
                }

               // ModelState.AddModelError("", $"There was an error sending the verification code: {verification.Status}");
            }
            catch (Exception)
            {
                ModelState.AddModelError("",
                    "There was an error sending the verification code, please check the phone number is correct and try again");
            }

            return Page();
        }

        private async Task LoadPhoneNumber()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new Exception($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            PhoneNumber = user.PhoneNumber;
        }
    }
}
