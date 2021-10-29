using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class OldNewScoutSys : IEcsRunSystem
    {
        private EcsFilter<ForOldNewUnitCom> _forOldNewUnitCom = default;

        private EcsFilter<CellUnitDataCom, HpComponent, StepComponent, ToolWeaponC> _cellUnitFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxCell = _forOldNewUnitCom.Get1(0).IdxCell;

            ref var unitCom = ref _cellUnitFilt.Get1(idxCell);
            ref var hpUnitC = ref _cellUnitFilt.Get2(idxCell);
            ref var stepUnitC = ref _cellUnitFilt.Get3(idxCell);
            ref var twUnitC = ref _cellUnitFilt.Get4(idxCell);

            var playerSender = WhoseMoveC.WhoseMove;

            if (hpUnitC.HaveAmountHealth)
            {
                if (stepUnitC.HaveMinAmountSteps)
                {
                    InventorUnitsC.TakeUnitsInInv(playerSender, UnitTypes.Scout, LevelUnitTypes.Wood);

                    if (twUnitC.HaveExtraTW) InventorTWCom.AddAmountTools(playerSender, twUnitC.TWExtraType, twUnitC.LevelTWType);


                    unitCom.UnitType = _forOldNewUnitCom.Get1(0).UnitType;
                    hpUnitC.SetMaxAmountHealth(unitCom.UnitType);
                    stepUnitC.SetMaxAmountSteps(unitCom.UnitType);
                    unitCom.DefCondType();
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
        }
    }
}