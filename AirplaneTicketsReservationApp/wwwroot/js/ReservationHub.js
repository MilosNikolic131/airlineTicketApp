"use strict";

//const { signalR } = require("../lib/@aspnet/signalr/dist/browser/signalr");

//var connection = new SignalR.HubConnectionBuilder().withUrl("/reservationhub")
//    .configureLogging(SignalR.LogLevel.Information).build();

$(document).ready(() => {

    let reservations = [];
    let $resBody = $("resBody");

    function approve(button) {
        let id = $(button).attr("id");

    }

    const client = new signalR.HubConnectionBuilder().withUrl("/reservationhub")
        .configureLogging(SignalR.LogLevel.Information).build();

    client.on("NewReservationReceived", newReservation => {
        addReservation(newReservation);
    })

    function addReservations() {
        $resBody.empty();
        $.each(reservations, (i, r) => addReservation(r));

    }

    function addReservation(res) {
        let $template =
            <tr>
                <td>${res.flightId}</td>
                <td>${res.userId}</td>
                <td>${res.numberOfSeats}</td>
                <td>${res.approved}</td>
            </tr>;
        $resBody.append($template);
    }

    client.start();

})

//connection.on("NewReservationReceived", function (_reservation) {
//    addReservation(newReservation);
//});

//connection.start().catch(function err() {
//    return console.error(err.toString());
//});