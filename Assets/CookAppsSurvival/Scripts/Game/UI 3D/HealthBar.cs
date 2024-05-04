using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CookApps.Game
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image image_fill;
        private HealthAbility _healthAbility;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
            _healthAbility = GetComponentInParent<HealthAbility>();
            _healthAbility.onChangedHealth += OnChangedHealth;
            _healthAbility.onDeath += OnDeath;
        }

        private void OnDestroy()
        {
            _healthAbility.onChangedHealth -= OnChangedHealth;
            _healthAbility.onDeath -= OnDeath;
        }

        private void OnEnable()
        {
            _canvasGroup.alpha = 1;
        }

        private void OnChangedHealth(int hp)
        {
            var maxHp = _healthAbility.maxHp;
            var per = hp / (float)maxHp;
            image_fill.fillAmount = per;
        }

        private void OnDeath()
        {
            _canvasGroup.alpha = 0;
        }
    }
}
