using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/Environment/StageLibrary", fileName = "StageLibrary", order = 0)]
    public class StageLibraryTemplate : ScriptableObject
    {
        public List<StageTemplate> stage = new List<StageTemplate>();
    }
}