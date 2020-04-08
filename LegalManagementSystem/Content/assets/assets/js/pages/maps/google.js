var map;
function initMap() {
    // Simple Map
    map = new google.maps.Map(document.getElementById('simple_map'), {
      center: {lat: 37.8403407, lng: -101.8046142},
      zoom: 5
    });

    // Map Marker
    var myLatLng = {lat: -25.363, lng: 131.044};

    var mapMarker = new google.maps.Map(document.getElementById('map-markers'), {
      zoom: 4,
      center: myLatLng
    });

    var marker = new google.maps.Marker({
      position: myLatLng,
      map: mapMarker,
      title: 'Hello World!'
    });

    // Panaroma
    var panorama = new google.maps.StreetViewPanorama(
    document.getElementById('street-view'),
    {
      position: {lat: 42.3455, lng: -71.0983},
      pov: {heading: 165, pitch: 0},
      zoom: 1
    });
}