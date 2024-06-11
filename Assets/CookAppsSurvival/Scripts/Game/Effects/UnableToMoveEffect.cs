using System;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Effects/Abnormal/UnableToMoveEffect", fileName = "UnableToMoveEffect", order = 0)]
    public class UnableToMoveEffect : Effect
    {
#if UNITY_EDITOR
        public override void Draw(Rect rect)
        {

        }
#endif

        public override string GetLabel()
        {
            return "이동 불가";
        }
    }
}

