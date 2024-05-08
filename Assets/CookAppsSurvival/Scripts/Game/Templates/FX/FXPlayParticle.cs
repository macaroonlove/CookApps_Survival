using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/FX/PlayParticle", fileName = "PlayParticle", order = 0)]
    public class FXPlayParticle : FX
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _offset;

        public override void Play(Unit target, Unit caster = null)
        {
            if (_particle == null) return;
            if (target == null) return;

            target.StartCoroutine(CoPlay(target));
        }

        private IEnumerator CoPlay(Unit target)
        {
            var poolSystem = BattleManager.Instance.GetSubSystem<PoolSystem>();
            
            // 파티클 만들기
            var instance = poolSystem.Spawn(_particle.gameObject);

            // 위치 지정
            instance.transform.position = target.transform.position + _offset;

            // 파티클 재생
            _particle.Play();

            // 지연시간
            yield return new WaitForSeconds(_duration);

            // 파티클 멈추기
            _particle.Stop();

            // 파티클 다시 풀에 넣어두기
            poolSystem.DeSpawn(instance);

        }
    }
}
