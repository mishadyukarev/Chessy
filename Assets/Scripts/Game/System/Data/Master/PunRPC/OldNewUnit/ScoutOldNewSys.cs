﻿using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class ScoutOldNewSys : IEcsRunSystem
    {
        private EcsFilter<ForOldNewUnitCom> _forOldNewUnitCom = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilt = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC> _cellUnitOthFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = _forOldNewUnitCom.Get1(0).IdxCell;

            ref var unit_0 = ref _cellUnitFilt.Get1(idx_0);
            ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);
            ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);
            ref var hpUnitC = ref _cellUnitFilt.Get2(idx_0);
            ref var stepUnitC = ref _cellUnitFilt.Get3(idx_0);
            ref var twUnitC_0 = ref _cellUnitOthFilt.Get3(idx_0);
            ref var effUnit_0 = ref _cellUnitOthFilt.Get4(idx_0);


            //var whoseMove = WhoseMoveC.WhoseMove;

            if (hpUnitC.HaveMaxHpUnit)
            {
                if (stepUnitC.HaveMaxSteps(effUnit_0, unit_0.Unit, UnitsUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit)))
                {
                    InvUnitsC.TakeUnitsInInv(ownUnit_0.Owner, UnitTypes.Scout, LevelUnitTypes.Wood);
                    WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnitC_0.Level, idx_0);
                    unit_0.NoneUnit();

                    if (twUnitC_0.HaveToolWeap)
                    {
                        InvToolWeapC.AddAmountTools(ownUnit_0.Owner, twUnitC_0.ToolWeapType, twUnitC_0.LevelTWType);
                        twUnitC_0.ToolWeapType = default;
                    }

                    unit_0.SetUnit(_forOldNewUnitCom.Get1(0).UnitType);
                    levUnitC_0.SetLevel(LevelUnitTypes.Wood);

                    hpUnitC.SetMaxHp();
                    stepUnitC.SetMaxSteps(effUnit_0, unit_0.Unit, UnitsUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit));
                    _cellUnitOthFilt.Get2(idx_0).DefCondition();
                    WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnitC_0.Level, idx_0);

                    RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
        }
    }
}