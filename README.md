# Flight-Map-Web-App
Shows the path an airplane has gone on a map of the world

## About
You know the map that they show on flights of the distance the plane has gone? As a huge traveling fan, I have spent nice portion of 12 hour flights simply watching the the plane inch forward on the screen. This project does exactly that (and more!) I developed this as the  third assignment in my Advanced Programming II course. 

## How to run
### The server side
Theoretically, this program runs with the Flight Gear (I talk about Flight Gear extensively here:
https://github.com/atararazin/Flight-Simulator-Desktop-App/blob/master/README.md)
and shows exactly what a real airplane would show. However, that would require you to run this program for hours in order for you to see 
any progress. Therefore, we created a python server that flies at quite the speed and travels all over the world within seconds. I uploaded
it in zip form, because it has a few files that the server uses, so just download the zip and run it. The zip is called server.zip. The server runs on python 3, so make sure you have it on your computer!

### The client side - our program
After the server is already running, now we can actually run the program. The program can take the 4 following URIs:

**`/display/ip/port`:** of course ip and port are numbers here, for instance you can run `/display/127.0.0.1/5402`. If you are using my server to run it, put in `display/127.0.0.1/5402`. When you type in this URI, it will show the map with a dot of the plane's current location.

**`display/ip/port/num`:** Here too, the ip, port and num are numbers and not those actual words. This will display the path at a rate of `num` times per second, meaning it gets the location from the server `num` many times per second. An example: `display/127.0.0.1/5402/4` will run connect to the server at ip `127.0.0.1`, port `5402`, and get the location 4 times per second.

**`/save/ip/port/num1/num2/file`:** This displays the path as it did before, but additionally will create a file called `file` (or whatever name you give it), and will save the values there. Example: `save/127.0.0.1/5402/4/10/flight1` will get the location from the simulator 4 times per second for 10 seconds. It will both display the map and save the data to a file called `flight1`.

**`/display/file/num`:** This can only be run after a `save` has been done. Meaning we assume that there exists a valid file called `file` with the necessary data. `/display/flight1/4` will upload the data from the file `flight1` and run for 4 times per second. At the end it will throw an alert.

For full details, check out the file I uploaded מטלה מספר 3


Bon Voyage!
![alt text](https://github.com/atararazin/Flight-Map-Web-App/blob/master/flight%20map.jpg)
