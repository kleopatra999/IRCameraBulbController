/*

Possible Commands:

Start an expsoure run:
EXP:exposureLength:spacing:mirrorUp:quantity

Abort an exposure run:
ABT

*/

#define DEBUG

#include <multiCameraIrControl.h>
#include "CameraController.h"
#include "DebugUtils.h"

/* Declare some constants */
enum {
	MAX_COMMAND_LENGTH = 20,
	BAUD_RATE = 19200
};
static const char COMMAND_START[] = "EXP";
static const char COMMAND_ABORT[] = "ABT";

/* Pin definitions*/
int statusLed = 13;
int cameraIrPin = 9;

/* Members */
char commandBuffer[MAX_COMMAND_LENGTH] = "";
char commandPosition = 0;

unsigned int lastOperationStart;
unsigned int lastSerialUpdate;

CameraState state;
ExposureData data;

/* Objects */
Nikon nikonDevice(9);

/* Mandatory functions */
void setup() {
	pinMode(statusLed, OUTPUT);
	Serial.begin(BAUD_RATE);

	digitalWrite(statusLed, HIGH);
	delay(500);
	digitalWrite(statusLed, LOW);
}

void loop() {

	// Check for a serial command.
	// Commands will start anything that needs starting.
	if (Serial.available() && GetSerialCommandString()) {
		DEBUG_PRINT(commandBuffer);
		ProcessCommandBuffer(commandBuffer);
	}

	// Do any work that needs doing.
	if (state != IDLE){

	}

	// Update back to the device if needed.

}

/* Custom functions */

boolean GetSerialCommandString(){
	boolean complete = false;

	while (Serial.available()) {
		char c = Serial.read();

		switch(c){
			case '\n':
				complete = CompleteBuffer();
				break;

			case '\r': // ignore carriage returns
				break;

			default:
				DEBUG_PRINT(c);
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
	commandBuffer[commandPosition] = '\0';
	commandPosition = 0;
	return true;
}

void ProcessCommandBuffer(char* buffer){
	// Ensure we've actually got something to parse.
	if (buffer != NULL && strlen(buffer) > 0){
		// Have we seen a start command
		if (strstr(buffer, COMMAND_START) != NULL){
			DEBUG_PRINT("Start");
			BeginExposure();

		// Have we seen an abort command
		} else if (strstr(buffer, COMMAND_ABORT) != NULL) {
			DEBUG_PRINT("Stop");
			// If we're exposing then end the exposure.
			if (state == EXPOSING) EndExposure();
			// Go back to idle.
			state = IDLE;
		}
	}
}

void BeginExposure(){
	digitalWrite(statusLed, HIGH);
	nikonDevice.shutterNow();
}

void EndExposure(){
	digitalWrite(statusLed, LOW);
	nikonDevice.shutterNow();
}

int SecondsSinceTime(int t){
	return (millis() - t) / 1000;
}
