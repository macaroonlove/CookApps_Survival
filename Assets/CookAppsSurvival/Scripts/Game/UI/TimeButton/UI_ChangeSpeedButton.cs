using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CookApps.Game
{
    public class UI_ChangeSpeedButton : MonoBehaviour
    {
        [SerializeField] private Button button_Speed;
        [SerializeField] private TextMeshProUGUI text_Speed;

        private TimeSystem _timeSystem;

        IEnumerator Start()
        {
            button_Speed.onClick.AddListener(OnClick);
            _timeSystem = BattleManager.Instance.GetSubSystem<TimeSystem>();

            while (_timeSystem == null)
            {
                yield return null;
                _timeSystem = BattleManager.Instance.GetSubSystem<TimeSystem>();
                ApplyText();
            }
        }

        private void OnClick()
        {
            if (_timeSystem.ChangeSpeed())
            {
                ApplyText();
            }
        }

        private void ApplyText()
        {
            switch (_timeSystem.speed)
            {
                case ESpeed.None:
                    text_Speed.text = "x1";
                    break;
                case ESpeed.Speedx2:
                    text_Speed.text = "x2";
                    break;
                case ESpeed.Speedx3:
                    text_Speed.text = "x3";
                    break;
                case ESpeed.Speedx4:
                    text_Speed.text = "x4";
                    break;
            }
        }
    }
}
