﻿using static Game.Game.CellEs;
using static Game.Game.CellUnitEntities;
using static Game.Game.CellFireEs;
using static Game.Game.CellUnitTWE;

namespace Game.Game
{
    struct SetterUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var idx_0 = EntityMPool.SetUnit<IdxC>().Idx;
            var unit = EntityMPool.SetUnit<UnitTC>().Unit;


            ref var fire_0 = ref Fire<HaveEffectC>(idx_0);

            ref var unit_0 = ref Else(idx_0).UnitC;
            ref var levUnit_0 = ref CellUnitEntities.Else(idx_0).LevelC;
            ref var ownUnit_0 = ref CellUnitEntities.Else(idx_0).OwnerC;

            ref var hp_0 = ref CellUnitEntities.Hp(idx_0).AmountC;
            ref var step_0 = ref CellUnitEntities.Step(idx_0).AmountC;

            ref var cond_0 = ref CellUnitEntities.Else(idx_0).ConditionC;

            ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);


            var whoseMove = WhoseMoveE.WhoseMove.Player;


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

                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}