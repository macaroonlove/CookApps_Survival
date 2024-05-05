using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/AbnormalStatus", fileName = "AbnormalStatus", order = 0)]
    public class AbnormalStatusTemplate : ScriptableObject
    {
        public string displayName;

        [TextArea]
        public string description;

        [Header("효과 구현")]
        public List<Effect> effects;
    }
}
