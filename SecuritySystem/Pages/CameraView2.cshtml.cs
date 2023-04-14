using buildingSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace SecuritySystem.Pages;
[Route("[controller]")]
public class CameraView2Controler : Controller
{
    private readonly ILogger<CameraView2Controler> _logger;
   private static BuildingControlSystem _buldingControl = new BuildingControlSystem();

    public CameraView2Controler(ILogger<CameraView2Controler> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
    [HttpGet("OnGetSpecificRoomState")]
    public IActionResult OnGetSpecificRoomState()
    {
        var data = _buldingControl.getSpecificRoomState(1);
        return Json(data);
    }

    [HttpGet]
    public IActionResult OnSetSpecificRoomState()
    {
        var data = _buldingControl.setSpecificRoomState(1, true);
        return Json(data);
    }
}


