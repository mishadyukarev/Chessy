using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Supports;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForUpgradeMasCom> _forUpgradeFilter = default;

        private EcsFilter<UpgradesBuildsCom> _upgradeBuildsFilter = default;
        private EcsFilter<InventResourCom> _inventResFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        private const byte FOR_NEXT_UPGRADE = 1;

        public void Run()
        {
            ref var infoCom = ref _infoFilter.Get1(0);
            var forUpgradeCom = _forUpgradeFilter.Get1(0);

            var sender = infoCom.FromInfo.Sender;
            var idxForUpgradeUnit = forUpgradeCom.IdxForUpgradeUnit;

            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxForUpgradeUnit);
            ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(idxForUpgradeUnit);

            ref var inventResCom = ref _inventResFilt.Get1(0);



            PlayerTypes playerSender = default;
            if (GameModesCom.IsOfflineMode) playerSender = WhoseMoveCom.WhoseMoveOffline;
            else playerSender = sender.GetPlayerType();

            var buildTypeForUpgrade = _forUpgradeFilter.Get1(0).BuildingType;

            if (inventResCom.CanUpgradeBuildings(playerSender, buildTypeForUpgrade, out var haves))
            {
                inventResCom.BuyUpgradeBuildings(playerSender, buildTypeForUpgrade);
                _upgradeBuildsFilter.Get1(0).AddAmountUpgrades(playerSender, buildTypeForUpgrade);

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.SoundGoldPack);
            }
            else
            {
                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                RpcSys.MistakeEconomyToGeneral(sender, haves);
            }
        }
    }
}
