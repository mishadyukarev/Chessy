using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ShieldUISys : IEcsRunSystem
    {
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            ref var selTwUnitC = ref _twUnitF.Get1(IdxSel.Idx);

            ExtraTWZoneUIC.DisableAll();

            if (selTwUnitC.HaveToolWeap)
            {
                ExtraTWZoneUIC.Toggle(selTwUnitC.ToolWeapType, selTwUnitC.LevelTWType, true);
            }
        }
    }
}