﻿/* This is the main layout file .NET used - C# backend code
 *
 */


using buildingSystem;
using controlSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace SecuritySystem.Pages.Shared
{
    [Route("[controller]")]
    public class _Layout : Controller
    {
        public static ControlCenter _controlCenter = new ControlCenter();
        public static BuildingControlSystem _buldingControl = new BuildingControlSystem();
        public IActionResult Index()
        {
            return View();
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
            // This sets the control center into temp data
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
                    // Convert the temp data to the control center object
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
                    //  makes a building control object out of the temp data
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

        [HttpGet("OnLogoutAJAX")]
        public IActionResult OnLogoutAJAX()
        {
            // gets the backend to log the user out
            // Success needs to be true or false
            var success = _controlCenter.attemptEmployeeLogout();
            return Json(success);
        }
    }
}
