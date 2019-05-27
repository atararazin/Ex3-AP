function draw() {
    var height = window.innerHeight;
    var width = window.innerWidth;
    var canvas = document.getElementById('displayCanvas');
    if (canvas.getContext) {
        var context = canvas.getContext('2d');

        context.fillStyle = "#ff2626"; //red color
        var xCoord = 30;
        var yCoord = 70;
        var pointSize = 4;
        context.beginPath(); //Start path
        context.arc(xCoord, yCoord, pointSize, 0, Math.PI * 2, true); // Draw a point using the arc function of the canvas with a point structure.
        context.fill();
    }
}