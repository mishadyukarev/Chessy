using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class UpgradeUnitMasSys : IEcsRunSystem
    {
        private EcsFilter<ForUpgradeUnitCom> _forUpgradeUnitFilt = default;

        private EcsFilter<CellUnitDataCom, HpComponent, StepComponent> _cellUnitDataFilt = default;
        private EcsFilter<InventResourcesC> _invResFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            ref var idxUpgUnit = ref _forUpgradeUnitFilt.Get1(0).idxCellForUpgrade;

            ref var unitDatForUpg = ref _cellUnitDataFilt.Get1(idxUpgUnit);
            ref var stepUnitC_0 = ref _cellUnitDataFilt.Get3(idxUpgUnit);

            ref var hpUnitC = ref _cellUnitDataFilt.Get2(idxUpgUnit);
            ref var invResCom = ref _invResFilt.Get1(0);



            var playSend = WhoseMoveC.WhoseMove;

            if (hpUnitC.HaveMaxAmountHealth(unitDatForUpg.UnitType))
            {
                if (stepUnitC_0.HaveMinAmountSteps)
                {
                    if (InventResourcesC.CanUpgradeUnit(playSend, unitDatForUpg.UnitType, out var needRes))
                    {
                        InventResourcesC.BuyUpgradeUnit(playSend, unitDatForUpg.UnitType);

                        unitDatForUpg.LevelUnitType = LevelUnitTypes.Iron;
                        stepUnitC_0.TakeAmountSteps();

                        hpUnitC.AmountHealth = hpUnitC.MaxAmountHealth(unitDatForUpg.UnitType);

                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.UpgradeUnitMelee);
                    }
                    else
                    {
                        RpcSys.MistakeEconomyToGeneral(sender, needRes);
                    }
                }
                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
            }
        }
    }
}