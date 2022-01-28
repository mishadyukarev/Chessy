using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct SetterUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var idx_0 = EntitiesMaster.SetUnit<IdxC>().Idx;
            var unit = EntitiesMaster.SetUnit<UnitTC>().Unit;


            ref var fire_0 = ref CellFireEs.Fire(idx_0).Fire;

            ref var unit_0 = ref Else(idx_0).UnitC;
            ref var levUnit_0 = ref CellUnitEs.Else(idx_0).LevelC;
            ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;

            ref var hp_0 = ref CellUnitEs.Hp(idx_0).AmountC;
            ref var step_0 = ref CellUnitEs.Step(idx_0).AmountC;

            ref var cond_0 = ref CellUnitEs.Else(idx_0).ConditionC;

            ref var tw_0 = ref CellUnitEs.ToolWeapon(idx_0).ToolWeaponC;


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
                SetNew((unit, levUnit, whoseMove, default, default), idx_0);


                //if (unit == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                Entities.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}