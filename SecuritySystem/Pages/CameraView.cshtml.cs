using buildingSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using controlSystem;
using System;

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
    private static readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();


    public CameraViewControler(ILogger<CameraViewControler> logger)
    {
        _logger = logger;
        Debug.WriteLine("Instantiated");
        
    }

    public override void OnActionExecuting(ActionExecutingContext context)

    {

        // Fetch or get the building state on load depending if it is already set
        if (!HttpContext.Session.Keys.Contains("BuildingControl"))
        {
            updateBuildingState();
        }
        else
        {
            getBuildingState();
        }
        if (!HttpContext.Session.Keys.Contains("ControlCenter"))
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
        Debug.WriteLine("updateControlCenterState");
        if (_controlCenter != null)
        {
            /// This should only be used when a building control variable is changed. and it should be insured that the current version building is used
            try
            {
                //TempData["ControlCenter"] = JsonConvert.SerializeObject(_controlCenter,
                //    new JsonSerializerSettings() { 
                //        NullValueHandling = NullValueHandling.Ignore
                //    }
                //    );
                HttpContext.Session.SetString("ControlCenter", JsonConvert.SerializeObject(_controlCenter));
            }
            catch (JsonException exeption)
            {
                Debug.WriteLine(exeption);
            }

        }

    }

    private void getControlCenterState()
    {
        // This returnes the current shared building object
        var temp = HttpContext.Session.GetString("ControlCenter");
        Debug.WriteLine("getControlCenterState");
        if (temp != null)
        {
            try
            {
                _controlCenter = JsonConvert.DeserializeObject<ControlCenter>(temp);
            }
            catch (JsonException exeption)
            {
                Debug.WriteLine(exeption);
            }
        }

    }

    private void getBuildingState()
    {
        // This returnes the current shared building object
        var temp = HttpContext.Session.GetString("BuildingControl");
        Debug.WriteLine(temp);
        // We need to check if the value is null and also put it in a try catch just in case
        if (temp != null)
        {
            try
            {
                _buldingControl = JsonConvert.DeserializeObject<BuildingControlSystem>(temp);
            }
            catch (JsonException exeption)
            {
                Debug.WriteLine(exeption);
            }
        }
    }

    private void updateBuildingState()
    {
        Debug.WriteLine(JsonConvert.SerializeObject(_buldingControl));
        // This should only be used when a building control variable is changed. and it should be insured that the current version building is used
        HttpContext.Session.SetString("BuildingControl", JsonConvert.SerializeObject(_buldingControl));
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

    [HttpGet("OnActivateSensorAjax")]
    public IActionResult OnActivateSensorAjax([FromQuery] int roomNumber)
    {
        getBuildingState();
        // Turns specific sensor on
        var data = _buldingControl.requestToModifyBuildingState("requestTurnOnOffSensor", roomNumber, true);
        updateBuildingState();
        return Json(data);
    }
    [HttpGet("OnDeactivateSensorAjax")]
    public IActionResult OnDeactivateSensorAjax([FromQuery] int roomNumber)
    {
        // turns sensor off
        var data = _buldingControl.requestToModifyBuildingState("requestTurnOnOffSensor", roomNumber, false);
        updateBuildingState();
        return Json(data);
    }
    [HttpGet("OnGetSpecificSensorStatus")]
    public IActionResult OnGetSpecificSensorStatus([FromQuery] int roomNumber)
    {
        // Just gets a specicic sensors status
        getBuildingState();
        Debug.WriteLine("Room: " + roomNumber);
    
        var data = _buldingControl.getSpecificSensorState(roomNumber);
        return Json(data);
    }

    [HttpGet("OnAttemptGetPassword")]
    public IActionResult OnAttemptGetPassword()
    {
        // Success needs to be true or false
        var success = _controlCenter.testGetEmployeePassword();
        return Json(success);
    }

}


