using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class LeftCityEventUISys : IEcsRunSystem
    {
        public void Run()
        {
            BuildLeftZoneViewUICom.AddListenerToMelt(delegate { MeltOre(); });

            BuildLeftZoneViewUICom.AddListBuildUpgrade(BuildTypes.Farm, delegate { UpgradeBuilding(BuildTypes.Farm); });
            BuildLeftZoneViewUICom.AddListBuildUpgrade(BuildTypes.Woodcutter, delegate { UpgradeBuilding(BuildTypes.Woodcutter); });
            BuildLeftZoneViewUICom.AddListBuildUpgrade(BuildTypes.Mine, delegate { UpgradeBuilding(BuildTypes.Mine); });
        }

        private void MeltOre()
        {
            if (WhoseMoveC.IsMyMove) RpcSys.MeltOreToMaster();
        }

        private void UpgradeBuilding(BuildTypes buildingType)
        {
            if (WhoseMoveC.IsMyMove) RpcSys.UpgradeBuildingToMaster(buildingType);
        }
    }
}