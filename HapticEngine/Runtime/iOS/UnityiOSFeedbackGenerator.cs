using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

#if UNITY_IOS
	using UnityEngine.iOS;
#endif

namespace YGM
{
	public enum HapticTypes
	{
		Selection,
		Success,
		Warning,
		Failure,
		LightImpact,
		MediumImpact,
		HeavyImpact,
		RigidImpact,
		SoftImpact,
		None
	}

	public static class UnityiOSFeedbackGenerator
	{
#if UNITY_IOS && !UNITY_EDITOR
			[DllImport ("__Internal")]
			private static extern void InstantiateFeedbackGenerators();
			[DllImport ("__Internal")]
			private static extern void ReleaseFeedbackGenerators();
			[DllImport ("__Internal")]
			private static extern void SelectionHaptic();
			[DllImport ("__Internal")]
			private static extern void SuccessHaptic();
			[DllImport ("__Internal")]
			private static extern void WarningHaptic();
			[DllImport ("__Internal")]
			private static extern void FailureHaptic();
			[DllImport ("__Internal")]
			private static extern void LightImpactHaptic();
			[DllImport ("__Internal")]
			private static extern void MediumImpactHaptic();
			[DllImport ("__Internal")]
			private static extern void HeavyImpactHaptic();
			[DllImport ("__Internal")]
			private static extern void RigidImpactHaptic();
			[DllImport ("__Internal")]
			private static extern void SoftImpactHaptic();
#else
		private static void InstantiateFeedbackGenerators() { }
		private static void ReleaseFeedbackGenerators() { }
		private static void SelectionHaptic() { }
		private static void SuccessHaptic() { }
		private static void WarningHaptic() { }
		private static void FailureHaptic() { }
		private static void LightImpactHaptic() { }
		private static void MediumImpactHaptic() { }
		private static void HeavyImpactHaptic() { }
		private static void RigidImpactHaptic() { }
		private static void SoftImpactHaptic() { }
#endif
		private static bool iOSHapticsInitialized = false;

		/// <summary>
		/// Call this method to initialize the haptics. If you forget to do it, Nice Vibrations will do it for you the first time you
		/// call iOSTriggerHaptics. It's better if you do it though.
		/// </summary>
		public static void InitializeUIFeedbackGenerator()
		{
			InstantiateFeedbackGenerators();
			iOSHapticsInitialized = true;
		}

		/// <summary>
		/// Releases the feedback generators, usually you'll want to call this at OnDisable(); or anytime you know you won't need 
		/// vibrations anymore.
		/// </summary>
		public static void ReleaseUIFeedbackGenerator()
		{
			ReleaseFeedbackGenerators();
		}

		/// <summary>
		/// iOS only : triggers a haptic feedback of the specified type
		/// </summary>
		/// <param name="type">Type.</param>
		public static void TriggerUIFeedbackGenerator(HapticTypes type, bool defaultToRegularVibrate = true)
		{
			if (!iOSHapticsInitialized)
			{
				InitializeUIFeedbackGenerator();
			}

			// this will trigger a standard vibration on all the iOS devices that don't support haptic feedback

			if (SupportsUIFeedbackGenerator())
			{
				switch (type)
				{
					case HapticTypes.Selection:
						SelectionHaptic();
						break;

					case HapticTypes.Success:
						SuccessHaptic();
						break;

					case HapticTypes.Warning:
						WarningHaptic();
						break;

					case HapticTypes.Failure:
						FailureHaptic();
						break;

					case HapticTypes.LightImpact:
						LightImpactHaptic();
						break;

					case HapticTypes.MediumImpact:
						MediumImpactHaptic();
						break;

					case HapticTypes.HeavyImpact:
						HeavyImpactHaptic();
						break;

					case HapticTypes.RigidImpact:
						RigidImpactHaptic();
						break;

					case HapticTypes.SoftImpact:
						SoftImpactHaptic();
						break;
				}
			}
			else if (defaultToRegularVibrate)
			{
#if UNITY_IOS
			Handheld.Vibrate();
#endif
			}
		}

		public static bool SupportsUIFeedbackGenerator()
		{
			bool hapticsSupported = false;
#if UNITY_IOS
		DeviceGeneration generation = Device.generation;
		if ((generation == DeviceGeneration.iPhone3G)
		    || (generation == DeviceGeneration.iPhone3GS)
		    || (generation == DeviceGeneration.iPodTouch1Gen)
			    || (generation == DeviceGeneration.iPodTouch2Gen)
			    || (generation == DeviceGeneration.iPodTouch3Gen)
			    || (generation == DeviceGeneration.iPodTouch4Gen)
			    || (generation == DeviceGeneration.iPhone4)
			    || (generation == DeviceGeneration.iPhone4S)
			    || (generation == DeviceGeneration.iPhone5)
			    || (generation == DeviceGeneration.iPhone5C)
			    || (generation == DeviceGeneration.iPhone5S)
			    || (generation == DeviceGeneration.iPhone6)
			    || (generation == DeviceGeneration.iPhone6Plus)
			    || (generation == DeviceGeneration.iPhone6S)
			    || (generation == DeviceGeneration.iPhone6SPlus)
                || (generation == DeviceGeneration.iPhoneSE1Gen)
                || (generation == DeviceGeneration.iPad1Gen)
                || (generation == DeviceGeneration.iPad2Gen)
                || (generation == DeviceGeneration.iPad3Gen)
                || (generation == DeviceGeneration.iPad4Gen)
                || (generation == DeviceGeneration.iPad5Gen)
                || (generation == DeviceGeneration.iPadAir1)
                || (generation == DeviceGeneration.iPadAir2)
                || (generation == DeviceGeneration.iPadMini1Gen)
                || (generation == DeviceGeneration.iPadMini2Gen)
                || (generation == DeviceGeneration.iPadMini3Gen)
                || (generation == DeviceGeneration.iPadMini4Gen)
                || (generation == DeviceGeneration.iPadPro10Inch1Gen)
                || (generation == DeviceGeneration.iPadPro10Inch2Gen)
                || (generation == DeviceGeneration.iPadPro11Inch)
                || (generation == DeviceGeneration.iPadPro1Gen)
                || (generation == DeviceGeneration.iPadPro2Gen)
                || (generation == DeviceGeneration.iPadPro3Gen)
                || (generation == DeviceGeneration.iPadUnknown)
                || (generation == DeviceGeneration.iPodTouch1Gen)
                || (generation == DeviceGeneration.iPodTouch2Gen)
                || (generation == DeviceGeneration.iPodTouch3Gen)
                || (generation == DeviceGeneration.iPodTouch4Gen)
                || (generation == DeviceGeneration.iPodTouch5Gen)
                || (generation == DeviceGeneration.iPodTouch6Gen)
			    || (generation == DeviceGeneration.iPhone6SPlus))
			    {
			        hapticsSupported = false;
			    }
			    else
			    {
			        hapticsSupported = true;
			    }
#endif
			return hapticsSupported;
		}

	}
}