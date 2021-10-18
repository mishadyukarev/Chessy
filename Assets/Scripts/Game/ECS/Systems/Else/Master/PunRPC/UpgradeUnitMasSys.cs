using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class UpgradeUnitMasSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _fromInfoFilt = default;
        private EcsFilter<ForUpgradeUnitCom> _forUpgradeUnitFilt = default;

        private EcsFilter<CellUnitDataCom> _cellUnitDataFilt = default;

        public void Run()
        {
            var sender = _fromInfoFilt.Get1(0).FromInfo.Sender;
            ref var idxUpgUnit = ref _forUpgradeUnitFilt.Get1(0).idxCellForUpgrade;
            ref var unitDatForUpg = ref _cellUnitDataFilt.Get1(idxUpgUnit);

            if (unitDatForUpg.HaveMaxAmountHealth)
            {
                if (unitDatForUpg.HaveMinAmountSteps)
                {
                    if (unitDatForUpg.UpgradeUnitType == UpgradeUnitTypes.First)
                        unitDatForUpg.UpgradeUnitType = UpgradeUnitTypes.Second;

                    else unitDatForUpg.UpgradeUnitType = UpgradeUnitTypes.First;

                    unitDatForUpg.TakeAmountSteps();

                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.UpgradeUnitMelee);
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