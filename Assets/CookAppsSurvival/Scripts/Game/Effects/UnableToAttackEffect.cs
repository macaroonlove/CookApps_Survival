using System;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Effects/Abnormal/UnableToAttackEffect", fileName = "UnableToAttackEffect", order = 0)]
    public class UnableToAttackEffect : Effect
    {
#if UNITY_EDITOR
        public override void Draw(Rect rect)
        {

        }
#endif

        public override string GetLabel()
        {
            return "공격 불가";
        }
    }
}

