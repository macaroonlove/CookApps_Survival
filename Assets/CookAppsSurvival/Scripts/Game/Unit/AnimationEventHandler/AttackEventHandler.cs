using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    public class AttackEventHandler : MonoBehaviour
    {
        internal UnityAction onAttack;

        public void AttackEvent()
        {
            onAttack?.Invoke();
        }
    }
}
