using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class UpgradeUnitMasSys : IEcsRunSystem
    {
        private EcsFilter<ForUpgradeUnitCom> _forUpgradeUnitFilt = default;

        private EcsFilter<CellUnitDataCom> _cellUnitDataFilt = default;
        private EcsFilter<InventResourcesC> _invResFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            ref var idxUpgUnit = ref _forUpgradeUnitFilt.Get1(0).idxCellForUpgrade;

            ref var unitDatForUpg = ref _cellUnitDataFilt.Get1(idxUpgUnit);
            ref var invResCom = ref _invResFilt.Get1(0);



            var playSend = WhoseMoveC.WhoseMove;

            if (unitDatForUpg.HaveMaxAmountHealth)
            {
                if (unitDatForUpg.HaveMinAmountSteps)
                {
                    if (InventResourcesC.CanUpgradeUnit(playSend, unitDatForUpg.UnitType, out var needRes))
                    {
                        InventResourcesC.BuyUpgradeUnit(playSend, unitDatForUpg.UnitType);

                        unitDatForUpg.LevelUnitType = LevelUnitTypes.Iron;
                        unitDatForUpg.TakeAmountSteps();

                        unitDatForUpg.AmountHealth = unitDatForUpg.MaxAmountHealth;

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