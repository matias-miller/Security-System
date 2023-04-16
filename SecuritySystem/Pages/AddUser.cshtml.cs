using buildingSystem;
using controlSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace SecuritySystem.Pages;
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
        if (!TempData.ContainsKey("BuildingControl"))
        {
            updateBuildingState();
        }
        else if (TempData["BuildingControl"] != null)
        {
            getBuildingState();
        }
        if (!TempData.ContainsKey("ControlCenter"))
        {
            updateControlCenterState();
        }
        else if(TempData["ControlCenter"] != null)
        {
            _controlCenter = new ControlCenter();
            getControlCenterState();
        }

        base.OnActionExecuting(context);
    }


    private void updateControlCenterState()
    {
        if (_controlCenter != null)
        {
            /// This should only be used when a building control variable is changed. and it should be insured that the current version building is used
            TempData["ControlCenter"] = JsonConvert.SerializeObject(_controlCenter);
        }

    }

    private void getControlCenterState()
    {
        // This returnes the current shared building object
        var temp = TempData["ControlCenter"] as string;
        // We need to check if the value is null and also put it in a try catch just in case
        if (temp != null)
        {
            try
            {
                _controlCenter = JsonConvert.DeserializeObject<ControlCenter>(temp);
            }
            catch (JsonException exeption)
            {

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
    [HttpGet("OnAttemptAddUserAJAX")]
    public IActionResult OnAttemptAddUserAJAX([FromQuery] string first, string last, string email, string password)
    {
        // Success needs to be true or false
        var success = _controlCenter.attemptAddUser(first,last,email,password);
        return Json(success);
    }
}


