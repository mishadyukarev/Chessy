using static Game.Game.EntCellUnit;

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
                    InvUnitsC.Take(Unit<PlayerC>(idx_0).Player, UnitTypes.Scout, LevelTypes.First);
                    Unit<UnitCellEC>(idx_0).SetScout();

                    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}