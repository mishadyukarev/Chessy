using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class ShieldUISys : IEcsRunSystem
    {
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            ref var selTwUnitC = ref _twUnitF.Get1(SelIdx.Idx);

            ExtraTWZoneUIC.DisableAll();

            if (selTwUnitC.HaveTW)
            {
                ExtraTWZoneUIC.Toggle(selTwUnitC.TW, selTwUnitC.Level, true);
            }
        }
    }
}