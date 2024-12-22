using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AspdotnetCoreSecurityAuthenticationAuthorizationIdentity.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (Credential.Username == "Admin" && Credential.Password == "Password")
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,"Admin"),
                    new Claim(ClaimTypes.Email,"admin@gmail.com"),
                    //this claim is for Department(HR), REGISTER CLAIM IN PROGRAM FILE SERVICE
                    //holder.Services.AddAuthorization,
                    //with police options.AddPolicy("name of police", policy=>policy.RequireClaim("Department","HR"))
                    new Claim("Department","HR"),
                    new Claim("Admin","true")


                };

                var identity=new ClaimsIdentity(claims,"MyCookieAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", principal);
                return RedirectToPage("/Index");
            }

            return Page();
            
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name ="User Name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
