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
                        // Sensor not active

                        drawWhiteOverSensor(i);
                    } else {
                        // delete the white space over the sensor essentially making it in the room
                        removeItemFromGUI(i, "sensor",removeWhiteOnSensor=true)
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
                activateSesor(room)
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
                activateSesor(cnt + 1)
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
                        // sprinkler needs to be activated

                        activateSprinkler(i);
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
                        // Sprinkler needs to be activated

                        activateAlarm(i);
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
                        // directions needs to be activated
                        activateDoors(i);
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



///////////////////////////////
/*
Drawing Functions
 
*/
///////////////////////////////

function activateSesor(room) {

    //This is used to deactivate a single sensor
    var checkMarker = document.getElementById("sensor-Marker" + room);

    // there needs to be no marker made for that sensor
    if (checkMarker === null) {
        var sensor = document.getElementById("sensor-" + room);

        if (sensor !== null) {
            //Get the sensor location
            var sensorLocation = sensor.coords.split(",")
            var x = sensorLocation[0];
            var y = sensorLocation[1];
            var radius = sensorLocation[2];

            // create marker for the flashing icon
            var marker = document.createElement("div");
            marker.id = "sensor-Marker" + room;

            if (room != "tutorial") {
                marker.className = "sensor active";
            } else {
                marker.className = "sensorLarge active";
            }

            // Set the top location of the marker
            marker.style.top = (y - radius) + "px";
            // Set the left position of the marker
            marker.style.left = (x - radius) + "px";
            // Set the marker width and height
            marker.style.height = (radius * 2) + "px";
            marker.style.width = marker.style.height;

            // set up class properties 

            sensor.marker = marker

            if (!sensor.marker.parentNode) {
                // push the sensor to the room container
                var addition = ""
                if (room == "tutorial") {
                    addition = "2"
                }
                document.getElementById("containerForRoom" + addition).appendChild(sensor.marker);
            }
        }
    }
}

function drawWhiteOverSensor(room) {

    //This is used to deactivate a single sensor
    var checkMarker = document.getElementById("sensor-Marker-white" + room);
    // there needs to be no marker made for that sensor
    if (checkMarker === null) {
        var sensor = document.getElementById("sensor-" + room);
        if (sensor !== null) {
            //Get the sensor location
            var sensorLocation = sensor.coords.split(",")
            var x = sensorLocation[0];
            var y = sensorLocation[1];
            var radius = sensorLocation[2];

            // create marker for the flashing icon
            var marker = document.createElement("div");
            marker.id = "sensor-Marker-white" + room;
            marker.className = "whiteBackground";
            // Set the top location of the marker
            marker.style.top = (y - radius) + "px";
            // Set the left position of the marker
            marker.style.left = (x - radius) + "px";
            // Set the marker width and height
            marker.style.height = (radius * 2) + "px";
            marker.style.width = marker.style.height;

            // set up class properties
            sensor.marker = marker
            if (!sensor.marker.parentNode) {
                // push the sensor to the room container
                var addition = ""
                if (room == "tutorial") {
                    addition = "2"
                }
                document.getElementById("containerForRoom" + addition).appendChild(sensor.marker);
            }
        }
    }
}
function activateAlarm(room) {
    //This is used to deactivate a single alarm
    var checkMarker = document.getElementById("alarm-Marker" + room);

    // there needs to be no marker made for that alarm
    if (checkMarker === null) {
        var alarm = document.getElementById("alarm-" + room);
        if (alarm !== null) {
            //Get the alarm location

            var alarmLocation = alarm.coords.split(",")
            var x = alarmLocation[0];
            var y = alarmLocation[1];
            var height = alarmLocation[2];
            var width = alarmLocation[3];

            // create marker for the flashing icon
            var marker = document.createElement("div");
            marker.id = "alarm-Marker" + room;
            if (room != "tutorial") {
                marker.className = "alarm active";
            } else {
                marker.className = "alarmLarge active";
            }

            // Set the top location of the marker
            marker.style.top = (y) + "px";
            // Set the left position of the marker
            marker.style.left = (x) + "px";
            // Set the marker width and height
            marker.style.height = (height) + "px";
            marker.style.width = (width) + "px";

            // set up class properties
            alarm.marker = alarm.marker || marker
            if (!alarm.marker.parentNode) {
                // push the sensor to the room container
                var addition = ""
                if (room == "tutorial") {
                    addition = "2"
                }
                document.getElementById("containerForRoom" + addition).appendChild(alarm.marker);
            }
        }
    }
}

function activateSprinkler(room) {
    //This is used to deactivate a single alarm
    var checkMarker = document.getElementById("sprinkler-Marker" + room);

    // there needs to be no marker made for that alarm
    if (checkMarker === null) {
        var sprinkler = document.getElementById("sprinkler-" + room);
        if (sprinkler !== null) {
            //Get the alarm location

            var sprinklerLocation = sprinkler.coords.split(",")
            var x = sprinklerLocation[0];
            var y = sprinklerLocation[1];
            var height = sprinklerLocation[2];
            var width = sprinklerLocation[3];

            // create marker for the flashing icon
            var marker = document.createElement("div");
            marker.id = "sprinkler-Marker" + room;
            if (room != "tutorial") {
                marker.className = "sprinkler active";
            } else {
                marker.className = "sprinklerLarge active";
            }

            // Set the top location of the marker
            marker.style.top = (y) + "px";
            // Set the left position of the marker
            marker.style.left = (x) + "px";
            // Set the marker width and height
            marker.style.height = (height) + "px";
            marker.style.width = (width) + "px";

            // set up class properties
            sprinkler.marker = sprinkler.marker || marker
            if (!sprinkler.marker.parentNode) {
                // push the sensor to the room container
                var addition = ""
                if (room == "tutorial") {
                    addition = "2"
                }
                document.getElementById("containerForRoom" + addition).appendChild(sprinkler.marker);
            }
        }
    }
}


function addItemToGUI(room,type) {
    // This is used to add a element to the building layout
    var checkMarker = document.getElementById(type+"-Marker" + room);
    // there needs to be no marker made the specific element to be added
    if (checkMarker === null) {
        // get the room element
        var roomElement = document.getElementById(type+"-" + room);
        if (roomElement !== null) {
            // Now we need to find the element location by splitting its coords
            // These coords have x,y,height, and width.
            // in the case of a circle there is no height and width just radious
            var elementLocation = roomElement.coords.split(",")
            var x = elementLocation[0];
            var y = elementLocation[1];
            var height = elementLocation[2];
            var width = elementLocation[3];

            // create marker for the flashing icon
            var marker = document.createElement("div");
            marker.id = type+"-Marker" + room;
            if (room != "tutorial") {
                // normal building view class
                marker.className = type+" active";
            } else {
                // coming from tutorial so it needs to be large
                marker.className = type+"Large active";
            }

            // Set the top location of the marker
            marker.style.top = (y) + "px";
            // Set the left position of the marker
            marker.style.left = (x) + "px";
            // Set the marker width and height
            marker.style.height = (height) + "px";
            marker.style.width = (width) + "px";

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


function activateDoors(room, doorNumber = 0, direction = 0) {
    var iterations = 1;
    if (room == 9 || room == 14 || room == 15 || room == 21 || room == 22) {
        iterations = 2
    }
    for (j = 1; j <= iterations; j++) {

        var checkMarker = document.getElementById("door-Marker" + room + "-" + j);

        if (checkMarker === null) {
            var door = document.getElementById("door-" + room + "-" + j);

            if (door !== null) {
                //Get the alarm location

                var doorLocation = door.coords.split(",")
                var x = doorLocation[0];
                var y = doorLocation[1];
                var height = doorLocation[2];
                var width = doorLocation[3];

                // create marker for the flashing icon
                var marker = document.createElement("div");
                marker.id = "door-Marker" + room + "-" + j;

                var leftOrUp = door.dataset.functionparameters.split(",")[2]

                if (leftOrUp == "'left'") {
                    marker.className = "doorLeft active";

                    if (room == "tutorial") {

                        marker.className = "doorLeftLarge active";
                    }

                } else {
                    marker.className = "doorUp active";
                }

                // Set the top location of the marker
                marker.style.top = (y) + "px";
                // Set the left position of the marker
                marker.style.left = (x) + "px";
                // Set the marker width and height
                marker.style.height = (height) + "px";
                marker.style.width = (width) + "px";
                // set up class properties
                door.marker = door.marker || marker
                if (!door.marker.parentNode) {
                    // push the sensor to the room container
                    var addition = ""
                    if (room == "tutorial") {
                        addition = "2"
                    }

                    document.getElementById("containerForRoom" + addition).appendChild(door.marker);
                }
            }
        }

    }

}

function removeItemFromGUI(room,type,removeWhiteOnSensor=false, deactivateDoors=false) {
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