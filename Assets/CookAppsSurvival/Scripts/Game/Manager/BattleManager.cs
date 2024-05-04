using FrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 전투에 필요한 것들을 전역으로 쉽게 접근할 수 있도록 지원해주는 클래스
    /// 전투 시작 및 종료도 조작 가능
    /// </summary>
    public class BattleManager : Singleton<BattleManager>
    {
        private Dictionary<Type, ISubSystem> _subSystems = new Dictionary<Type, ISubSystem>();

        protected override void Awake()
        {
            var systems = this.GetComponentsInChildren<ISubSystem>(true);
            foreach (var system in systems)
            {
                _subSystems.Add(system.GetType(), system);
            }

            base.Awake();
        }

        protected override void Initialize()
        {
            foreach (var system in _subSystems.Values)
            {
                system.Initialize();
            }
        }

        public void Deinitialize()
        {
            foreach (var item in _subSystems.Values)
            {
                item.Deinitialize();
            }
        }

        public T GetSubSystem<T>() where T : ISubSystem
        {
            _subSystems.TryGetValue(typeof(T), out var subSystem);
            return (T)subSystem;
        }
    }
}
