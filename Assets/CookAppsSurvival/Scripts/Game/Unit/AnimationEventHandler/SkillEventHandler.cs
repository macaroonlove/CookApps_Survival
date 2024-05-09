using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    public class SkillEventHandler : MonoBehaviour
    {
        internal UnityAction onSkill;

        public void SkillEvent()
        {
            onSkill?.Invoke();
        }
    }
}
