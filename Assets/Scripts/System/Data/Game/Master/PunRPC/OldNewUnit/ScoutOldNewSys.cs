using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct ScoutOldNewSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);
            UnitDoingMC.Get(out var unit);

            ref var hpUnitCell_0 = ref Unit<UnitCellEC>(idx_0);

            if (CellUnitHpEs.HaveMax(idx_0))
            {
                if (CellUnitStepEs.HaveMaxSteps(idx_0))
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