using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuritySystem.Pages;

public class RoomViewModel : PageModel
{
    private readonly ILogger<RoomViewModel> _logger;

    public RoomViewModel(ILogger<RoomViewModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


