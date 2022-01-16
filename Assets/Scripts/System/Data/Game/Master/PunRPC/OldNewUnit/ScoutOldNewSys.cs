using static Game.Game.CellUnitEs;

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
                    EntInventorUnits.Units<AmountC>(UnitTypes.Scout, LevelTypes.First, Unit<PlayerTC>(idx_0).Player).Amount -= 1;
                    Unit<UnitCellEC>(idx_0).SetScout();

                    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}