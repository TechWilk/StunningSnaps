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
}