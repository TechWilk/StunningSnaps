var map;
function initMap() {

    // hide map img
    var hideMapImg = document.getElementById('hide-map');
    hideMapImg.style.display = 'none';

    // replace with Google Maps from JS API
    var location = { lat: 53.1932506, lng: -2.8819181 };

    map = new google.maps.Map(document.getElementById('map'), {
        center: location,
        zoom: 15
    });

    var marker = new google.maps.Marker({
        map: map,
        position: location,
        title: 'StunningSnaps Studios',
        animation: google.maps.Animation.DROP,
    });
}