using FrameWork.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class BuffAbility : MonoBehaviour
    {
        private Unit _unit;

        #region Effect List
        [NonSerialized] public List<ATKIncreaseDataEffect> ATKIncreaseDataEffects = new List<ATKIncreaseDataEffect>();
        [NonSerialized] public List<AttackSpeedIncreaseDataEffect> AttackSpeedIncreaseDataEffects = new List<AttackSpeedIncreaseDataEffect>();
        [NonSerialized] public List<MoveSpeedIncreaseDataEffect> MoveSpeedIncreaseDataEffects = new List<MoveSpeedIncreaseDataEffect>();

        #endregion

        private Dictionary<BuffTemplate, StatusInstance> statusDic = new Dictionary<BuffTemplate, StatusInstance>();

#if UNITY_EDITOR
        [SerializeField, ReadOnly] private List<BuffTemplate> statusList = new List<BuffTemplate>();
#endif

        internal void Initialize(Unit unit)
        {
            _unit = unit;
            _unit.healthAbility.onDeath += HealthAbility_onDeath;
        }

        internal void DeInitialize()
        {
            _unit.healthAbility.onDeath -= HealthAbility_onDeath;
        }

        private void HealthAbility_onDeath()
        {
            ClearStatusEffects();
        }

        internal void ApplyBuff(BuffTemplate template)
        {
            if (this == null || this.gameObject == null)
            {
                return;
            }
            var duration = template.duration;

            var isContained = false;

            if (statusDic.ContainsKey(template))
            {
                isContained = true;

                var instance = statusDic[template];
                if (instance.IsOld(duration))
                {
                    instance.duration = duration;
                    instance.startTime = Time.time;
                    return;
                }
                else
                {
                    return;
                }
            }

            var statusInstance = new StatusInstance(duration, Time.time);
            var corutine = StartCoroutine(CoStatus(statusInstance, template));
            statusInstance.corutine = corutine;
            statusDic.Add(template, statusInstance);

#if UNITY_EDITOR
            statusList.Add(template);
#endif

            if (isContained == false)
            {
                foreach (var effect in template.effects)
                {
                    if (effect is ATKIncreaseDataEffect ATKIncreaseDataEffect)
                    {
                        ATKIncreaseDataEffects.Add(ATKIncreaseDataEffect);
                    }
                    else if (effect is AttackSpeedIncreaseDataEffect AttackSpeedIncreaseDataEffect)
                    {
                        AttackSpeedIncreaseDataEffects.Add(AttackSpeedIncreaseDataEffect);
                    }
                    else if (effect is MoveSpeedIncreaseDataEffect MoveSpeedIncreaseDataEffect)
                    {
                        MoveSpeedIncreaseDataEffects.Add(MoveSpeedIncreaseDataEffect);
                    }
                }
            }
        }

        private IEnumerator CoStatus(StatusInstance statusInstance, BuffTemplate template)
        {
            while (statusInstance.IsCompete == false)
            {
                yield return null;
            }

            foreach (var effect in template.effects)
            {
                if (effect is ATKIncreaseDataEffect ATKIncreaseDataEffect)
                {
                    ATKIncreaseDataEffects.Remove(ATKIncreaseDataEffect);
                }
                else if (effect is AttackSpeedIncreaseDataEffect AttackSpeedIncreaseDataEffect)
                {
                    AttackSpeedIncreaseDataEffects.Remove(AttackSpeedIncreaseDataEffect);
                }
                else if (effect is MoveSpeedIncreaseDataEffect MoveSpeedIncreaseDataEffect)
                {
                    MoveSpeedIncreaseDataEffects.Remove(MoveSpeedIncreaseDataEffect);
                }
            }

            if (statusDic.ContainsKey(template))
            {
                statusDic.Remove(template);

#if UNITY_EDITOR
                statusList.Remove(template);
#endif
            }
        }

        internal void ClearStatusEffects()
        {
            foreach (var status in statusDic)
            {
                var template = status.Key;
                var instance = status.Value;

                // 효과 제거
                foreach (var effect in template.effects)
                {
                    if (effect is ATKIncreaseDataEffect ATKIncreaseDataEffect)
                    {
                        ATKIncreaseDataEffects.Remove(ATKIncreaseDataEffect);
                    }
                    else if (effect is AttackSpeedIncreaseDataEffect AttackSpeedIncreaseDataEffect)
                    {
                        AttackSpeedIncreaseDataEffects.Remove(AttackSpeedIncreaseDataEffect);
                    }
                    else if (effect is MoveSpeedIncreaseDataEffect MoveSpeedIncreaseDataEffect)
                    {
                        MoveSpeedIncreaseDataEffects.Remove(MoveSpeedIncreaseDataEffect);
                    }
                }
                if (instance.corutine != null)
                {
                    StopCoroutine(instance.corutine);
                    instance.corutine = null;
                }

            }

            statusDic.Clear();

#if UNITY_EDITOR
            statusList.Clear();
#endif
        }

        internal bool Contains(BuffTemplate template)
        {
            return statusDic.ContainsKey(template);
        }

        internal bool Contains(List<BuffTemplate> templates)
        {
            var isContains = false;
            foreach (var template in templates)
            {
                if (statusDic.ContainsKey(template))
                {
                    isContains = true;
                }
            }
            return isContains;
        }
    }
}
