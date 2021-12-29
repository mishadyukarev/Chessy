using Leopotam.Ecs;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class ShieldUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var tw_sel = ref UnitTW<ToolWeaponC>(SelIdx<IdxC>().Idx);
            ref var twLevel_sel = ref UnitTW<LevelC>(SelIdx<IdxC>().Idx);

            ExtraTWZoneUIC.DisableAll();

            if (tw_sel.HaveTW)
            {
                ExtraTWZoneUIC.Toggle(tw_sel.ToolWeapon, twLevel_sel.Level, true);
            }
        }
    }
}