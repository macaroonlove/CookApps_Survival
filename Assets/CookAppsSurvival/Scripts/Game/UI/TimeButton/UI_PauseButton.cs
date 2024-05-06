using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CookApps.Game
{
    public class UI_PauseButton : MonoBehaviour
    {
        [SerializeField] private Button button_Pause;
        [SerializeField] private Image image_Pause;
        [SerializeField] private Sprite sprite_Play;
        [SerializeField] private Sprite sprite_Pause;

        private TimeSystem _timeSystem;

        IEnumerator Start()
        {
            button_Pause.onClick.AddListener(OnClick);
            _timeSystem = BattleManager.Instance.GetSubSystem<TimeSystem>();

            while (_timeSystem == null)
            {
                yield return null;
                _timeSystem = BattleManager.Instance.GetSubSystem<TimeSystem>();
                ApplySprite();
            }
        }

        private void OnClick()
        {
            _timeSystem.Pause();
            ApplySprite();
        }

        private void ApplySprite()
        {
            if (_timeSystem.isPause)
            {
                image_Pause.sprite = sprite_Play;
            }
            else
            {
                image_Pause.sprite = sprite_Pause;
            }
        }
    }
}
