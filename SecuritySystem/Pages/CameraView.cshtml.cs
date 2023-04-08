using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuritySystem.Pages;

public class CameraViewModel : PageModel
{
    private readonly ILogger<CameraViewModel> _logger;

    public CameraViewModel(ILogger<CameraViewModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


