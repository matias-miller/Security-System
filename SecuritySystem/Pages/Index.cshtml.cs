using buildingSystem;
using controlSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace SecuritySystem.Pages;
[Route("[controller]")]
public class Index : Controller
{
    private readonly ILogger<Index> _logger;

    // create a building object regardless
    public static BuildingControlSystem _buldingControl = new BuildingControlSystem();
    public static ControlCenter _controlCenter = new ControlCenter();

    public Index(ILogger<Index> logger)
    {
        _logger = logger;
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
        if (_controlCenter != null)
        {
            /// This should only be used when a building control variable is changed. and it should be insured that the current version building is used
            try
            {
                HttpContext.Session.SetString("ControlCenter", JsonConvert.SerializeObject(_controlCenter));
            }
            catch (JsonException exeption)
            {
                Debug.WriteLine(exeption);
            }

        }

    }

    private IActionResult getControlCenterState()
    {
        // This returnes the current shared building object
        var temp = HttpContext.Session.GetString("ControlCenter");
        Debug.WriteLine(temp);
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
        return Json(true);

    }

    private IActionResult getBuildingState()
    {
        // This returnes the current shared building object
        var temp = HttpContext.Session.GetString("BuildingControl");
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
        return Json(true);
    }

    private void updateBuildingState()
    {
        // This should only be used when a building control variable is changed. and it should be insured that the current version building is used
        HttpContext.Session.SetString("BuildingControl", JsonConvert.SerializeObject(_buldingControl));
    }

    [HttpPost("OnAttemptGetRoomStateOnClick")]
    public IActionResult OnAttemptGetRoomStateOnClick(int roomNumber)
    {
        getBuildingState();
        updateBuildingState();
        return Json(HttpContext.Session.GetString("BuildingControl"));
    }

    [HttpGet("OnAttemptGetBuildingJson")]
    public IActionResult OnAttemptGetBuildingJson()
    {
        getBuildingState();
        return Json(HttpContext.Session.GetString("BuildingControl"));
    }

    [HttpGet("OnAttemptGetSupervisor")]
    public IActionResult OnAttemptGetSupervisor()
    {
        // Success needs to be true or false
        var success = _controlCenter.getOnDutySupervisor();
        return Json(success);
    }


    [HttpGet("OnAttemptGetOnCall1")]
    public IActionResult OnAttemptGetOnCall1(int roomNumber)
    {
        // Success needs to be true or false
        var success = _controlCenter.getOnCallOperator();
        return Json(success);
    }

    [HttpGet("OnAttemptGetOnCall")]
    public IActionResult OnAttemptGetOnCall([FromQuery] int man=0)
    {
        // Success needs to be true or false
        if(man==0) {
            getControlCenterState();
            var success = _controlCenter.getOnCallOperator();
            return Json(success);
        } else {
            getControlCenterState();
            if(man == 1) {
                var test = _controlCenter.isManned = true;
            } else {
                var test = _controlCenter.isManned = false;
            }
            
            updateControlCenterState();
            return Json(true);
        }
    }
}