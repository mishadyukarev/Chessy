using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using System;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForUpgradeMasCom> _forUpgradeFilter = default;

        private EcsFilter<UpgradesBuildingsComponent> _upgradeBuildsFilter = default;
        private EcsFilter<InventorResourcesComponent> _inventResFilt = default;

        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

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



            bool[] haves;


            var buildTypeForUpgrade = _forUpgradeFilter.Get1(0).BuildingType;

            if (inventResCom.CanUpgradeBuildings(sender, buildTypeForUpgrade, out haves))
            {
                inventResCom.BuyUpgradeBuildings(sender, buildTypeForUpgrade);
                _upgradeBuildsFilter.Get1(0).AddAmountUpgrades(buildTypeForUpgrade, sender.IsMasterClient);

                RpcGeneralSystem.SoundToGeneral(sender, SoundEffectTypes.SoundGoldPack);
            }
            else
            {
                RpcGeneralSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                RpcGeneralSystem.MistakeEconomyToGeneral(sender, haves);
            }
        }
    }
}
