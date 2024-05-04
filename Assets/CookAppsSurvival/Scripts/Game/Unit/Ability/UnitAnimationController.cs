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
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void Move(bool isMove)
        {
            _animator.SetBool("move", isMove);
        }
    }
}
