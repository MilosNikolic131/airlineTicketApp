"use strickt";

var connection = new SignalR.HubConnectionBuilder().withUrl("/reservationhub")
    .configureLogging(SignalR.LogLevel.Information).build();

connection.on("NewReservationReceived", function (_reservation) {
    document.getElementById("FlightID").value = _reservation;
});

connection.start().catch(function err() {
    return console.error(err.toString());
});