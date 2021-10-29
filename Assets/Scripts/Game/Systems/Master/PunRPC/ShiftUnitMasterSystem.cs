using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class ShiftUnitMasterSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, HpComponent, StepComponent, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvrDataFilter = default;

        public void Run()
        {
            var fromIdx = ForShiftMasCom.IdxFrom;
            var toIdx = ForShiftMasCom.IdxTo;

            var playerType = WhoseMoveC.WhoseMove;


            if (CellsForShiftCom.HaveIdxCell(playerType, fromIdx, toIdx))
            {
                ref var fromUnitDatCom = ref _cellUnitFilter.Get1(fromIdx);
                ref var hpUnitC_from = ref _cellUnitFilter.Get2(fromIdx);
                ref var stepUnitC_from = ref _cellUnitFilter.Get3(fromIdx);
                ref var fromOnlineUnitCom = ref _cellUnitFilter.Get4(fromIdx);

                ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdx);
                ref var hpUnitC_to = ref _cellUnitFilter.Get2(toIdx);
                ref var stepUnitC_to = ref _cellUnitFilter.Get3(toIdx);
                ref var toOwnUnitCom = ref _cellUnitFilter.Get4(toIdx);
                ref var toEnvDatCom = ref _cellEnvrDataFilter.Get1(toIdx);



                if (fromUnitDatCom.Have(StatTypes.Steps) && toEnvDatCom.NeedAmountSteps < 2)
                {
                    fromUnitDatCom.Set(StatTypes.Steps, false);
                }
                else
                {
                    stepUnitC_from.TakeAmountSteps(toEnvDatCom.NeedAmountSteps);
                    if (stepUnitC_from.AmountSteps < 0) stepUnitC_from.DefAmountSteps();
                }
                

                toUnitDatCom.ReplaceUnit(fromUnitDatCom);
                hpUnitC_to.AmountHealth = hpUnitC_from.AmountHealth;
                stepUnitC_to.AmountSteps = stepUnitC_from.AmountSteps;
                toUnitDatCom.CondUnitType = default;
                toOwnUnitCom.PlayerType = playerType;

                fromUnitDatCom.DefUnitType();

                RpcSys.SoundToGeneral(InfoC.Sender(MasGenOthTypes.Master), SoundEffectTypes.ClickToTable);
            }
        }
    }
}