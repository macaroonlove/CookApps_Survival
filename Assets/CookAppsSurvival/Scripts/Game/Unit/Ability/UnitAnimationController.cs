using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 유닛 애니메이션 클래스
    /// </summary>
    public class UnitAnimationController : MonoBehaviour
    {
        private Unit _unit;
        private Animator _animator;

        int hash_Move;
        int hash_Attack;
        int hash_Skill;
        int hash_Hit;
        int hash_Death;
        int hash_Victory;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            hash_Move = Animator.StringToHash("move");
            hash_Attack = Animator.StringToHash("attack");
            hash_Skill = Animator.StringToHash("skill");
            hash_Hit = Animator.StringToHash("hit");
            hash_Death = Animator.StringToHash("death");
            hash_Victory = Animator.StringToHash("victory");
        }

        internal void Initialze(Unit unit)
        {
            _unit = unit;

            _unit.healthAbility.onChangedHealth += Hit;
        }

        internal void DeInitialize()
        {
            _unit.healthAbility.onChangedHealth -= Hit;
        }

        internal void Attack()
        {
            _animator.SetTrigger(hash_Attack);
        }
        
        internal void Skill()
        {
            _animator.SetTrigger(hash_Skill);
        }

        internal void Hit(int a)
        {
            _animator.SetTrigger(hash_Hit);
        }

        internal void Death()
        {
            _animator.SetTrigger(hash_Death);
        }

        internal void Victory()
        {
            _animator.SetTrigger(hash_Victory);
        }

        private void Update()
        {
            if (_unit.moveAbility.isMove)
            {
                _animator.SetBool(hash_Move, true);
            }
            else
            {
                _animator.SetBool(hash_Move, false);
            }
        }
    }
}
