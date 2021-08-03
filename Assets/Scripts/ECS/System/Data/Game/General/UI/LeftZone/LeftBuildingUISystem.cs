using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Assets.Scripts.Workers.Game.UI.Left;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class LeftBuildingUISystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter;
    internal EcsComponentRef<SelectorComponent> SelComRef => _selectorFilter.Get1Ref(0);

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
        ref var selCom = ref _selectorFilter.Get1(0);

        if (selCom.IsSelectedCell && CellBuildDataSystem.BuildTypeCom(selCom.XySelectedCell).Is(BuildingTypes.City))
        {
            if (CellBuildDataSystem.OwnerCom(selCom.XySelectedCell).HaveOwner)
            {
                if (CellBuildDataSystem.OwnerCom(selCom.XySelectedCell).IsMine)
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
            if (SelComRef.Unref().SelectorType == SelectorTypes.UpgradeUnit)
            {
                SelComRef.Unref().SelectorType = SelectorTypes.StartClick;
            }
            else
            {
                SelComRef.Unref().SelectorType = SelectorTypes.UpgradeUnit;
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
