using buildingSystem;
using controlSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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
    }

    [HttpGet("OnAttemptLoginAJAX")]
    public IActionResult OnAttemptLoginAJAX([FromQuery] string email, string password)
    {
        // Success needs to be true or false
        var success = _controlCenter.validateEmployeeLogin(email, password);
        return Json(success);
    }
}


