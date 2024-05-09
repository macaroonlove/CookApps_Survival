using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// 스킬의 효과들을 관리하는 클래스
    /// </summary>
    public abstract class SkillEffect : Effect
    {
        public abstract List<Unit> GetTarget(PartyUnit unit);

        public abstract void Excute(PartyUnit unit, Unit enemy);
    }
}