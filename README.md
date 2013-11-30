IRCameraBulbController
======================

**IRCameraBulbController** is a simple Ardiuno sketch designed to allow control of a *Nikon* SLR in bulb mode via the cameras IR port.  This was initially intended to be used for astrophotography but there is no reason that it can't be used for other uses such as time lapse.

Commands are sent over the Arduinos serial port and will trigger a sequence of bulb exposures on the SLR.

The sketch is set to 19200 baud, but this can be set by changing the `BAUD_RATE` value near the top of the sketch.

Prerequisites
-------------
This sketch uses the *Multi Camera IR Control* library.  This can be found at: [http://sebastian.setz.name/arduino/my-libraries/multi-camera-ir-control/](http://sebastian.setz.name/arduino/my-libraries/multi-camera-ir-control/)

Instructions on how to install this can be found on the Arduino website [http://arduino.cc/en/Guide/Libraries](http://arduino.cc/en/Guide/Libraries)

Commands
--------
The command string should be newline (`\n`) terminated when sent over serial.

**Start new sequence**
`EXP:{duration}:{gap between exposures}:{mirrorUp}:{quantity}`

+ *duration*: Duration in seconds the exposures should be.
+ *gap between exposures*: between each exposure how long should the system wait before starting a new one.
+ *mirrorUp*: Enable/Disable (1/0) mirror up mode.  When enabled mirror up will trigger the shutter once to raise the mirror, wait 2 seconds, then trigger again to open the shutter.
+ *quantity*: how many exposures should the system take.  If this is 0 nothing will happen.

example:
`EXP:120:30:1:10` Will take 10 exposures, 120s (2 min) long, 30s apart and will use mirror up to do this.

If a second EXP command is issued while one is running then an `ERR:busy` message will be sent, and the current sequence will continue.

**Abort sequence**
`ABT`  This command will stop a currently running exposure and then put the system back into an idle state to wait for the next command.

Responces
---------

+ `SRT` Sequence started
+ `FIN` Sequence finished
+ `ERR:{error text}` There was an error as described in `error text`

Debugging
---------
If you wish to modify this sketch at all there is an included debug library which can be enabled by defining DEBUG (currently commented out) on the line above `#include "DebugUtils.h"`.  This will enable the `DEBUG_PRINT(value)` function which will out put over serial detailing the time (using `millis()`), function, file, line and value passed in.

This has prooved very useful in developing this sketch.
