function displaySenarios() {
    // This just shows or hids the senarios page
    var senarioBlock = document.getElementById("testSenarios");
    if (senarioBlock.style.display == "none") {
        senarioBlock.style.display = "block";
    } else {
        senarioBlock.style.display = "none";
    }
}

function muteOrUnmute() {
    //This toggles the <p> soundOn to soundOff or visa versa, which will allow adible alarm to play
    var sound = document.getElementById("soundStatus");
    if (sound.innerHTML == "SoundOn") {
        sound.innerHTML = "SoundOff";
    }
    else { sound.innerHTML = "SoundOn"; }
}
function playAlarm() {
    // play sound if system is not muted
    if (document.getElementById("soundStatus").innerHTML == "SoundOn") {
        var alarm = new Audio();
        alarm.src = document.querySelector('source').src;
        alarm.play();
    }
}

function addSensors(single, room2 = 0) {
    if (single == true) {
        var room = document.getElementById("addSensorToRoom").value;
    }
    else {
        room = room2;
    }
    $.ajax({
        url: '/CameraViewControler/OnAttemptAddSensorToRoom',
        // Room array index starts at 0 so we subract 1
        data: { roomNumber: room - 1 },
        dataType: 'json',
        async: false,
        success: function (data) {

            if (single != false) {
                placeSensorsInRoomOnPageLoad(1);
            }

        },
        error: function (errorThrown) {
            console.log("An Error occured in the ajax request for addPersonToRoomAJAX:", errorThrown);
        }
    });

}

function removeSensors(single, room2 = 0) {
    if (single == true) {
        var room = document.getElementById("removeSensorInRoom").value;
    }
    else {
        room = room2;
    }


    $.ajax({
        url: '/CameraViewControler/OnAttemptRemoveSensorFromRoom',
        // Room array index starts at 0 so we subract 1
        data: { roomNumber: room - 1 },
        dataType: 'json',
        async: false,
        success: function (data) {

            if (single != false) {
                placeSensorsInRoomOnPageLoad(1);
            }

        },
        error: function (errorThrown) {
            console.log("An Error occured in the ajax request for addPersonToRoomAJAX:", errorThrown);
        }
    });
}

async function addAllSensors() {
    for (i = 1; i <= 41; i++) {
        await addSensors(false, i);

    }
    placeSensorsInRoomOnPageLoad(1);

}

async function removeAllSensors() {
    //await DeactivateAllSensors();
    for (i = 1; i <= 41; i++) {
        await removeSensors(false, i);

    }
    placeSensorsInRoomOnPageLoad(1);
    alarmReportedProcedureAJAX();


}

function placeSensorsInRoomOnPageLoad(type) {


    $.ajax({
        url: '/CameraViewControler/onPlaceSensorsInRoomOnPageLoad',
        dataType: 'json',
        async: false,
        success: function (data) {

            for (i = 1; i <= 41; i++) {
                if (i != 2 && i != 38) {
                    if (!data.includes(i)) {
                        // sensor is not active so we will draw a empty white circle over it
                        addItemToGUI(i, "sensor",true,true)
                    } else {
                        // delete the white space over the sensor essentially making it in the room
                        removeItemFromGUI(i, "sensor",true)
                    }
                }

            }
        },
        error: function (errorThrown) {
            console.log("An Error occured in the ajax request for addPersonToRoomAJAX:", errorThrown);
        }
    });

}
    //document.addEventListener("DOMContentLoaded", placeSensorsInRoomOnPageLoad);


///////////////////////////////
/*
Ajax Functions
 
*/
///////////////////////////////
function ExampleFunctionForAJAX() {
    // The purpouse of this function is show how to use AJAX to call the Controller for this page
    var URL = '/CameraViewControler/OnGetSpecificRoomState';
    $.ajax({
        url: URL,
        dataType: 'json',
        success: function (data) {

        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('Error:', textStatus, errorThrown);
        }
    });
}

var cnt3 = 0;
async function DeactivateRandomSensorsCallForActivateRandomSensors() {


    if (cnt3 == 41) {
        cnt3 = 0;
        return;
    }
    await DeactivateSensorAjax(false, cnt3);
    cnt3 = cnt3 + 1;
    await DeactivateRandomSensorsCallForActivateRandomSensors();

}

