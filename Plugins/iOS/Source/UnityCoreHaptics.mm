#import "UnityFramework/UnityFramework-Swift.h"

extern "C"
{
  // Other functions
  void CoreHapticsCreateEngine()
  {
    [UnityCoreHaptics CreateEngine];
  }
  
  void CoreHapticsStopEngine()
  {
    [UnityCoreHaptics StopEngine];
  }

  bool CoreHapticsSupported()
  {
    return [UnityCoreHaptics SupportsCoreHaptics];
  }

  void PlayTransientHaptic(float intensity, float sharpness)
  {
    [UnityCoreHaptics PlayTransientHapticWithIntensity:intensity sharpness:sharpness];
  }

  void PlayContinuousHaptic(float intensity, float sharpness, float duration)
  {
    [UnityCoreHaptics PlayContinuousHapticWithIntensity:intensity sharpness:sharpness duration:duration];
  }
  
  void StopContinuousHaptic()
  {
    [UnityCoreHaptics StopContinuousHaptic];
  }
}
