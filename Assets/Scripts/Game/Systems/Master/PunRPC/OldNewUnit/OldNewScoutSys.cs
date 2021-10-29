using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class OldNewScoutSys : IEcsRunSystem
    {
        private EcsFilter<ForOldNewUnitCom> _forOldNewUnitCom = default;

        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent, ConditionUnitC, ToolWeaponC> _cellUnitFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxCell = _forOldNewUnitCom.Get1(0).IdxCell;

            ref var unitCom = ref _cellUnitFilt.Get1(idxCell);
            ref var hpUnitC = ref _cellUnitFilt.Get2(idxCell);
            ref var stepUnitC = ref _cellUnitFilt.Get3(idxCell);
            ref var twUnitC = ref _cellUnitFilt.Get5(idxCell);

            var playerSender = WhoseMoveC.WhoseMove;

            if (hpUnitC.HaveHp)
            {
                if (stepUnitC.HaveMinSteps)
                {
                    InventorUnitsC.TakeUnitsInInv(playerSender, UnitTypes.Scout, LevelUnitTypes.Wood);

                    if (twUnitC.HaveToolWeap) InventorTWCom.AddAmountTools(playerSender, twUnitC.ToolWeapType, twUnitC.LevelTWType);


                    unitCom.UnitType = _forOldNewUnitCom.Get1(0).UnitType;
                    hpUnitC.SetStandMaxHp(unitCom.UnitType);
                    stepUnitC.SetMaxSteps(unitCom.UnitType);
                    _cellUnitFilt.Get4(idxCell).DefCondition();
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
        }
    }
}