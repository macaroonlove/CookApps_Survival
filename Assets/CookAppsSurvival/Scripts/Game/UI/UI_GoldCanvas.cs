using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CookApps.Game
{
    public class UI_GoldCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text_gold;
        private GoldSystem _goldSystem;

        IEnumerator Start()
        {
            _goldSystem = BattleManager.Instance.GetSubSystem<GoldSystem>();

            while (_goldSystem == null)
            {
                yield return null;
                _goldSystem = BattleManager.Instance.GetSubSystem<GoldSystem>();
            }

            text_gold.text = _goldSystem.GetGold().ToString();

            _goldSystem.onChangedGold += OnChangeGold;
        }

        void OnDestroy()
        {
            if (_goldSystem != null)
            {
                _goldSystem.onChangedGold -= OnChangeGold;
            }
        }

        private void OnChangeGold(int gold)
        {
            text_gold.text = gold.ToString();
        }
    }
}