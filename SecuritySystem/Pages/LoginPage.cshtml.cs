using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuritySystem.Pages;

public class LoginPageModel : PageModel
{
    private readonly ILogger<LoginPageModel> _logger;

    public LoginPageModel(ILogger<LoginPageModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


