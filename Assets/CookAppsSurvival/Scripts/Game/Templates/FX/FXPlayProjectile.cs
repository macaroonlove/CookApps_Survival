using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/FX/PlayProjectile", fileName = "PlayProjectile", order = 0)]
    public class FXPlayProjectile : FX
    {
        [SerializeField] private GameObject _prefab;

        public override void Play(Unit target, Unit caster = null)
        {
            if (_prefab == null) return;
            if (target == null || caster == null) return;

            var poolSystem = BattleManager.Instance.GetSubSystem<PoolSystem>();

            var projectile = poolSystem.Spawn(_prefab).GetComponent<Projectile>();
            projectile.transform.SetPositionAndRotation(caster.transform.position, Quaternion.identity);
            projectile.Initialize(caster.attackAbility, target);
        }
    }
}
