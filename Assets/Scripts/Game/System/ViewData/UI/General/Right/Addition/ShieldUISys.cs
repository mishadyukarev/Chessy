using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class ShieldUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var tw_sel = ref EntityPool.UnitToolWeapon<ToolWeaponC>(SelIdx.Idx);
            ref var twLevel_sel = ref EntityPool.UnitToolWeapon<LevelC>(SelIdx.Idx);

            ExtraTWZoneUIC.DisableAll();

            if (tw_sel.HaveTW)
            {
                ExtraTWZoneUIC.Toggle(tw_sel.ToolWeapon, twLevel_sel.Level, true);
            }
        }
    }
}