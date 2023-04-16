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
        Debug.WriteLine("zero");
        getBuildingState();
        getBuildingState();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Debug.WriteLine("zero");
        // Fetch or get the building state on load depending if it is already set
        if (!TempData.ContainsKey("BuildingControl"))
        {
            Debug.WriteLine("one");
            updateBuildingState();
        }
        else {
            Debug.WriteLine("two");
            getBuildingState();
        }
        if (!TempData.ContainsKey("ControlCenter"))
        {
            Debug.WriteLine("three");
            updateControlCenterState();
        }
        else
        {
            Debug.WriteLine("four");
            getControlCenterState();
        }

        base.OnActionExecuting(context);
    }

    public static void hello() {
        Debug.WriteLine("zero");
        //getBuildingState();
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
                TempData["ControlCenter"] = JsonConvert.SerializeObject(_controlCenter);
                Debug.WriteLine(TempData["ControlCenter"]);
            }
            catch (JsonException exeption)
            {
                Debug.WriteLine(exeption);
            }

        }

    }

    private void getControlCenterState()
    {
        // This returnes the current shared building object
        var temp = TempData["ControlCenter"] as string;
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
                Debug.WriteLine(exeption);
            }
        }
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

