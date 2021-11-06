﻿using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class SetterUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;

        private EcsFilter<CellEnvDataC> _cellEnvirDataFilter = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, WaterUnitC> _cellUnitOthFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unitForSet = _setterFilter.Get1(0).UnitTypeForSetting;
            var idx_0 = _setterFilter.Get1(0).IdxCellForSetting;

            ref var curEnvDatCom = ref _cellEnvirDataFilter.Get1(idx_0);

            ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);

            ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);
            ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);

            ref var curHpUnitC = ref _cellUnitFilter.Get2(idx_0);
            ref var stepUnitC = ref _cellUnitFilter.Get3(idx_0);

            ref var condUnit_0 = ref _cellUnitOthFilt.Get2(idx_0);
            ref var curTwUnitC = ref _cellUnitOthFilt.Get3(idx_0);
            ref var effUnit_0 = ref _cellUnitOthFilt.Get4(idx_0);
            ref var thirUnitC_0 =ref _cellUnitOthFilt.Get5(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (CellsForSetUnitC.HaveIdxCell(whoseMove, idx_0))
            {
                unit_0.SetUnit(unitForSet);
                ownUnit_0.SetOwner(whoseMove);
                curTwUnitC.ToolWeapType = default;
                effUnit_0.DefAllEffects();
                curHpUnitC.SetMaxHp();
                stepUnitC.SetMaxSteps(unitForSet, false, UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit));
                if(condUnit_0.HaveCondition) condUnit_0.Def();
                thirUnitC_0.SetMaxWater(UnitPercUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));
                if (InvUnitsC.HaveUnitInInv(whoseMove, unitForSet, LevelUnitTypes.Iron))
                {
                    InvUnitsC.TakeUnitsInInv(whoseMove, unitForSet, LevelUnitTypes.Iron);
                    levUnitC_0.SetLevel(LevelUnitTypes.Iron);
                }
                else
                {
                    InvUnitsC.TakeUnitsInInv(whoseMove, unitForSet, LevelUnitTypes.Wood);
                    levUnitC_0.SetLevel(LevelUnitTypes.Wood);
                }
                WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnitC_0.Level, idx_0);

                if (unitForSet == UnitTypes.King) PickUpgZoneDataUIC.SetHaveUpgrade(whoseMove, true);

                RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);
            }
        }
    }
}