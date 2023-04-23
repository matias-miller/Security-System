// This handles the server side of add user
using buildingSystem;
using controlSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics;

namespace SecuritySystem.Pages;
// by using controller we are able to use get requests
[Route("[controller]")]
public class AddUser: Controller
{
    private readonly ILogger<AddUser> _logger;
    public static BuildingControlSystem _buldingControl = new BuildingControlSystem();
    public static ControlCenter _controlCenter = new ControlCenter();

    public AddUser(ILogger<AddUser> logger)
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
        // this sets the Session variable to control center to the control center object
        if (_controlCenter != null)
        {
            /// This should only be used when a building control variable is changed. and it should be insured that the current version building is used
            try
            {
                // set the session variable to control center object
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
        if (temp != null)
        {
            try
            {
                // This sets the control center to the session variable control center
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
                // This sets the buildingControl object to the session building control
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
        // Sets the building control session variable
        // This should only be used when a building control variable is changed. and it should be insured that the current version building is used
        HttpContext.Session.SetString("BuildingControl", JsonConvert.SerializeObject(_buldingControl));
    }

    [HttpGet("OnAttemptAddUserAJAX")]
    public IActionResult OnAttemptAddUserAJAX([FromQuery] string first, string last, string email, string password)
    {
        // Requests the backend add a user
        var success = _controlCenter.attemptAddUser(first,last,email,password);
        return Json(success);
    }
}


