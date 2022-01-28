using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct SetterUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var idx_0 = Entities.MasterEs.SetUnit<IdxC>().Idx;
            var unit = Entities.MasterEs.SetUnit<UnitTC>().Unit;


            ref var fire_0 = ref Entities.CellEs.FireEs.Fire(idx_0).Fire;

            ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
            ref var levUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).LevelC;
            ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;

            ref var hp_0 = ref Entities.CellEs.UnitEs.Hp(idx_0).AmountC;
            ref var step_0 = ref Entities.CellEs.UnitEs.Step(idx_0).Steps;

            ref var cond_0 = ref Entities.CellEs.UnitEs.Else(idx_0).ConditionC;

            ref var tw_0 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeaponC;


            var whoseMove = Entities.WhoseMove.WhoseMove.Player;


            if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(whoseMove, idx_0).Can)
            {
                var levUnit = LevelTypes.None;

                if (InventorUnitsE.Units(unit, LevelTypes.Second, whoseMove).Have)
                {
                    InventorUnitsE.Units(unit, LevelTypes.Second, whoseMove).Amount -= 1;
                    levUnit = LevelTypes.Second;
                }
                else
                {
                    InventorUnitsE.Units(unit, LevelTypes.First, whoseMove).Amount -= 1;
                    levUnit = LevelTypes.First;
                }
                Entities.CellEs.UnitEs.SetNew((unit, levUnit, whoseMove, default, default), idx_0);


                //if (unit == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}