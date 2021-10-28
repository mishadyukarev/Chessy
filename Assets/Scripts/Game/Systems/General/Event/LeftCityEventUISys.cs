using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class LeftCityEventUISys : IEcsRunSystem
    {
        public void Run()
        {
            BuildLeftZoneViewUICom.AddListenerToMelt(delegate { MeltOre(); });

            BuildLeftZoneViewUICom.AddListBuildUpgrade(BuildingTypes.Farm, delegate { UpgradeBuilding(BuildingTypes.Farm); });
            BuildLeftZoneViewUICom.AddListBuildUpgrade(BuildingTypes.Woodcutter, delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
            BuildLeftZoneViewUICom.AddListBuildUpgrade(BuildingTypes.Mine, delegate { UpgradeBuilding(BuildingTypes.Mine); });
        }

        private void MeltOre()
        {
            if (WhoseMoveC.IsMyMove) RpcSys.MeltOreToMaster();
        }

        private void UpgradeBuilding(BuildingTypes buildingType)
        {
            if (WhoseMoveC.IsMyMove) RpcSys.UpgradeBuildingToMaster(buildingType);
        }
    }
}