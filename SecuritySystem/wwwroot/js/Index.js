document.addEventListener("DOMContentLoaded", function () { // Initilizes and displays 40 room btns on page load
    const totalRooms = 40;
    const zones = 4;
    const roomsPerZone = Math.ceil(totalRooms / zones);
    let roomStatusesHtml = '';
    for (let z = 1; z <= zones; z++) {
        roomStatusesHtml += `<div class="col-md-3 zone"><h4>Zone ${z}</h4>`;
        for (let i = 1; i <= roomsPerZone; i++) {
            const roomNumber = (z - 1) * roomsPerZone + i;
            if (roomNumber <= totalRooms) {
                roomStatusesHtml += `<a onclick="roomClick(${roomNumber})" class="room-button">Room ${roomNumber}</a>`;
            }
        }
        roomStatusesHtml += '</div>';
    }
    document.getElementById("roomStatuses").innerHTML = roomStatusesHtml;
});

function soundAlarmClick() { // sends notifcation to evacuate site when site evacuation btn is clicked
    alert("Evacuate the site");
}
function policeClick() { // sends notifcation that police where called when police department btn is clicked
    alert("Police Department Alerted");
}
function fireDepartmentClick() { // sends notifcation that police where called when fire department btn is clicked
    alert("Fire Department Alerted");
}
function roomClick(roomNumber) { // Retrieves 

    // Update the modal content with the room number
    document.getElementById("roomDetails").innerHTML = "Details for Room " + roomNumber;
    // Show the modal
    var modal = document.getElementById("roomModal");
    modal.style.display = "block";
    // Set the close button functionality
    var closeButton = document.getElementsByClassName("close")[0];
    closeButton.onclick = function () {
        modal.style.display = "none";
    };
    // Close the modal when clicking outside of it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    };
    AttemptGetRoomStateOnClick(roomNumber);
}

///////////////////////////////
/*
Ajax Functions
*/
///////////////////////////////

function loopJSON(obj, prefix = '') {
    let result = '';

    $.each(obj, function (key, value) {
        var newKey = prefix ? `${prefix}.${key}` : key;
        if (typeof value === 'object' && !Array.isArray(value) && value !== null) {
            loopJSON(value, newKey);
        } else {

            if ((newKey === `isElectricityActive`) ||
                (newKey === `peoplePresent`) ||
                (newKey === `doorArray`)) {

                result += `${newKey}: ${value}<br>`;
            }
        }
    });

    var pElement = $('<p>').attr('id', 'roomDetails').html(result);
    $('.modal-body').html(pElement);
}

function AttemptGetRoomStateOnClick(roomNumber) {
    $.ajax({
        url: '/Index/OnAttemptGetRoomStateOnClick',
        type: 'POST',
        data: { arg1: roomNumber },
        dataType: 'json',
        success: function (data) {
            var json = JSON.parse(data); // Parse the JSON string into a JSON object
            loopJSON(json);
        },
        error: function (xhr, status, error) {
            console.log(error);
        },
        async: true
    });
}

function OnAttemptGetSpecificRoomState() {
    $(document).ready(function () {
        $.ajax({
            url: "/Index/OnAttemptGetSpecificRoomState",
            dataType: "json",
            success: function (data) {
                // process the response

            },
            error: function (errorThrown) {
                console.log("An Error occured in the ajax request for activateOrDeactivateSensorsAJAX:", errorThrown);
            }
        });
    });
}

function AttemptGetSupervisor() {
    $(document).ready(function () {
        $.ajax({
            url: "/Index/OnAttemptGetSupervisor",
            dataType: "json",
            success: function (data) {

                // Update the supervisor <p> element with the response data
                $("#supervisor").append(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("An error occurred while making the request: " + textStatus + " - " + errorThrown);
            }
        });
    });
}
document.addEventListener("DOMContentLoaded", AttemptGetSupervisor);

function AttemptGetOnCall() {
    $(document).ready(function () {
        $.ajax({
            url: "/Index/OnAttemptGetOnCall",
            dataType: "json",
            success: function (data) {

                // Update the on-call operator <p> element with the response data
                $("#onCallOp").append(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("An error occurred while making the request: " + textStatus + " - " + errorThrown);
            }
        });
    });
}
document.addEventListener("DOMContentLoaded", AttemptGetOnCall);

function setNotifications(alarmActive, sensorActive, directionIndicatorActive, sprinklerActive, roomName) {

    if (alarmActive) {
        $("#alarm-notification").append(` ${roomName}`);
    }

    if (sensorActive) {
        $("#sensor-notification").append(` ${roomName}`);
    }

    if (directionIndicatorActive) {

        $("#direction-notification").append(` ${roomName}`);
    }

    if (sprinklerActive) {
        $("#sprinkler-notification").append(` ${roomName}`);
    }

}

function iterateOverDataOnLoad(data) {
    const parsedData = $.parseJSON(data);
    $.each(parsedData.building.roomList, function (index, room) {

        let sensorActive = false;
        let alarmActive = false;
        let sprinklerActive = false;
        let directionIndicatorActive = false;
        $.each(room, function (key, value) {
            if (key === "sprinkler" && (value.isActive)) {
                sprinklerActive = true;
            }

            if (key === "directionIndecator") {
                if (value.isActive == true) {
                    directionIndicatorActive = true;
                }

            }
            if (key === "alarm") { // check if the key is "alarm" and value is a gas/smoke alarm

                if ((value.isActive === true) || (value.isActive === true)) {
                    alarmActive = true;
                }
            }
            if (key === "sensor") { // check if the key is "sensor" and value is a gas/smoke sensor

                if ((value.isActive === true) || (value.isActive === true)) {
                    sensorActive = true;
                }
            }
            if (typeof value === "object") {
                $.each(value, function (k, v) {

                });
            } else {

            }
        });

        if (alarmActive || sensorActive) {
            // Change the color of the room button to red
            $(`a:contains("Room ${room.roomName}")`).css("background-color", "red");
            $(`a:contains("Room ${room.roomName}")`).css("border", "1px solid red");
        } else {
            // Change the color of the room button to green
            $(`a:contains("Room ${room.roomName}")`).css("background-color", "green");
            $(`a:contains("Room ${room.roomName}")`).css("border", "1px solid green");
        }

        setNotifications(alarmActive, sensorActive, directionIndicatorActive, sprinklerActive, room.roomName);
    });
}

function AttemptGetBuildingJson() { 
    // Gets Entire Building State as a JSON file on page load.
    $(document).ready(function () {
        $.ajax({
            url: "/Index/OnAttemptGetBuildingJson",
            dataType: "json",
            success: function (data) {
                iterateOverDataOnLoad(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    });
}
document.addEventListener("DOMContentLoaded", AttemptGetBuildingJson);

function AttemptGetPasswordAJAX() {
    // This is the function that should happen on page submit
    $.ajax({
        url: '/Index/OnAttemptGetPassword',
        dataType: 'json',
        success: function (data) {

            // if data is true return change page to index else remain on current page
            if (data == true) {
                // navigate to the home page on success
                //console.log(document.getElementById("link").getAttribute('href'));
                //window.location.href = document.getElementById("link").getAttribute('href');
            } else {
            }
        },
        error: function (errorThrown) {
            console.log("An Error occured in the ajax request for activateOrDeactivateSensorsAJAX:", errorThrown);
        }
    });
}
document.addEventListener("DOMContentLoaded", AttemptGetPasswordAJAX);