var cnt2 = 0;
async function ActivateRandomSensors(Gas = false) {
    // Need to perform a callback loop so it will activate them randomly but sequentially random

    if (cnt2 == 0) {
        await DeactivateRandomSensorsCallForActivateRandomSensors()

    }

    if (cnt2 == 40) {
        cnt2 = 0;

        alarmReportedProcedureAJAX(Gas);
        return;
    }
    // Loop through all rooms with a 1/3 chance of it becomming active
    if (Math.floor(Math.random() * 3) == 2) {
        // perform another callback
        var test = ActivateSensorAjax(false, cnt2 + 1, function () {
            cnt2++;
            ActivateRandomSensors(Gas);
        });

    } else {
        // perform callback
        cnt2++;
        ActivateRandomSensors(Gas);
    }

}

function ActivateSensorAjax(specific = true, room = 0, returnFunction = false, fromPageLoad = false) {
    // called to activate a specific sensor based on the room it is in
    if (specific == true) {
        var room = document.getElementById("ActivateSensor").value;
    }
    else {
        var room = room
    }

    $.ajax({
        url: '/CameraViewControler/OnActivateSensorAjax',
        // Room array index starts at 0 so we subract 1
        data: { roomNumber: room - 1 },
        dataType: 'json',
        async: true,
        success: function (data) {

            if (data == true) {
                // add the sensor to the building view
                addItemToGUI(room,"sensor",false,true)
            }

            if (returnFunction != false) {
                returnFunction();
                // alarm reported feature will get called in calling function
            } else if (fromPageLoad) {
                // Do nothing
            }
            else {
                // function will only be called once so we should enter alarm reported procedure.
                alarmReportedProcedureAJAX();
            }
        },
        error: function (errorThrown) {
            console.log("An Error occured in the ajax request for ActivateSensorAjax:", errorThrown);
        }
    });
}

function DeactivateSensorAjax(specific = true, room = 0) {
    // called to Deactivate a specific sensor based on the room it is in
    if (specific == true) {

        room = document.getElementById("DeactivateSensor").value;

    } else {
        var room = room
    }

    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/CameraViewControler/OnDeactivateSensorAjax',
            data: { roomNumber: room },
            dataType: 'json',
            success: function (data) {

                // remove the sensor from the building view
                removeItemFromGUI(room, "sensor")

                resolve();
            },
            error: function (errorThrown) {
                console.log("An Error occured in the ajax request for DeactivateSensorAjax:", errorThrown);
                reject();
            }
        });
    });
}
function addPersonToRoomAJAX() {

    var room = document.getElementById("AddPersonToRoom").value;
    $.ajax({
        url: '/CameraViewControler/OnAttemptAddPersonToRoom',
        // Room array index starts at 0 so we subract 1
        data: { roomNumber: room - 1 },
        dataType: 'json',
        async: true,
        success: function (data) {

        },
        error: function (errorThrown) {
            console.log("An Error occured in the ajax request for addPersonToRoomAJAX:", errorThrown);
        }
    });
}

function removePersonToRoomAJAX() {

    var room = document.getElementById("RemovePersonToRoom").value;
    $.ajax({
        url: '/CameraViewControler/OnAttemptRemovePersonToRoom',
        // Room array index starts at 0 so we subract 1
        data: { roomNumber: room - 1 },
        dataType: 'json',
        async: true,
        success: function (data) {

        },
        error: function (errorThrown) {
            console.log("An Error occured in the ajax request for addPersonToRoomAJAX:", errorThrown);
        }
    });
}

async function DeactivateAllSensors(calledFromAnotherFunction = false) {


    for (var i = 0; i < 41; i++) {

        await DeactivateSensorAjax(false, i);
    }
    if (calledFromAnotherFunction == false) {
        //This is because alarmReportedProcedure should only activate once
        alarmReportedProcedureAJAX();
    }
}
// this will determin if the function setBackActiveSensorsOnRefreshAJAX should continue running 
var cnt = 0;

