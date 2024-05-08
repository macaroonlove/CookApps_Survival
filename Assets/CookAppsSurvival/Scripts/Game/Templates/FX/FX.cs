using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public abstract class FX : ScriptableObject
    {
        public abstract void Play(Unit target, Unit caster = null);
    }
}
