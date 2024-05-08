using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/FX/FXLibrary", fileName = "FXLibrary", order = 0)]
    public class FXLibrary : FX
    {
        [SerializeField] List<FX> fxs = new List<FX>();

        public override void Play(Unit target, Unit caster = null)
        {
            foreach (var fx in fxs) 
            {
                if (fx == null) continue;

                fx.Play(target, caster);
            }
        }
    }
}
