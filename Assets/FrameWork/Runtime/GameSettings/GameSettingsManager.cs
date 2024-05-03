using FrameWork.Sound;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.GameSettings
{
	public static class GameSettingsManager
	{
		private static readonly Vector2 MinScreenSize = new Vector2(1024, 768);

		// ------------------------------------------------------------------------------------------------------------
		#region main

		/// <summary> Restore saved settings. You normally call this as soon as the game has started </summary>
		public static void RestoreSettings()
		{
			// restore sound volume
			var masterVolume = PlayerPrefs.GetFloat($"Settings.Volume.0", 0.5f);
			SetSoundVolume(Audio.AudioType.Master, masterVolume);
			var musicVolume = PlayerPrefs.GetFloat($"Settings.Volume.1", 0.5f);
			SetSoundVolume(Audio.AudioType.Music, musicVolume);
			var sfxVolume = PlayerPrefs.GetFloat($"Settings.Volume.2", 0.5f);
			SetSoundVolume(Audio.AudioType.SFX, sfxVolume);
			var uiVolume = PlayerPrefs.GetFloat($"Settings.Volume.3", 1f);
			SetSoundVolume(Audio.AudioType.UI, uiVolume);
			var voiceVolume = PlayerPrefs.GetFloat($"Settings.Volume.4", 1f);
			SetSoundVolume(Audio.AudioType.Voice, voiceVolume);

			// restore quality setting
			int q = PlayerPrefs.GetInt("Settings.Quality", QualitySettings.GetQualityLevel());
			QualitySettings.SetQualityLevel(q);
		}

		#endregion
		// ------------------------------------------------------------------------------------------------------------
		#region resolution

		public static string[] ScreenModes = new[] { "Exclusive FullScreen", "FullScreen Window", "Maximized Window", "Windowed" };

		// for handlers interested in knowing when resolution changed
		public static event System.Action ResolutionChanged;
		private static List<Resolution> Resolutions = new List<Resolution>();

		/// <summary> A list of strings representing the available screen resolutions. </summary>
		public static List<string> ScreenResolutions
		{
			get
			{
				List<string> l = new List<string>(Screen.resolutions.Length);
				Resolutions.Clear();

				if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
				{
					// list all resolutionswith refresh rates  when in exclusive fullscreen
					for (int i = 0; i < Screen.resolutions.Length; i++)
					{
						if (Screen.resolutions[i].width >= MinScreenSize.x && Screen.resolutions[i].height >= MinScreenSize.y)
						{
							Resolutions.Add(Screen.resolutions[i]);
							l.Add(Screen.resolutions[i].ToString());
						}
					}
				}
				else
				{
					// dop not include refresh rate options when not exclusive
					for (int i = 0; i < Screen.resolutions.Length; i++)
					{
						if (Screen.resolutions[i].width >= MinScreenSize.x && Screen.resolutions[i].height >= MinScreenSize.y)
						{
							var res = $"{Screen.resolutions[i].width} x {Screen.resolutions[i].height}";
							if (!l.Contains(res))
							{
								Resolutions.Add(Screen.resolutions[i]);
								l.Add(res);
							}
						}
					}
				}

				return l;
			}
		}

		/// <summary> Get/Set current screen resolution index. This is an integer value representing a resolution from
		/// the list of supported resolutions. The list of resolutions is retrieved via ScreenResolutions property. 
		/// It will return -1 if the resolution index could not be determined </summary>
		public static int ScreenResolutionIndex
		{
			get
			{
				int _screenResolutionIdx = -1;

				int w = Screen.width;
				int h = Screen.height;
				if (Screen.fullScreenMode != FullScreenMode.Windowed)
				{
					w = Screen.currentResolution.width;
					h = Screen.currentResolution.height;
				}

				for (int i = Resolutions.Count - 1; i >= 0; i--)
				{
					if (w == Resolutions[i].width && h == Resolutions[i].height)
					{
						_screenResolutionIdx = i;
						if (Screen.fullScreenMode != FullScreenMode.ExclusiveFullScreen ||
							(   // the refresh can be out with about 1 or 2 values so just get it close
								Screen.currentResolution.refreshRate < Resolutions[i].refreshRate + 2 &&
								Screen.currentResolution.refreshRate > Resolutions[i].refreshRate - 2
							))
						{
							break; // break now if the exact resolution with refresh rate was found
						}
					}
				}

				return _screenResolutionIdx;
			}

			set
			{
				if (value >= 0 && value < Resolutions.Count)
				{
					Screen.SetResolution(Resolutions[value].width, Resolutions[value].height, Screen.fullScreenMode, Resolutions[value].refreshRate);
					PlayerPrefs.Save();

					ResolutionChanged?.Invoke();
				}
			}
		}

		/// <summary> Get or Set whether the game is in fullscreen mode or not. 
		/// This toggles between FullScreenWindow and Windowed. </summary>
		public static bool Fullscreen
		{
			get => Screen.fullScreenMode != FullScreenMode.Windowed;
			set => SetScreenMode(value ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
		}

		public static FullScreenMode ScreenMode
		{
			get => Screen.fullScreenMode;
			set => SetScreenMode(value);
		}

		public static int ScreenModeIndex
		{
			get => (int)Screen.fullScreenMode;
			set => SetScreenMode((FullScreenMode)value);
		}

		private static void SetScreenMode(FullScreenMode mode)
		{
			int w = Screen.width;
			int h = Screen.height;
			if (Screen.fullScreenMode != FullScreenMode.Windowed)
			{
				w = Screen.currentResolution.width;
				h = Screen.currentResolution.height;
			}

			Screen.SetResolution(w, h, mode, Screen.currentResolution.refreshRate);
			PlayerPrefs.Save();

			ResolutionChanged?.Invoke();
		}

		#endregion
		// ------------------------------------------------------------------------------------------------------------
		#region quality

		/// <summary> An array of quality level names as set up in Quality Settings editor; menu: Edit > Project Settings > Quality </summary>
		public static string[] GFXQualityLevels => QualitySettings.names;

		/// <summary> Get or Set the quality level to use by the index into the list of defined quality levels. The 1st defined level's index will be 0, the 2nd will be 1, the 3rd will be 2, and so on.
		/// These quality levels are created in the Quality Settings editor; menu: Edit > Project Settings > Quality </summary>
		public static int GFXQualityLevelIndex
		{
			get => QualitySettings.GetQualityLevel();
			set
			{
#if !UNITY_EDITOR
				QualitySettings.SetQualityLevel(value);
#endif
				PlayerPrefs.SetInt("Settings.Quality", value);
				PlayerPrefs.Save();
			}
		}

		#endregion
		// ------------------------------------------------------------------------------------------------------------
		#region sound

		// for handlers interested in knowing when any of the volume types changes
		public static event System.Action<Audio.AudioType, float> SoundVolumeChanged;

		/// <summary> Set sound volume of specified sound type. The value is a float value between 0 (no sound) and 1 (full). So (0.5) is half the sound volume.</summary>
		public static void SetSoundVolume(Audio.AudioType type, float value)
		{
			int idx = (int)type;
			value = Mathf.Clamp01(value);
			PlayerPrefs.SetFloat($"Settings.Volume.{idx}", value);
			PlayerPrefs.Save();

			switch (type)
			{
				case Audio.AudioType.Master:
					SoundManager.GlobalVolume = value;
					break;
				case Audio.AudioType.Music:
					SoundManager.GlobalMusicVolume = value;
					break;
				case Audio.AudioType.SFX:
					SoundManager.GlobalSFXVolume = value;
					break;
				case Audio.AudioType.UI:
					SoundManager.GlobalUIVolume = value;
					break;
				case Audio.AudioType.Voice:
					SoundManager.GlobalVoiceVolume = value;
					break;
			}

			SoundVolumeChanged?.Invoke(type, value);
		}

		/// <summary> Get sound volume of specified sound type. The value is a float value between 0 (no sound) and 1 (full). So (0.5) is half the sound volume.</summary>
		public static float GetSoundVolume(Audio.AudioType type)
		{
			switch (type)
			{
				case Audio.AudioType.Master:
					return SoundManager.GlobalVolume;
				case Audio.AudioType.Music:
					return SoundManager.GlobalMusicVolume;
				case Audio.AudioType.SFX:
					return SoundManager.GlobalSFXVolume;
				case Audio.AudioType.UI:
					return SoundManager.GlobalUIVolume;
				case Audio.AudioType.Voice:
					return SoundManager.GlobalVoiceVolume;
			}
			return 0f;
		}

		#endregion
		// ------------------------------------------------------------------------------------------------------------
	}
}
