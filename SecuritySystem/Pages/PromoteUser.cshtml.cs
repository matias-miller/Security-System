using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuritySystem.Pages;

public class PromoteUserModel : PageModel
{
    private readonly ILogger<PromoteUserModel> _logger;

    public PromoteUserModel(ILogger<PromoteUserModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


