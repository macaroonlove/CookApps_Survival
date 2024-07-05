using FrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ������ �ʿ��� �͵��� �������� ���� ������ �� �ֵ��� �������ִ� Ŭ����
    /// ���� ���� �� ���ᵵ ���� ����
    /// </summary>
    public class BattleManager : Singleton<BattleManager>
    {
        private Dictionary<Type, ISubSystem> _subSystems = new Dictionary<Type, ISubSystem>();

        protected override void Awake()
        {
            Application.targetFrameRate = 120;

            var systems = this.GetComponentsInChildren<ISubSystem>(true);
            foreach (var system in systems)
            {
                _subSystems.Add(system.GetType(), system);
            }

            base.Awake();
        }

        internal void InitializeStage(StageTemplate stage)
        {
            foreach (var system in _subSystems.Values)
            {
                system.Initialize(stage);
            }
        }

        public void Deinitialize()
        {
            foreach (var system in _subSystems.Values)
            {
                system.Deinitialize();
            }
        }

        public T GetSubSystem<T>() where T : ISubSystem
        {
            _subSystems.TryGetValue(typeof(T), out var subSystem);
            return (T)subSystem;
        }
    }
}
