using FrameWork.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class AbnormalStatusAbility : MonoBehaviour
    {
        private Unit _unit;

        #region Effect List
        [NonSerialized] public List<UnableToMoveEffect> UnableToMoveEffects = new List<UnableToMoveEffect>();
        [NonSerialized] public List<UnableToAttackEffect> UnableToAttackEffects = new List<UnableToAttackEffect>();
        #endregion

        private Dictionary<AbnormalStatusTemplate, StatusInstance> statusDic = new Dictionary<AbnormalStatusTemplate, StatusInstance>();

#if UNITY_EDITOR
        [SerializeField, ReadOnly] private List<AbnormalStatusTemplate> statusList = new List<AbnormalStatusTemplate>();
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

        internal void ApplyAbnormalStatus(AbnormalStatusTemplate template, float duration)
        {
            if (this == null || this.gameObject == null)
            {
                return;
            }

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
                    if (effect is UnableToMoveEffect unableToMoveEffect)
                    {
                        UnableToMoveEffects.Add(unableToMoveEffect);
                    }
                    else if (effect is UnableToAttackEffect unableToAttackEffect)
                    {
                        UnableToAttackEffects.Add(unableToAttackEffect);
                    }
                }
            }
        }

        private IEnumerator CoStatus(StatusInstance statusInstance, AbnormalStatusTemplate template)
        {
            while (statusInstance.IsCompete == false)
            {
                yield return null;
            }

            foreach (var effect in template.effects)
            {
                if (effect is UnableToMoveEffect unableToMoveEffect)
                {
                    UnableToMoveEffects.Remove(unableToMoveEffect);
                }
                else if (effect is UnableToAttackEffect unableToAttackEffect)
                {
                    UnableToAttackEffects.Remove(unableToAttackEffect);
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
                    if (effect is UnableToMoveEffect unableToMoveEffect)
                    {
                        UnableToMoveEffects.Remove(unableToMoveEffect);
                    }
                    else if (effect is UnableToAttackEffect unableToAttackEffect)
                    {
                        UnableToAttackEffects.Remove(unableToAttackEffect);
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

        internal bool Contains(AbnormalStatusTemplate template)
        {
            return statusDic.ContainsKey(template);
        }

        internal bool Contains(List<AbnormalStatusTemplate> templates)
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
