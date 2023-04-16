using buildingSystem;
using controlSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics;

namespace SecuritySystem.Pages;
[Route("[controller]")]
public class PromoteUser : Controller
{
    private readonly ILogger<PromoteUser> _logger;
    public static ControlCenter _controlCenter = new ControlCenter();
    public static BuildingControlSystem _buldingControl = new BuildingControlSystem();
    public PromoteUser(ILogger<PromoteUser> logger)
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
        if (_controlCenter != null) {
            /// This should only be used when a building control variable is changed. and it should be insured that the current version building is used
            TempData["ControlCenter"] = JsonConvert.SerializeObject(_controlCenter);
        }

    }

    private void getControlCenterState()
    {
        // This returnes the current shared building object
        var temp = TempData["ControlCenter"] as string;
        // We need to check if the value is null and also put it in a try catch just in case
        if (temp != null) {
            try
            {
                _controlCenter = JsonConvert.DeserializeObject<ControlCenter>(temp);
            }
            catch(JsonException exeption) { 
            
            }
        }
        
    }

    private void getBuildingState()
    {
        // This returnes the current shared building object
        var temp = TempData["BuildingControl"] as string;
        // We need to check if the value is null and also put it in a try catch just in case
        if (temp != null)
        {
            try
            {
                _buldingControl = JsonConvert.DeserializeObject<BuildingControlSystem>(temp);
            }
            catch (JsonException exeption)
            {

            }
        }
    }

    private void updateBuildingState()
    {
        // This should only be used when a building control variable is changed. and it should be insured that the current version building is used
        TempData["BuildingControl"] = JsonConvert.SerializeObject(_buldingControl);
    }


 

    [HttpGet("OnAttemptPromoteUserAJAX")]
    public IActionResult OnAttemptPromoteUserAJAX([FromQuery] string user)
    {
        // Success needs to be true or false
        var success = _controlCenter.attemptToPromoteUser(user);
        return Json(success);
    }

    [HttpGet("OnAttemptAddUsersToDropdownAJAX")]
    public IActionResult OnAttemptAddUsersToDropdownAJAX()
    {
        // Success needs to be true or false
        return Json(_controlCenter.returnAllNonAdmins());
    }
}


