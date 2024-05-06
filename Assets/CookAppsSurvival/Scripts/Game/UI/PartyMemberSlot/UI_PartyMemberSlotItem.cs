using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CookApps.Game
{
    public class UI_PartyMemberSlotItem : MonoBehaviour
    {
        [SerializeField] private Image image_Face;
        [SerializeField] private TextMeshProUGUI text_Level;
        [SerializeField] private TextMeshProUGUI text_Name;
        [SerializeField] private TextMeshProUGUI text_ATK;
        [SerializeField] private Slider slider_Exp;

        private PartyUnit _unit;
        private LevelSystem _levelSystem;

        public void Initialize(PartyUnit unit)
        {
            _unit = unit;
            _levelSystem = BattleManager.Instance.GetSubSystem<LevelSystem>();

            image_Face.sprite = unit.template.face;
            text_Name.text = unit.template.displayName;

            text_Level.text = $"LV. {unit.GetLevel()}";
            slider_Exp.value = _levelSystem.GetExpGauge(unit);

            _levelSystem.onGainExp += OnGainExp;
            _levelSystem.onLevelUp += OnLevelUp;
        }

        void Update()
        {
            if (_unit == null) return;
            if (!_unit.healthAbility.IsAlive) return;

            text_ATK.text = _unit.agentAttackAbility.finalATK.ToString();
        }

        private void OnGainExp()
        {
            slider_Exp.value = _unit.GetExp() / _unit.GetNeedExp();
        }

        private void OnLevelUp()
        {
            text_Level.text = $"LV. {_unit.GetLevel()}";
            slider_Exp.value = 0;
        }
    }
}