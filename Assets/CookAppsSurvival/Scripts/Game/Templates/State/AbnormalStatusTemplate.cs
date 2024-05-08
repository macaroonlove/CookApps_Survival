using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/State/AbnormalStatus", fileName = "AbnormalStatus", order = 0)]
    public class AbnormalStatusTemplate : ScriptableObject
    {
        public string displayName;

        [TextArea]
        public string description;

        public FX fx;

        [Header("효과 구현")]
        public List<Effect> effects;
    }
}
