// This file handles the start of flow of execution for ajax calls to the backend
using buildingSystem;
using controlSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;

namespace SecuritySystem.Pages;
// Set it to a controller so the ajax can reference it
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
        // This happens on page load

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
        // This sets the control center state to a session variable so it can be referenced else where
        if (_controlCenter != null)
        {
            /// This should only be used when a building control variable is changed. and it should be insured that the current version building is used
            try
            {
                // save the session for control center
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
        // This is how the control center object is fetched from session

        var temp = HttpContext.Session.GetString("ControlCenter");
        if (temp != null)
        {
            try
            {
                // Convert the session control center into a control center object
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
        // This gets the session for the building and stores it in a building object
        var temp = HttpContext.Session.GetString("BuildingControl");
        // We need to check if the value is null and also put it in a try catch just in case
        if (temp != null)
        {
            try
            {
                // convert the session to the _buildingControl object
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

    [HttpGet("OnAttemptPromoteUserAJAX")]
    public IActionResult OnAttemptPromoteUserAJAX([FromQuery] string user)
    {
        // This is called by ajax and is used to attempt to promote the user
        var success = _controlCenter.attemptToPromoteUser(user);
        return Json(success);
    }

    [HttpGet("OnAttemptAddUsersToDropdownAJAX")]
    public IActionResult OnAttemptAddUsersToDropdownAJAX()
    {
        // This is used by ajax and is used to attempt to return all users
        return Json(_controlCenter.returnAllNonAdmins());
    }
}


