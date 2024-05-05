using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public abstract class AttackAbility : MonoBehaviour
    {
        [SerializeField, ReadOnly] protected bool isAttackAble;
        [SerializeField, ReadOnly] protected float cooldownTime = 0;

        protected int _pureATK;
        protected float _pureAttackTerm;
        protected float _pureAttackDistance;
        
        /// <summary>
        /// 최종 공격 간격
        /// </summary>
        protected abstract float finalAttackTerm { get; }

        /// <summary>
        /// 최종 공격거리
        /// </summary>
        protected abstract float finalAttackDistance { get; }

        protected virtual void Update()
        {
            if (!isAttackAble) return;

            //공격사이 시간 계산
            if (cooldownTime > 0)
            {
                cooldownTime -= Time.deltaTime;
                return;
            }

            //공격 가능상태
            bool isExcute = Action();
            if (isExcute)
            {
                cooldownTime = finalAttackTerm;
            }
        }

        protected abstract bool Action();
    }
}