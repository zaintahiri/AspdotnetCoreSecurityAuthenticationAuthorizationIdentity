using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspdotnetCoreSecurityAuthenticationAuthorizationIdentity.Pages
{
    [Authorize(Policy ="HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        
        public void OnGet()
        {
        }
    }
}
