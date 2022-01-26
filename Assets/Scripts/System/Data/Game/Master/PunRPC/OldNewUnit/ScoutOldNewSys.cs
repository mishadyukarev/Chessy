using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct ScoutOldNewSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = EntitiesMaster.ScoutOldNew<IdxC>().Idx;
            var unit = EntitiesMaster.ScoutOldNew<UnitTC>().Unit;

            if (CellUnitEs.Hp(idx_0).HaveMax)
            {
                if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitEs.MaxAmountSteps(idx_0))
                {
                    InventorUnitsE.Units(UnitTypes.Scout, LevelTypes.First, CellUnitEs.Else(idx_0).OwnerC.Player).Amount -= 1;
                    SetScout(idx_0);

                    Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}