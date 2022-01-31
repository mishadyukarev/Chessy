namespace Game.Game
{
    sealed class SetterUnitMS : SystemAbstract, IEcsRunSystem
    {
        public SetterUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var unitEs = UnitEs;


            var sender = InfoC.Sender(MGOTypes.Master);

            var idx_0 = Es.MasterEs.SetUnit<IdxC>().Idx;
            var unit = Es.MasterEs.SetUnit<UnitTC>().Unit;

            var whoseMove = Es.WhoseMove.WhoseMove.Player;


            if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(whoseMove, idx_0).Can)
            {
                var levUnit = LevelTypes.None;

                if (Es.InventorUnitsEs.Units(unit, LevelTypes.Second, whoseMove).Units.Have)
                {
                    Es.InventorUnitsEs.Units(unit, LevelTypes.Second, whoseMove).Units.Amount -= 1;
                    levUnit = LevelTypes.Second;
                }
                else
                {
                    Es.InventorUnitsEs.Units(unit, LevelTypes.First, whoseMove).Units.Amount -= 1;
                    levUnit = LevelTypes.First;
                }
                unitEs.Main(idx_0).SetNew((unit, levUnit, whoseMove, ConditionUnitTypes.None, false), Es);


                //if (unit == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}