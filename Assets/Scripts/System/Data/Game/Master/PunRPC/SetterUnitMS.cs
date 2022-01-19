﻿using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
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

            ref var unit_0 = ref Unit<UnitTC>(idx_0);
            ref var levUnit_0 = ref Unit<LevelTC>(idx_0);
            ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);

            ref var hp_0 = ref CellUnitHpEs.Hp<AmountC>(idx_0);
            ref var step_0 = ref CellUnitStepEs.Steps<AmountC>(idx_0);
            ref var water_0 = ref CellUnitWaterEs.Water<AmountC>(idx_0);

            ref var cond_0 = ref Unit<ConditionUnitC>(idx_0);

            ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);


            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;


            if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(whoseMove, idx_0).Can)
            {
                var levUnit = LevelTypes.None;

                if (InventorUnitsE.Units<AmountC>(unit, LevelTypes.Second, whoseMove).Have)
                {
                    InventorUnitsE.Units<AmountC>(unit, LevelTypes.Second, whoseMove).Amount -= 1;
                    levUnit = LevelTypes.Second;
                }
                else
                {
                    InventorUnitsE.Units<AmountC>(unit, LevelTypes.First, whoseMove).Amount -= 1;
                    levUnit = LevelTypes.First;
                }
                SetNew((unit, levUnit, whoseMove, default, default), idx_0);


                //if (unit == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}