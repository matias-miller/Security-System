using buildingSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace SecuritySystem.Pages;
[Route("[controller]")]
public class Index : Controller
{
    private readonly ILogger<Index> _logger;

    public static BuildingControlSystem _buldingControl = new BuildingControlSystem();
    public Index(ILogger<Index> logger)
    {
        _logger = logger;
        //_buldingControl = new BuildingControlSystem();
        //TempData["BuildingControl"] = JsonConvert.SerializeObject(_buldingControl);
        // updateBuildingState();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // happens on page load
        if (!TempData.ContainsKey("BuildingControl"))
        {
            updateBuildingState();
        }
        else {
            getBuildingState();
        }
        
        base.OnActionExecuting(context);
    }
    private void getBuildingState() {
        // need to update the current version of building control
        _buldingControl = JsonConvert.DeserializeObject<BuildingControlSystem>(TempData["BuildingControl"] as string);
    }
    private void updateBuildingState()
    {
        //Function for getting the current state of the building
        TempData["BuildingControl"] = JsonConvert.SerializeObject(_buldingControl);
    }



    [HttpGet("OnGetSpecificRoomState2")]
    public IActionResult OnGetSpecificRoomState2() {
        //tBuildingState();
        updateBuildingState();
        //return Json(false);
        return Json(_buldingControl.getSpecificRoomState(3));
    }
}

