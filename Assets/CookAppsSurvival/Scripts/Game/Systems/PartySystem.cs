using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 아군 유닛 파티를 관리하는 시스템
    /// </summary>
    public class PartySystem : MonoBehaviour, ISubSystem
    {
        private List<Transform> _pos = new List<Transform>();

        internal Transform pos 
        {
            get
            {
                foreach(var p in _pos)
                {
                    if (p.gameObject.activeSelf)
                    {
                        return p;
                    }
                }

                // 게임 종료 이벤트 보내주기?(Action)
                return null;
            }
        }

        public void Initialize()
        {
            _pos = transform.GetChild(0).GetComponents<Transform>().ToList();
        }

        public void Deinitialize()
        {
            
        }
    }
}
