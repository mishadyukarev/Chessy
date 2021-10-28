using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal class UpgradeMasterSystem : IEcsRunSystem
    {
        private EcsFilter<ForUpgradeMasCom> _forUpgradeFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            var forUpgradeCom = _forUpgradeFilter.Get1(0);

            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxForUpgradeUnit = forUpgradeCom.IdxForUpgradeUnit;

            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxForUpgradeUnit);
            ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(idxForUpgradeUnit);


            var playerSend = WhoseMoveC.WhoseMove;


            var buildTypeForUpgrade = _forUpgradeFilter.Get1(0).BuildingType;

            if (InventResourcesC.CanUpgradeBuildings(playerSend, buildTypeForUpgrade, out var needRes))
            {
                InventResourcesC.BuyUpgradeBuildings(playerSend, buildTypeForUpgrade);
                UpgBuildsC.AddAmountUpgrades(playerSend, buildTypeForUpgrade);

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.SoundGoldPack);
            }
            else
            {
                RpcSys.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}
