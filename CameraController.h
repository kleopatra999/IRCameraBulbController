
enum CameraState {
  IDLE,
  EXPOSING,
  WAIT,
  MIRRORUP
};

typedef struct exposureData_t
{
  int exposureLength;
  int spacing;
  boolean mirrorUp;
  int quantity;
  int taken;
} ExposureData;
