using buildingSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace SecuritySystem.Pages;

// Note that you need to route controller
[Route("[controller]")]

// The class needs to extend controller
public class CameraViewControler : Controller
{
    private readonly ILogger<CameraViewControler> _logger;
   private static BuildingControlSystem _buldingControl = new BuildingControlSystem();

    public CameraViewControler(ILogger<CameraViewControler> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    // You need to route the functon name  
    [HttpGet("OnGetSpecificRoomState")]
    public IActionResult OnGetSpecificRoomState()
    {
        var data = _buldingControl.getSpecificRoomState(1);
        return Json(data);
    }

    [HttpGet("OnActivateOrDeactivateSensorsAJAX")]
    public IActionResult OnActivateOrDeactivateSensorsAJAX([FromQuery] int roomNumber) {
        // Called from activateOrDeactivateSensorsAJAX in CameraView
        bool sensorOn = _buldingControl.getSpecificRoomState(roomNumber);
        var data = _buldingControl.requestToModifyBuildingState("requestTurnOnOffSensor", roomNumber, !sensorOn);
        return Json(data);
    }

}


