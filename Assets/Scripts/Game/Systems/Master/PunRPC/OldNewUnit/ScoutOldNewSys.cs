using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class ScoutOldNewSys : IEcsRunSystem
    {
        private EcsFilter<ForOldNewUnitCom> _forOldNewUnitCom = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilt = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC> _cellUnitOthFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idx_0 = _forOldNewUnitCom.Get1(0).IdxCell;

            ref var unitC_0 = ref _cellUnitFilt.Get1(idx_0);

            ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);

            ref var hpUnitC = ref _cellUnitFilt.Get2(idx_0);
            ref var stepUnitC = ref _cellUnitFilt.Get3(idx_0);

            ref var twUnitC = ref _cellUnitOthFilt.Get3(idx_0);
            ref var effUnitC = ref _cellUnitOthFilt.Get4(idx_0);


            var playerSender = WhoseMoveC.WhoseMove;

            if (hpUnitC.HaveMaxHpUnit(effUnitC, unitC_0.UnitType))
            {
                if (stepUnitC.HaveMaxSteps(effUnitC, unitC_0.UnitType))
                {
                    InventorUnitsC.TakeUnitsInInv(playerSender, UnitTypes.Scout, LevelUnitTypes.Wood);

                    if (twUnitC.HaveToolWeap) InventorTWCom.AddAmountTools(playerSender, twUnitC.ToolWeapType, twUnitC.LevelTWType);


                    unitC_0.SetUnit(_forOldNewUnitCom.Get1(0).UnitType);
                    levUnitC_0.SetNewLevel(LevelUnitTypes.Wood);

                    hpUnitC.SetStandMaxHp(effUnitC, unitC_0.UnitType);
                    stepUnitC.SetMaxSteps(effUnitC, unitC_0.UnitType);
                    _cellUnitOthFilt.Get2(idx_0).DefCondition();
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
        }
    }
}