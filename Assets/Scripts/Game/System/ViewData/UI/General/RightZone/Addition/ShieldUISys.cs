using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class ShieldUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, ToolWeaponC> _cellUnitFilt = default;

        public void Run()
        {
            ref var selTwUnitC = ref _cellUnitFilt.Get2(SelectorC.IdxSelCell);

            ExtraTWZoneUIC.DisableAll();

            if (selTwUnitC.HaveToolWeap)
            {
                ExtraTWZoneUIC.Toggle(selTwUnitC.ToolWeapType, selTwUnitC.LevelTWType, true);
            }
        }
    }
}