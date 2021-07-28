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

        LeftBuildUIWorker.AddListener(LeftBuildButtonTypes.BuyPawn, delegate { BuyUnit(UnitTypes.Pawn); });
        LeftBuildUIWorker.AddListener(LeftBuildButtonTypes.BuyRook, delegate { BuyUnit(UnitTypes.Rook); });
        LeftBuildUIWorker.AddListener(LeftBuildButtonTypes.BuyBishop, delegate { BuyUnit(UnitTypes.Bishop); });

        LeftBuildUIWorker.AddListener(LeftBuildButtonTypes.Melt, delegate { MeltOre(); });
        LeftBuildUIWorker.AddListener(LeftBuildButtonTypes.UpgradeUnit, delegate { ToggleUpgradeMod(UpgradeModTypes.Unit); });

        LeftBuildUIWorker.AddListener(LeftBuildButtonTypes.UpgradeFarm, delegate { UpgradeBuilding(BuildingTypes.Farm); });
        LeftBuildUIWorker.AddListener(LeftBuildButtonTypes.UpgradeWoodcutter, delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
        LeftBuildUIWorker.AddListener(LeftBuildButtonTypes.UpgradeMine, delegate { UpgradeBuilding(BuildingTypes.Mine); });
    }

    public void Run()
    {

        if (SelectorWorker.IsSelectedCell && CellBuildingsDataWorker.IsBuildingType(BuildingTypes.City, XySelectedCell))
        {
            if (CellBuildingsDataWorker.HaveOwner(XySelectedCell))
            {
                if (CellBuildingsDataWorker.IsMine(XySelectedCell))
                {
                    LeftBuildUIWorker.SetActiveZone(true);
                }
                else LeftBuildUIWorker.SetActiveZone(false);
            }
        }
        else
        {
            LeftBuildUIWorker.SetActiveZone(false);
        }
    }


    private void BuyUnit(UnitTypes unitType)
    {
        if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.CreateUnitToMaster(unitType);
    }
    private void ToggleUpgradeMod(UpgradeModTypes upgradeModType)
    {
        if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient))
        {
            if (SelectorWorker.IsUpgradeModType(UpgradeModTypes.None))
            {
                SelectorWorker.UpgradeModType = upgradeModType;
            }
            else
            {
                SelectorWorker.ResetUpgradeModType();
            }
        }
    }

    private void UpgradeBuilding(BuildingTypes buildingType)
    {
        if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.UpgradeBuildingToMaster(buildingType);
    }

    private void MeltOre()
    {
        if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.MeltOreToMaster();
    }
}