async function setBackActiveSensorsOnRefreshAJAX() {
    // After the page has been reloaded we need to check if the sensors were previously loaded
    if (cnt == 0) {
        //await placeSensorsInRoomOnPageLoad(1)
    }
    if (cnt == 41) {
        cnt = 0;
        await placeSensorsInRoomOnPageLoad(1)
        alarmReportedProcedureAJAX();
        return;
    }
    $.ajax({
        url: '/CameraViewControler/OnGetSpecificSensorStatus',
        data: { roomNumber: cnt },
        dataType: 'json',
        aync: true,
        success: function (data) {
            if (data == true) {
                // Add the sensors back into the room
                addItemToGUI(cnt+1,"sensor",false,true)
                // performing a call back like this will make this run more sequentially.
            }
            cnt++;
            setBackActiveSensorsOnRefreshAJAX();
        },
        error: function (errorThrown) {
            console.log("An Error occured in the ajax request for setBackActiveSensorsOnRefreshAJAX:", errorThrown);
        }
    });

}
document.addEventListener("DOMContentLoaded", setBackActiveSensorsOnRefreshAJAX);




function alarmReportedProcedureAJAX(Gas = false) {
    // On UI first
    // Should be called after sensors have been activated

    /*
    I could send a fetch to the backend, sends nothing, returns 2d array with things that need to be activated 
    {
     "confirmed": true,
     "sprinklers": [6,20,22],
     "alarm": [6,20,22],
     "direction": [6,20,22],
     "doors": [6,22],
     "peopleCalled":["FireDepartment","OnCall"]
    }

    By the time the json is returned all logic should be handled

    // First version will be for unmanned control center
    // Confirm alarm
    // if confirmed by multiple confirmed sensors
    // Call emergency department - call oncall person - display symbol
    // play audible alarm that can be muted activate direction indicators
    // if person is not in the room lock doors and display them as locked
    // activate sprinkler

    */

    $.ajax({
        url: '/CameraViewControler/OnAlarmReportedProcedureAJAX',
        data: { Gas: Gas },
        dataType: 'json',
        async: true,
        success: function (data) {

            if (data[0][0] == true) {
                // Alarm was confirmed play alarm
                // UNCOMMENT during presentation
                //playAlarm()
            }
            if (data[1].length > 0) {
                // activate sprinkler
                for (i = 1; i <= 41; i++) {
                    if (data[1][0].indexOf(i) !== -1) {

                        // Here we will add a sprinkler flashing to the building view
                        addItemToGUI(i, "sprinkler");
                    } else {
                        // remove the sprinkler from the building view
                        removeItemFromGUI(i, "sprinkler")
                    }
                }

            }
            if (data[2].length > 0) {
                // activate alarm
                for (i = 1; i <= 41; i++) {
                    if (data[2][0].indexOf(i) !== -1) {

                        // Add a flashing alarm to the building view 
                        addItemToGUI(i, "alarm")
                    } else {
                        // Remove the alarm from the building view
                        removeItemFromGUI(i, "alarm")
                    }

                }
            }
            if (data[3].length > 0) {
                // activate directions
                for (i = 1; i <= 41; i++) {
                    if (data[3][0].indexOf(i) !== -1) {
                        // this will add a direction indicator flasher to the room
                        addItemToGUI(i, "direction")
                    } else {
                        // remove the direction indicator from the building view
                        removeItemFromGUI(i, "direction")
                    }

                }
            }
            if (data[4].length > 0) {
                // activate doors
                for (i = 1; i <= 41; i++) {
                    if (data[4][0].indexOf(i) !== -1) {
                        // now lets add a flashing door to the building view
                        addItemToGUI(i,"door", false,false,true)
                    } else {
                        // open the door for the room
                        removeItemFromGUI(i, "door",false,true)
                    }
                }
            }
            if (data[5][0].length > 0) {
                // show people to call
                alert("The following have been called: " + data[5][0])

            }
        },
        error: function (errorThrown) {
            console.log("An Error occured in the ajax request for alarmReportedProcedureAJAX:", errorThrown);
        }
    });
}

