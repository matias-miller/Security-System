using buildingSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using controlSystem;
using System;
using buildingSystem.roomComponents;

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
        //Debug.WriteLine("Instantiated");
        
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
        //Debug.WriteLine("updateControlCenterState");
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
        //Debug.WriteLine("getControlCenterState");
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
        //Debug.WriteLine(temp);
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
        //Debug.WriteLine(JsonConvert.SerializeObject(_buldingControl));
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
        getBuildingState();
        // turns sensor off
        var data = _buldingControl.requestToModifyBuildingState("requestTurnOnOffSensor", roomNumber, false);
        updateBuildingState();
        Debug.WriteLine(_buldingControl.getNumberOfActiveSensors());
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

    [HttpGet("OnAttemptAddPersonToRoom")]
    public IActionResult OnAttemptAddPersonToRoom([FromQuery] int roomNumber)
    {
        // Success needs to be true or false
        getBuildingState();
        var success = _buldingControl.attemptToAddPersonToRoom(roomNumber); 
        updateBuildingState();
        return Json(success);
    }

    [HttpGet("OnAttemptRemovePersonToRoom")]
    public IActionResult OnAttemptRemovePersonToRoom([FromQuery] int roomNumber)
    {
        // Success needs to be true or false
        getBuildingState();
        var success = _buldingControl.attemptToRemovePersonToRoom(roomNumber);
        updateBuildingState();
        return Json(success);
    }

    [HttpGet("OnAttemptAddSensorToRoom")]
    public IActionResult OnAttemptAddSensorToRoom([FromQuery] int roomNumber)
    {
        // Success needs to be true or false
        getBuildingState();
        var success = _buldingControl.RequestToAddSensorToRoom(roomNumber);
        updateBuildingState();
        return Json(success);
    }

    [HttpGet("onPlaceSensorsInRoomOnPageLoad")]
    public IActionResult onPlaceSensorsInRoomOnPageLoad([FromQuery] int roomNumber)
    {
        // Success needs to be true or false
        getBuildingState();
        var success = _buldingControl.getRoomsWithSensors();
        updateBuildingState();
        return Json(success);
    }



    [HttpGet("OnAlarmReportedProcedureAJAX")]
    public IActionResult OnAlarmReportedProcedureAJAX()
    {
        getBuildingState();
        /* Returned from this should be in this form 
         {
         "confirmed": true,
         "sprinklers": [6,20,22],
         "alarm": [6,20,22],
         "direction": [6,20,22],
         "doors": [6,22],
         "peopleCalled":["FireDepartment","OnCall"]
        }
 
        */
        // First version will be for unmanned control center
        // Confirm alarm
        // if confirmed by multiple confirmed sensors
        // Call emergency department - call oncall person - display symbol
        // play audible alarm that can be muted activate direction indicators
        // if person is not in the room lock doors and display them as locked
        // activate sprinkler
        // Success needs to be true or false
        // var success = _controlCenter.testGetEmployeePassword();
        if (_controlCenter.isManned)
        {
            // Control center is manned process
        }
        else {
            // Control center is not manned process

            //
            /* If the control area is unmanned and an alarm is activated
            this alarm should not be ignored if it is potentially serious.
            Emergency services should be automatically called. */
            // need to call people
            // Get 
            var numberOfActiveSensors = _buldingControl.getNumberOfActiveSensors();
            //Confirmed unconfirmed procedure
            var confirmed = false;
            if (numberOfActiveSensors > 0)
            {
                confirmed = true;
            }
            else {
                confirmed = false;
            }
            object[][] AlarmReportedProcedure = new object[6][];

            // "confirmed": true,
            AlarmReportedProcedure[0] = new object[] { confirmed };

            var sprinklers = _buldingControl.requestToActivateSprinklersAutomated();
            // "sprinklers": [6,20,22],
            AlarmReportedProcedure[1] = new object[] { sprinklers };

            var alarms = _buldingControl.requestToActivateAlarmsAutomated();
            //"alarm": [6,20,22],
            AlarmReportedProcedure[2] = new object[] { alarms };

            var directions = _buldingControl.requestToActivateDirectionsAutomated();
            //"direction": [6,20,22],
            AlarmReportedProcedure[3] = new object[] { directions };

            var doors = _buldingControl.requestToActivateDoorsAutomated();
            // "doors": [6,22],
            AlarmReportedProcedure[4] = new object[] { doors };

            var peopleCalled = _buldingControl.requestToMakeCallsAutomated();
            // "peopleCalled":["FireDepartment","OnCall"]
            AlarmReportedProcedure[5] = new object[] { peopleCalled };

            updateBuildingState();
            return Json(AlarmReportedProcedure);
        }
        return (Json(false));
    }

}


