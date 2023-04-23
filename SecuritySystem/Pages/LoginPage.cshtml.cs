// This handles the ajax calls from the loginPage
using buildingSystem;
using controlSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using EmployeeNamespace = controlSystem.Employee;

namespace SecuritySystem.Pages;
// Needs to use controller so get lines up
[Route("[controller]")]
public class Login : Controller
{
    private readonly ILogger<Login> _logger;
    public static ControlCenter _controlCenter = new ControlCenter();
    public static BuildingControlSystem _buldingControl = new BuildingControlSystem();
    
    public Login(ILogger<Login> logger)
    {
        _logger = logger;
    }
    public override void OnActionExecuting(ActionExecutingContext context)

    {
        // This happens on page load, to store the state of the control center and building control center
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
        // Updates the session variable that stores the control center
        if (_controlCenter != null)
        {
            /// This should only be used when a building control variable is changed. and it should be insured that the current version building is used
            try
            {
                HttpContext.Session.SetString("ControlCenter", JsonConvert.SerializeObject(_controlCenter));
            }
            catch (JsonException exeption) {
                Debug.WriteLine(exeption);
            }
        }
    }

    private void getControlCenterState()
    {
        // Gets the control center session variable and converts it into the control center object
        var temp = HttpContext.Session.GetString("ControlCenter");
        if (temp != null)
        {
            try
            {
                // Convert the Session string into an control center object and store it
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
        // Gets the current session string that holds the building control 
        var temp = HttpContext.Session.GetString("BuildingControl");
        // We need to check if the value is null and also put it in a try catch just in case
        if (temp != null)
        {
            try
            {
                // convert the session into an control center object and save it
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
        // This should only be used when a building control variable is changed. and it should be insured that the current version building is used
        HttpContext.Session.SetString("BuildingControl", JsonConvert.SerializeObject(_buldingControl));
    }


    [HttpGet("OnAttemptLoginAJAX")]
    public IActionResult OnAttemptLoginAJAX([FromQuery] string email, string password)
    {
        // trys to login and changes the control center attriutes as a result
        var success = _controlCenter.validateEmployeeLogin(email, password);
        updateBuildingState();
        updateControlCenterState();
        return Json(success);
    }
}


