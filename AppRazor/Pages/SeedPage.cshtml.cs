using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace AppRazor.Pages
{
    public class SeedModel : PageModel
    {
        readonly IAdminService _adminService;
        readonly ILogger<SeedModel> _logger;

        public SeedModel(IAdminService adminService, ILogger<SeedModel> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            await _adminService.SeedAsync(100);
            return RedirectToPage("/Index");
        }
    }
}