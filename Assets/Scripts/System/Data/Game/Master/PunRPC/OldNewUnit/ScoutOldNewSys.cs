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

            if (CellUnitHpEs.HaveMax(idx_0))
            {
                if (CellUnitStepEs.HaveMaxSteps(idx_0))
                {
                    InventorUnitsE.Units<AmountC>(UnitTypes.Scout, LevelTypes.First, Unit<PlayerTC>(idx_0).Player).Amount -= 1;
                    SetScout(idx_0);

                    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}