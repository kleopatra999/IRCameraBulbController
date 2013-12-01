/*
Copyright (c) 2013 Christopher Pearson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

--------------------------------------------------------------------------------

Arduino sketch to controll a camera in bulb, using the IR commands, mode via
a serial connection.

Possible Commands:

Start an expsoure run:
EXP:exposureLength:spacing:mirrorUp {1/0}:quantity

Abort an exposure run:
ABT


Responces:

Start Sequence:
SRT

Sequence finished:
FIN

Error:
ERR:{error description}

Required Libraries:
Multi Camera IR Control
	http://sebastian.setz.name/arduino/my-libraries/multi-camera-ir-control/

*/

// Uncomment the next line to enable debug serial out put.
// To use add a DEBUG_PRINT() where needed.
// #define DEBUG
#include "DebugUtils.h"

#include <multiCameraIrControl.h>
#include "IRCameraBulbController.h"

/* Constants */
enum {
	MAX_COMMAND_LENGTH = (20 + 1), // Allow room for '\0'
	BAUD_RATE = 19200,
	WAIT_MIRRORUP = 2000,
	POS_EXP = 1,
	POS_GAP = 2,
	POS_MIRROR_UP = 3,
	POS_QTY = 4
};
static const char *COMMAND_START = "EXP";
static const char *COMMAND_ABORT = "ABT";

/* Pin definitions*/
int statusLed = 13;
int cameraIrPin = 9;

/* Members */
char commandBuffer[MAX_COMMAND_LENGTH] = "";
char commandPosition = 0;
unsigned long lastOperationStart;

/* Objects and Structures */
Nikon nikonDevice(cameraIrPin);
CameraState state;
ExposureData data;


/* Mandatory functions */
void setup() {
	pinMode(statusLed, OUTPUT);
	Serial.begin(BAUD_RATE);
	state = IDLE;
}

void loop() {
	// Check for a serial command.
	// Commands will start anything that needs starting.
	if (Serial.available() && ReadSerialCommandString()) {
		DEBUG_PRINT(commandBuffer);
		ProcessCommandBuffer(commandBuffer);
	}

	// Do any work that needs doing.
	if (state != IDLE){
		LoopBody();
	}
}

/* Custom functions */

boolean ReadSerialCommandString(){
	boolean complete = false;

	while (Serial.available()) {
		char c = Serial.read();

		switch(c){
			case '\n':
				complete = CompleteBuffer();
				break;

			case '\r': // strip out carriage returns
				break;

			default:
				// Check theres room left in the buffer.
				if(commandPosition < (MAX_COMMAND_LENGTH - 1)) {
					commandBuffer[commandPosition++] = toupper(c);
				} else {
					// Buffers full, null terminate the string and finish.
					complete = CompleteBuffer();
				}
				break;
		}
	}

	return complete;
}

boolean CompleteBuffer() {
	Serial.flush();
	commandBuffer[commandPosition] = '\0';
	commandPosition = 0;
	return true;
}

void ProcessCommandBuffer(char *buffer){
	// Ensure we've actually got something to parse.
	if (buffer != NULL && strlen(buffer) > 0){
		// check if we have we seen a start command
		if (strstr(buffer, COMMAND_START) != NULL){
			DEBUG_PRINT(state);
			if (state == IDLE){
				ProcessStartCommand(buffer);
			} else {
				Serial.flush();
				Serial.println("ERR:Busy");
			}

		// Have we seen an abort command
		} else if (strstr(buffer, COMMAND_ABORT) != NULL) {
			Abort();
		}
	}
}

void ProcessStartCommand(char *buffer){
	int i = 0;

	char *c = strtok(buffer, ":");
	while (c != NULL){
		switch(i++){
			case POS_EXP:
			data.exposureLength = atoi(c);
			break;
			case POS_GAP:
			data.gap = atoi(c);
			break;
			case POS_MIRROR_UP:
			data.mirrorUp = atoi(c);
			break;
			case POS_QTY:
			data.quantity = atoi(c);
			break;
		}

		c = strtok(NULL, ":");
	}

	data.taken = 0;

	if (data.quantity > 0) {
		if (data.mirrorUp){state = MIRRORUP;} else {state = EXPOSING;};
		BeginExposure();
		ResetLastActionTime();
		Serial.println("SRT");
	}
}

void Abort(){
	// If we're exposing then end the exposure.
	if (state == EXPOSING) {EndExposure();}
	// Go back to idle.
	state = IDLE;
	Serial.println("FIN");
}

void BeginExposure(){
	DEBUG_PRINT("start");
	digitalWrite(statusLed, HIGH);
	nikonDevice.shutterNow();
}

void EndExposure(){
	DEBUG_PRINT("end");
	digitalWrite(statusLed, LOW);
	nikonDevice.shutterNow();
}

void ResetLastActionTime(){
	lastOperationStart = millis();
}

void LoopBody(){
	// Calulate elapsed time since last event
	unsigned long t = millis() - lastOperationStart;

	switch(state) {
		case MIRRORUP:
			// wait 2 seconds then fire the shutter
			if(t > WAIT_MIRRORUP){
				state = EXPOSING;
				BeginExposure();
				ResetLastActionTime();
			}
			break;

		case EXPOSING:
			if (t > (data.exposureLength * 1000)) {
				EndExposure();
				ResetLastActionTime();

						// Are we done?
				data.taken++;
				if (data.taken >= data.quantity){
					state = IDLE;
					Serial.println("FIN");
				} else {
					state = GAP;
				}
			}

			break;

		case GAP:
			if (t > (data.gap * 1000)) {
				BeginExposure();
				ResetLastActionTime();

				if (data.mirrorUp) {
					state = MIRRORUP;
				} else {
					state = EXPOSING;
				};
			}
			break;
	}
}
