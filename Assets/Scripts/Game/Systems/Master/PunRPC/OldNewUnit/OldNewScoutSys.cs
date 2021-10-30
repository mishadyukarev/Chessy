using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class OldNewScoutSys : IEcsRunSystem
    {
        private EcsFilter<ForOldNewUnitCom> _forOldNewUnitCom = default;

        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilt = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC> _cellUnitOthFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxCell = _forOldNewUnitCom.Get1(0).IdxCell;

            ref var unitCom = ref _cellUnitFilt.Get1(idxCell);
            ref var hpUnitC = ref _cellUnitFilt.Get2(idxCell);
            ref var stepUnitC = ref _cellUnitFilt.Get3(idxCell);

            ref var twUnitC = ref _cellUnitOthFilt.Get3(idxCell);
            ref var effUnitC = ref _cellUnitOthFilt.Get4(idxCell);

            var playerSender = WhoseMoveC.WhoseMove;

            if (hpUnitC.HaveMaxHpUnit(effUnitC, unitCom.UnitType))
            {
                if (stepUnitC.HaveMaxSteps(effUnitC, unitCom.UnitType))
                {
                    InventorUnitsC.TakeUnitsInInv(playerSender, UnitTypes.Scout, LevelUnitTypes.Wood);

                    if (twUnitC.HaveToolWeap) InventorTWCom.AddAmountTools(playerSender, twUnitC.ToolWeapType, twUnitC.LevelTWType);


                    unitCom.UnitType = _forOldNewUnitCom.Get1(0).UnitType;
                    hpUnitC.SetStandMaxHp(effUnitC, unitCom.UnitType);
                    stepUnitC.SetMaxSteps(effUnitC, unitCom.UnitType);
                    _cellUnitOthFilt.Get2(idxCell).DefCondition();
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
        }
    }
}