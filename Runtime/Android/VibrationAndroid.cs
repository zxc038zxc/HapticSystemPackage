using UnityEngine;

namespace YGM
{
	public class VibrationAndroid
	{
#if UNITY_ANDROID && !UNITY_EDITOR
	private static AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	private static AndroidJavaObject CurrentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	private static AndroidJavaObject AndroidVibrator = CurrentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
		private static AndroidJavaObject AndroidVibrator = null;
#endif

		private static AndroidJavaClass VibrationEffectClass;
		private static AndroidJavaObject VibrationEffect;
		private static int _sdkVersion = -1;

		public static void Vibration(int intensity, long milliseconds)
		{
			StopVibration();

			// SDK Version �j��26 �~�䴩 VibrationEffect �A ���ѩ󦹳B�ڭ̥D�n�ت��� Effect ������_�T�����p�A�ҥH�ݭn�ˬd�O�_�䴩AmplitudeControl
			if (/*AndroidSDKVersion() >= 26 && */AndroidHasAmplitudeControl())
			{
				AndroidVibrationEffectClassInitialization();

				VibrationEffect = VibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", milliseconds, intensity);
				AndroidVibrator.Call("vibrate", VibrationEffect);

#if WaninTaipei
			Debug.Log($"Vibrate Type: AndroidVibrator.Call("vibrate", VibrationEffect), millisecond:{milliseconds}, intensity:{intensity}");
#endif
			}
			else
			{
				AndroidVibrator.Call("vibrate", milliseconds);

#if WaninTaipei
			Debug.Log("Vibrate Type: AndroidVibrator.Call(milliseconds), milliseconds:{milliseconds}");
#endif
			}
		}
		private static AndroidJavaObject CreateEffect_Waveform(long[] timings, int repeat)
		{
			return VibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", timings, repeat);
		}

		public static void StopVibration()
		{
			AndroidVibrator.Call("cancel");
		}

		public static bool HasVibrator()
		{
			if (AndroidVibrator == null)
			{
				return false;
			}
			return AndroidVibrator.Call<bool>("hasVibrator");
		}

		/// <summary>
		/// Initializes the VibrationEffectClass if needed.
		/// </summary>
		private static void AndroidVibrationEffectClassInitialization()
		{
			if (VibrationEffectClass == null)
			{
				VibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
			}
		}

		/// <summary>
		/// Returns true if the device running the game has amplitude control
		/// </summary>
		/// <returns></returns>
		private static bool AndroidHasAmplitudeControl()
		{
			return AndroidVibrator.Call<bool>("hasAmplitudeControl");
		}

		/// <summary>
		/// Returns the current Android SDK version as an int
		/// </summary>
		/// <returns>The SDK version.</returns>
		private static int AndroidSDKVersion()
		{
			if (_sdkVersion == -1)
			{
				int apiLevel = int.Parse(SystemInfo.operatingSystem.Substring(SystemInfo.operatingSystem.IndexOf("-") + 1, 3));
				_sdkVersion = apiLevel;
				return apiLevel;
			}
			else
			{
				return _sdkVersion;
			}
		}
	}
}