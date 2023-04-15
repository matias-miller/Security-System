using buildingSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using controlSystem;

namespace SecuritySystem.Pages;

// Note that you need to route controller
[Route("[controller]")]

// The class needs to extend controller
public class CameraViewControler : Controller
{
    private readonly ILogger<CameraViewControler> _logger;
    // Set up the building control system regardless
    public static BuildingControlSystem _buldingControl = new BuildingControlSystem();
    public static ControlCenter _controlCenter = new ControlCenter();

    public CameraViewControler(ILogger<CameraViewControler> logger)
    {
        _logger = logger; 
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Fetch or get the building state on load depending if it is already set
        if (!TempData.ContainsKey("BuildingControl"))
        {
            updateBuildingState();
        }
        else
        {
            getBuildingState();
        }
        if (!TempData.ContainsKey("ControlCenter"))
        {
            updateControlCenterState();
        }
        else
        {
            getControlCenterState();
        }

        base.OnActionExecuting(context);
    }


    private void updateControlCenterState()
    {
        /// This should only be used when a building control variable is changed. and it should be insured that the current version building is used
        TempData["ControlCenter"] = JsonConvert.SerializeObject(_controlCenter);
    }

    private void getControlCenterState()
    {
        // This returnes the current shared building object
        _controlCenter = JsonConvert.DeserializeObject<ControlCenter>(TempData["ControlCenter"] as string);
    }

    private void getBuildingState()
    {
        // This returnes the current shared building object
        _buldingControl = JsonConvert.DeserializeObject<BuildingControlSystem>(TempData["BuildingControl"] as string);
    }
    private void updateBuildingState()
    {
        // This should only be used when a building control variable is changed. and it should be insured that the current version building is used
        TempData["BuildingControl"] = JsonConvert.SerializeObject(_buldingControl);
    }
    public void OnGet()
    {
       // This happens when a get ajax call is performed
    }

    // You need to route the functon name  
    [HttpGet("OnGetSpecificRoomState")]
    public IActionResult OnGetSpecificRoomState()
    {
        // This is more of an example function
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


