function draw() {
    //use these to neutralize the values from lot and lat
    var height = window.innerHeight;
    var width = window.innerWidth;
    var canvas = document.getElementById('displayCanvas');
        var context = canvas.getContext('2d');

    var xCoord = 30;
    var yCoord = 70;

    //draw the outer circle
    var pointSize = 4;
    context.fillStyle = "000000"//black
    context.beginPath(); //Start path
    context.arc(xCoord, yCoord, pointSize, 0, Math.PI * 2, true); // Draw a point using the arc function of the canvas with a point structure.
    context.fill();

    //draw the inner circle
    context.fillStyle = "#ff2626"; //red color
    pointSize = 3;
    context.beginPath(); //Start path
    context.arc(xCoord, yCoord, pointSize, 0, Math.PI * 2, true); // Draw a point using the arc function of the canvas with a point structure.
    context.fill();
}