namespace Game.Game
{
    public struct ScoutOldNewSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = Entities.MasterEs.ScoutOldNew<IdxC>().Idx;
            var unit = Entities.MasterEs.ScoutOldNew<UnitTC>().Unit;

            if (Entities.CellEs.UnitEs.Hp(idx_0).HaveMax)
            {
                if (Entities.CellEs.UnitEs.Step(idx_0).HaveMax(Entities.CellEs.UnitEs.Else(idx_0)))
                {
                    InventorUnitsE.Units(UnitTypes.Scout, LevelTypes.First, Entities.CellEs.UnitEs.Else(idx_0).OwnerC.Player).Amount -= 1;
                    Entities.CellEs.UnitEs.SetScout(idx_0);

                    Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}