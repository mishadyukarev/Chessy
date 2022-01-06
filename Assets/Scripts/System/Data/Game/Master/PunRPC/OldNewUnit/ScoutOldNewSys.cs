using Leopotam.Ecs;
using Game.Common;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class ScoutOldNewSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);
            UnitDoingMC.Get(out var unit);

            ref var hpUnitCell_0 = ref Unit<UnitCellEC>(idx_0);

            if (hpUnitCell_0.HaveMax)
            {
                if (Unit<UnitCellEC>(idx_0).HaveMaxSteps)
                {
                    InvUnitsC.Take(Unit<OwnerC>(idx_0).Owner, UnitTypes.Scout, LevelTypes.First);
                    Unit<UnitCellEC>(idx_0).SetScout();

                    RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}