var map;
var marker;
function initMap() {
    // replace img with Google Maps from JS API
    var location = { lat: 53.1932506, lng: -2.8819181 };

    map = new google.maps.Map(document.getElementById('map'), {
        center: location,
        zoom: 15,
        scaleControl: true
    });

    marker = new google.maps.Marker({
        position: location,
        title: 'StunningSnaps Studios',
        animation: google.maps.Animation.DROP
    });

    marker.setMap(map);

    // ~~~~~~~~~~
    // Following code from (Eugenemail, 2015).
    // ~~~~~~~~~~

    google.maps.event.addDomListener(window, 'resize',
  resize);
}

function resize() {
    map.setCenter(map_center);
    map.fitBounds(path_bounds);
}
// ~~~~~~~~~~
// End of code from (Eugenemail, 2015). 
// ~~~~~~~~~~