using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class ShieldUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var tw_sel = ref EntityPool.TWCellC<ToolWeaponC>(SelIdx.Idx);
            ref var twLevel_sel = ref EntityPool.TWCellC<LevelC>(SelIdx.Idx);

            ExtraTWZoneUIC.DisableAll();

            if (tw_sel.HaveTW)
            {
                ExtraTWZoneUIC.Toggle(tw_sel.TW, twLevel_sel.Level, true);
            }
        }
    }
}