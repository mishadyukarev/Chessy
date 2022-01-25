using static Game.Game.CellUnitEntities;

namespace Game.Game
{
    public struct ScoutOldNewSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = EntityMPool.ScoutOldNew<IdxC>().Idx;
            var unit = EntityMPool.ScoutOldNew<UnitTC>().Unit;

            if (CellUnitEntities.Hp(idx_0).HaveMax)
            {
                if (CellUnitEntities.Step(idx_0).AmountC.Amount >= CellUnitEntities.MaxAmountSteps(idx_0))
                {
                    InventorUnitsE.Units(UnitTypes.Scout, LevelTypes.First, CellUnitEntities.Else(idx_0).OwnerC.Player).Amount -= 1;
                    SetScout(idx_0);

                    EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}