////////////
///////////
// Drawing Functions
///////////
///////////
function addItemToGUI(room,type, removeSensor=false,sensor=false,door=false) {
    // Drawing function
    // This is used to add a element to the building layout
    // Iterations can change for doors
    var iterations = 1;
    // this is the added bit of text needed to make the door id
    var doorString = "";
    // These are the rooms with multiple doors
    if ((room == 9 || room == 14 || room == 15 || room == 21 || room == 22) && door == true) {
        iterations = 2
    }
    for(j=1;j<=iterations;j++) { 
        
        if(door==true) {
            doorString = "-"+j;
        }

        if(removeSensor == true) {
            // This means we are going to be putting a white section over the sensor, to show that it is no longer in the room
            var checkMarker = document.getElementById("sensor-Marker-white" + room);
            
        } else {
            // just add the normal type
            var checkMarker = document.getElementById(type+"-Marker" + room+doorString);
        }
        
        // there needs to be no marker made the specific element to be added
        if (checkMarker === null) {
            
            // get the room element
            var roomElement = document.getElementById(type+"-" + room+doorString);
            if (roomElement !== null) {
                // Now we need to find the element location by splitting its coords
                // These coords have x,y,height, and width.
                // in the case of a circle there is no height and width just radious
                var elementLocation = roomElement.coords.split(",")
                var x = elementLocation[0];
                var y = elementLocation[1];
                var height = 0;
                var width = 0;
                var radius = 0;

                if(sensor == false) {
                    height = elementLocation[2];
                    width = elementLocation[3];
                } else {
                    radius = elementLocation[2];
                }
    
                // handel marker attributes
                var marker = document.createElement("div");
                
                if(removeSensor== true) {
                    // Add the white marker id
                    marker.id = "sensor-Marker-white" + room;
                    
                } else {
                    // just the normal ID
                    marker.id = type+"-Marker" + room+doorString;
                }
                var direction = "";
                if(door == true) {
                    // get the door orientation
                    var direction = roomElement.dataset.functionparameters.split(",")[2]
                }
                
                if (room != "tutorial") {
                    // normal building view class
                    if(removeSensor == true) {
                        // white for clearing sensor
                        marker.className = "whiteBackground";
                    } else if (door == true) {
                        if(direction == "'left'") {
                            // check door direction left
                            marker.className = "doorLeft active";
                        } else {
                            // otherwise it is an up and down door
                            marker.className = "doorUp active";
                        }
                    }else {
                        // normal classname
                        marker.className = type+" active";
                    }
                } else {
                    // coming from tutorial so it needs to be large
                    if(removeSensor == true) {
                        // this will be sensor-Marker-white-tutorial
                        marker.className = "whiteBackground";
                    } else if(door == true){
                        // set the tutorial door version
                        marker.className = "doorLeftLarge active";
                    } else {
                        marker.className = type+"Large active";
                    }
                }

                // set the elements size and position
                if(sensor == false) {
                    // everything else is rectangular
                    // Set the top location of the marker
                    marker.style.top = (y) + "px";
                    // Set the left position of the marker
                    marker.style.left = (x) + "px";
                    marker.style.height = (height) + "px";
                    marker.style.width = (width) + "px";
                } else {
                    //Sensors are circular
                    marker.style.top = (y - radius) + "px";
                    // Set the left position of the marker
                    marker.style.left = (x - radius) + "px";
                    marker.style.height = (radius * 2) + "px";
                    marker.style.width = marker.style.height;
                }

                // set up class properties
                roomElement.marker =  marker
                // push the sensor to the room container
                var addition = ""
                if (room == "tutorial") {
                    // This means the element should be pushed to "containerForRoom2" instead of "containerForRoom"
                    addition = "2"
                }
                // Finnaly push the marker to over the element
                document.getElementById("containerForRoom" + addition).appendChild(roomElement.marker);
            }
        }
    }
}

function removeItemFromGUI(room,type,removeWhiteOnSensor=false, deactivateDoors=false) {
    // Drawing function
    // This gets one of the elements that were added to the room map and removes it from the dom
    if(removeWhiteOnSensor == true) {
        var marker = document.getElementById(type+"-Marker-white" + room);
    } else {
        var marker = document.getElementById(type+"-Marker" + room);
    }
    if(deactivateDoors == true) {
        // Comming to deactivate a door
        var sensorMarker = document.getElementById("door-Marker" + room + "-" + 1);
        if (sensorMarker !== null) {
            // delete the room 1 if it exists
            sensorMarker.remove();
        }
        if (room == 9 || room == 14 || room == 15 || room == 21 || room == 22) {
            // these are the rooms that have a second door so we need to delete the second door
            var sensorMarker = document.getElementById("door-Marker" + room + "-" + 2);
            if (sensorMarker !== null) {
                sensorMarker.remove();
            }
        }
    } else {
        // not a door just remove it if it exists
        if (marker !== null) {
            marker.remove();
        }
    }
}