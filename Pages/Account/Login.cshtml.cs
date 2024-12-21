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
        public void OnPost()
        {
            if (ModelState.IsValid) return;

            if (Credential.Username == "Admin" && Credential.Password == "Password")
            {
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Name,"Admin"),
                new Claim(ClaimTypes.Email,"admin@gmail.com")
                };

                var identity=new ClaimsIdentity(claims,"MyClaims");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            }
            
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
