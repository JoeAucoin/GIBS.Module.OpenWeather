/* Module Script */
var GIBS = GIBS || {};

GIBS.OpenWeather = {
};

 ////Function to initialize the Leaflet map
GIBS.OpenWeather.initializeMap = function (mapDivId, latitude, longitude, mapZoom, locationName, leafletImagesPath) {
    try {
        var mapElement = document.getElementById(mapDivId);
        if (!mapElement) {
            console.error("Map element not found: " + mapDivId);
            return;
        }
        if (typeof L === 'undefined') {
            console.error("Leaflet (L) is not loaded. Make sure leaflet.js is included before this script.");
            return;
        }

        // Set Leaflet's default icon path
        // This tells Leaflet where to find marker-icon.png, marker-shadow.png etc.
        L.Icon.Default.prototype.options.imagePath = leafletImagesPath;
        // console.log("Leaflet image path set to: " + L.Icon.Default.prototype.options.imagePath);


        var map = L.map(mapDivId).setView([latitude, longitude], mapZoom);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(map);

        if (locationName && locationName.trim() !== '') {
            L.marker([latitude, longitude]).addTo(map)
                .bindPopup(locationName)
                .openPopup();
        } else {
            L.marker([latitude, longitude]).addTo(map);
        }
        // console.log("Map initialized for " + mapDivId);
    } catch (e) {
        console.error("Error initializing Leaflet map for " + mapDivId + ": ", e);
    }
};


window.renderChart = (canvasId, type, labels, datasets, xAxisLabel, yAxisLabel, yAxisBeginAtZero = false) => {
    const canvas = document.getElementById(canvasId);
    if (!canvas) {
        console.error(`Canvas element with id '${canvasId}' not found.`);
        return;
    }

    const ctx = canvas.getContext('2d');

    // To prevent issues with re-rendering, destroy any existing chart on the canvas
    let existingChart = Chart.getChart(canvasId);
    if (existingChart) {
        existingChart.destroy();
    }

    new Chart(ctx, {
        type: type, // e.g., 'line', 'bar'
        data: {
            labels: labels, // array of strings for x-axis labels
            datasets: datasets  // array of dataset objects
            // each dataset object: { label: 'My Dataset', data: [1,2,3], borderColor: 'red', backgroundColor: 'rgba(255,0,0,0.1)', fill: true/false }
        },
        options: {
            responsive: true,
            maintainAspectRatio: false, // Important for custom container sizes
            scales: {
                x: {
                    title: {
                        display: !!xAxisLabel, // Show title only if xAxisLabel is provided
                        text: xAxisLabel
                    }
                },
                y: {
                    title: {
                        display: !!yAxisLabel, // Show title only if yAxisLabel is provided
                        text: yAxisLabel
                    },
                    beginAtZero: yAxisBeginAtZero
                }
            }
            // You can add more Chart.js options here if needed
        }
    });
};

