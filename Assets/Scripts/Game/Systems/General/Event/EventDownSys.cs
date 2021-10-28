using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class EventDownSys : IEcsInitSystem
    {
        public void Init()
        {
            HeroZoneUIC.AddListScout(ExecuteScout);
        }

        private void ExecuteScout()
        {
            SelectorC.CellClickType = CellClickTypes.OldToNewUnit;
            SelectorC.UnitTypeOldToNew = UnitTypes.Scout;
        }
    }
}