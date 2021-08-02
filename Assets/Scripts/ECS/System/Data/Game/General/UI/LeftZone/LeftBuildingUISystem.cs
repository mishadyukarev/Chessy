using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Assets.Scripts.Workers.Game.UI.Left;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class LeftBuildingUISystem : IEcsInitSystem, IEcsRunSystem
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    public void Init()
    {

        LeftBuildUIViewContainer.AddListener(LeftBuildButtonTypes.BuyPawn, delegate { BuyUnit(UnitTypes.Pawn); });
        LeftBuildUIViewContainer.AddListener(LeftBuildButtonTypes.BuyRook, delegate { BuyUnit(UnitTypes.Rook); });
        LeftBuildUIViewContainer.AddListener(LeftBuildButtonTypes.BuyBishop, delegate { BuyUnit(UnitTypes.Bishop); });

        LeftBuildUIViewContainer.AddListener(LeftBuildButtonTypes.Melt, delegate { MeltOre(); });
        LeftBuildUIViewContainer.AddListener(LeftBuildButtonTypes.UpgradeUnit, ToggleSelectorUpgradeUnit);

        LeftBuildUIViewContainer.AddListener(LeftBuildButtonTypes.UpgradeFarm, delegate { UpgradeBuilding(BuildingTypes.Farm); });
        LeftBuildUIViewContainer.AddListener(LeftBuildButtonTypes.UpgradeWoodcutter, delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
        LeftBuildUIViewContainer.AddListener(LeftBuildButtonTypes.UpgradeMine, delegate { UpgradeBuilding(BuildingTypes.Mine); });
    }

    public void Run()
    {

        if (SelectorWorker.IsSelectedCell && CellBuildDataContainer.IsBuildingType(BuildingTypes.City, XySelectedCell))
        {
            if (CellBuildDataContainer.HaveOwner(XySelectedCell))
            {
                if (CellBuildDataContainer.IsMine(XySelectedCell))
                {
                    LeftBuildUIViewContainer.SetActiveZone(true);
                }
                else LeftBuildUIViewContainer.SetActiveZone(false);
            }
        }
        else
        {
            LeftBuildUIViewContainer.SetActiveZone(false);
        }
    }


    private void BuyUnit(UnitTypes unitType)
    {
        if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.CreateUnitToMaster(unitType);
    }
    private void ToggleSelectorUpgradeUnit()
    {
        if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
        {
            if (SelectorWorker.SelectorType == SelectorTypes.UpgradeUnit)
            {
                SelectorWorker.SelectorType = SelectorTypes.Other;
            }
            else
            {
                SelectorWorker.SelectorType = SelectorTypes.UpgradeUnit;
            }
        }
    }

    private void UpgradeBuilding(BuildingTypes buildingType)
    {
        if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.UpgradeBuildingToMaster(buildingType);
    }

    private void MeltOre()
    {
        if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.MeltOreToMaster();
    }
}
