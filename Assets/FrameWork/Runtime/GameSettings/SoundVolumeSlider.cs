using FrameWork.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FrameWork.GameSettings
{
    [AddComponentMenu("GameSettings/UI/Sound Volume Slider")]
    public class SoundVolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider targetElement;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Audio.AudioType volumeType = Audio.AudioType.Master;

        // ------------------------------------------------------------------------------------------------------------

        private void Reset()
        {
            targetElement = GetComponentInChildren<Slider>();
        }

        private void Start()
        {
            if (targetElement == null)
            {
                targetElement = GetComponentInChildren<Slider>();
                if (targetElement == null)
                {
                    Debug.Log("[SoundVolumeSlider] Could not find any Slider component on the GameObject.", gameObject);
                    return;
                }
            }

            targetElement.value = GameSettingsManager.GetSoundVolume(volumeType);
            targetElement.onValueChanged.AddListener(OnValueChange);

            if (text != null)
            {
                text.text = (targetElement.value * 100).ToString("F0");
            }
        }

        private void OnValueChange(float value)
        {
            GameSettingsManager.SetSoundVolume(volumeType, value);
            if (text != null) text.text = (value * 100).ToString("F0");
        }

        // ------------------------------------------------------------------------------------------------------------
    }
}
