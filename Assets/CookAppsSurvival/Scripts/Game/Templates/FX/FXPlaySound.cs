using FrameWork.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/FX/PlaySound", fileName = "PlaySound", order = 0)]
    public class FXPlaySound : FX
    {
        [SerializeField] private AudioClip _clip;

        public override void Play(Unit target, Unit caster = null)
        {
            if (_clip == null) return;

            SoundManager.PlaySound(_clip);
        }
    }
}
