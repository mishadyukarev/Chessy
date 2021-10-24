using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class EventDownSys : IEcsInitSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;

        private EcsFilter<HeroZoneUICom> _heroZoneFilt = default;

        public void Init()
        {
            _heroZoneFilt.Get1(0).AddListScout(ExecuteScout);
        }

        private void ExecuteScout()
        {
            _selFilt.Get1(0).CellClickType = CellClickTypes.OldToNewUnit;
            _selFilt.Get1(0).UnitTypeOldToNew = UnitTypes.Scout;
        }
    }
}