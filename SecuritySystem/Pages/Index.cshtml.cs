using buildingSystem;
using controlSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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
        if (!TempData.ContainsKey("BuildingControl"))
        {
            updateBuildingState();
        }
        else {
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

    private void getBuildingState() {
        // This returnes the current shared building object
        _buldingControl = JsonConvert.DeserializeObject<BuildingControlSystem>(TempData["BuildingControl"] as string);
    }
    private void updateBuildingState()
    {
        // This should only be used when a building control variable is changed. and it should be insured that the current version building is used
        TempData["BuildingControl"] = JsonConvert.SerializeObject(_buldingControl);
    }



    [HttpGet("OnGetSpecificRoomState2")]
    public IActionResult OnGetSpecificRoomState2() {
        // This is just a example function to show how to use a specific method from the building control
        //updateBuildingState();
        return Json(_buldingControl.getSpecificRoomState(3));
    }
}

