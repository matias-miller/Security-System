using buildingSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SecuritySystem.Pages;

// Note that you need to route controller
[Route("[controller]")]

// The class needs to extend controller
public class CameraViewControler : Controller
{
    private readonly ILogger<CameraViewControler> _logger;
    public static BuildingControlSystem _buldingControl = new BuildingControlSystem();

    public CameraViewControler(ILogger<CameraViewControler> logger)
    {
        _logger = logger;
        
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // happens on page load
        if (TempData.ContainsKey("BuildingControl"))
        {
            getBuildingState();
        }
        else {
            updateBuildingState();
        }
            
        base.OnActionExecuting(context);
    }

    private void updateBuildingState() {
        //Function for getting the current state of the building
        TempData["BuildingControl"] = JsonConvert.SerializeObject(_buldingControl);
    }

    private void getBuildingState()
    {
        // need to update the current version of building control
        _buldingControl = JsonConvert.DeserializeObject<BuildingControlSystem>(TempData["BuildingControl"] as string);
    }

    public void OnGet()
    {
        getBuildingState();
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
        updateBuildingState();
        return Json(data);
    }

}


