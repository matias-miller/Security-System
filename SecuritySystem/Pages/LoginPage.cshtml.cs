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
                //TempData["ControlCenter"] = JsonConvert.SerializeObject(_controlCenter,
                //    new JsonSerializerSettings() { 
                //        NullValueHandling = NullValueHandling.Ignore
                //    }
                //    );
                HttpContext.Session.SetString("ControlCenter", JsonConvert.SerializeObject(_controlCenter));
            }
            catch (JsonException exeption) {
                Debug.WriteLine(exeption);
            }
            
        }

    }

    private void getControlCenterState()
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
        // This should only be used when a building control variable is changed. and it should be insured that the current version building is used
        HttpContext.Session.SetString("BuildingControl", JsonConvert.SerializeObject(_buldingControl));
    }
    public void OnGet()
    {
    }

    [HttpGet("OnAttemptLoginAJAX")]
    public IActionResult OnAttemptLoginAJAX([FromQuery] string email, string password)
    {
        // Success needs to be true or false
        var success = _controlCenter.validateEmployeeLogin(email, password);
        updateBuildingState();
        updateControlCenterState();
        return Json(success);
    }
 

    [HttpGet("OnAttemptGetPassword")]
    public IActionResult OnAttemptGetPassword()
    {
        // Success needs to be true or false
        var success = _controlCenter.testGetEmployeePassword();
        return Json(success);
    }
}


