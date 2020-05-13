using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;

namespace AzureDeveloperTemplates.AdB2C.WebApp.Pages
{
    [Authorize]
    public class PrivateContentModel : PageModel
    {
        public Guid UserId { get; set; }
        public void OnGet()
        {
            UserId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}