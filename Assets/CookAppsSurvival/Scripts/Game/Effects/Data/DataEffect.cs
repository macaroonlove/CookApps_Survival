using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class DataEffect<T> : Effect
    {
        [SerializeField] private T _value;

        public T value => _value;

#if UNITY_EDITOR
        public override void Draw(Rect rect)
        {

        }
#endif

        public override string GetLabel()
        {
            return "";
        }
    }
}