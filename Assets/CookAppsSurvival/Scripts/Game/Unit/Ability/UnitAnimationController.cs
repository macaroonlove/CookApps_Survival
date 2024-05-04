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

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            hash_Move = Animator.StringToHash("move");
            hash_Attack = Animator.StringToHash("attack");
        }

        internal void Initialze(Unit unit)
        {
            _unit = unit;
        }

        public void Attack()
        {
            _animator.SetTrigger(hash_Attack);
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
