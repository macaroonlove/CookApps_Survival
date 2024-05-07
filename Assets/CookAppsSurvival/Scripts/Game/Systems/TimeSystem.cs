using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 시간을 관리하는 시스템
    /// </summary>
    public class TimeSystem : MonoBehaviour, ISubSystem
    {
        [SerializeField, ReadOnly] private ESpeed _speed;
        [SerializeField, ReadOnly] private bool _isPause;

        internal ESpeed speed => _speed;
        internal bool isPause => _isPause;

        public void Initialize(StageTemplate stage)
        {
            _speed = ESpeed.None;
            _isPause = false;
            Time.timeScale = 1;
        }

        public void Deinitialize()
        {

        }

        internal void Pause()
        {
            _isPause = !_isPause;
            
            if (_isPause)
            {
                Time.timeScale = 0;
            }
            else
            {
                ApplyTimeScale();
            }
        }

        internal bool ChangeSpeed()
        {
            if (_isPause) return false;

            switch (_speed)
            {
                case ESpeed.None:
                    _speed = ESpeed.Speedx2;
                    break;
                case ESpeed.Speedx2:
                    _speed = ESpeed.Speedx3;
                    break;
                case ESpeed.Speedx3:
                    _speed = ESpeed.Speedx4;
                    break;
                case ESpeed.Speedx4:
                    _speed = ESpeed.None;
                    break;
            }
            ApplyTimeScale();

            return true;
        }

        private void ApplyTimeScale()
        {
            switch (_speed)
            {
                case ESpeed.None:
                    Time.timeScale = 1;
                    break;
                case ESpeed.Speedx2:
                    Time.timeScale = 2;
                    break;
                case ESpeed.Speedx3:
                    Time.timeScale = 3;
                    break;
                case ESpeed.Speedx4:
                    Time.timeScale = 4;
                    break;
            }
        }
    }
}
