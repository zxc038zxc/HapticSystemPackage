using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEngine;

namespace YGM
{
	public static class UnityiOSCoreHapticsProxy
	{
#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void CoreHapticsCreateEngine();

	[DllImport("__Internal")]
	private static extern void CoreHapticsStopEngine();

	[DllImport("__Internal")]
	private static extern bool CoreHapticsSupported();

	[DllImport("__Internal")]
	private static extern void PlayTransientHaptic(float intensity, float sharpness);
	
	[DllImport("__Internal")]
	private static extern void PlayContinuousHaptic(float intensity, float sharpness, float duration);

	[DllImport("__Internal")]
	private static extern void StopContinuousHaptic();
#else
		private static extern void CoreHapticsCreateEngine();
		private static extern void CoreHapticsStopEngine();
		private static extern bool CoreHapticsSupported();
		private static extern void PlayTransientHaptic(float intensity, float sharpness);
		private static extern void PlayContinuousHaptic(float intensity, float sharpness, float duration);
		private static extern void StopContinuousHaptic();
#endif

		static UnityiOSCoreHapticsProxy()
		{
#if !UNITY_2019_3_OR_NEWER
		throw new Exception("[UnityCoreHaptics] plugin is only supported in Unity 2019.3 or later.");
#endif
		}

		public static void PlayTransientHapticPattern(float intensity, float sharpness)
		{
			PlayTransientHaptic(intensity, sharpness);
		}

		public static void PlayContinuousHapticPattern(float intensity, float sharpness, float duration)
		{
			if (intensity < 0.01f)
			{
				intensity = 0.01f;
			}

			PlayContinuousHaptic(intensity, sharpness, duration);
		}

		public static void StopContinuousHapticPatterns()
		{
			StopContinuousHaptic();
		}

		public static void CreateEngine()
		{
			CoreHapticsCreateEngine();
		}

		public static void StopEngine()
		{
			CoreHapticsStopEngine();
		}

		public static bool SupportsCoreHaptics()
		{
			return CoreHapticsSupported();
		}
	}
}