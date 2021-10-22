using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class UpgradeUnitMasSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _fromInfoFilt = default;
        private EcsFilter<ForUpgradeUnitCom> _forUpgradeUnitFilt = default;

        private EcsFilter<CellUnitDataCom> _cellUnitDataFilt = default;
        private EcsFilter<InventResourCom> _invResFilt = default;

        public void Run()
        {
            var sender = _fromInfoFilt.Get1(0).FromInfo.Sender;
            ref var idxUpgUnit = ref _forUpgradeUnitFilt.Get1(0).idxCellForUpgrade;

            ref var unitDatForUpg = ref _cellUnitDataFilt.Get1(idxUpgUnit);
            ref var invResCom = ref _invResFilt.Get1(0);



            PlayerTypes playSend = default;
            if (GameModesCom.IsOfflineMode) playSend = WhoseMoveCom.WhoseMoveOffline;
            else playSend = sender.GetPlayerType();

            if (unitDatForUpg.HaveMaxAmountHealth)
            {
                if (unitDatForUpg.HaveMinAmountSteps)
                {
                    if(invResCom.CanUpgradeUnit(playSend, unitDatForUpg.UnitType, out var needRes))
                    {
                        invResCom.BuyUpgradeUnit(playSend, unitDatForUpg.UnitType);

                        unitDatForUpg.LevelUnitType = LevelUnitTypes.Iron;
                        unitDatForUpg.TakeAmountSteps();

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