using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System;

internal sealed class BuildingUISystem : IEcsInitSystem, IEcsRunSystem
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    public void Init()
    {
        RightUIViewContainer.AddListenerBuildButton(delegate { Build(BuildingTypes.Farm); }, BuildingButtonTypes.First);
        RightUIViewContainer.AddListenerBuildButton(delegate { Build(BuildingTypes.Mine); }, BuildingButtonTypes.Second);
        RightUIViewContainer.AddListenerBuildButton(delegate { Build(BuildingTypes.City); }, BuildingButtonTypes.Third);
    }

    public void Run()
    {
        if (SelectorWorker.IsSelectedCell && CellUnitsDataContainer.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataContainer.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataContainer.IsMine(XySelectedCell))
                {
                    RightUIViewContainer.RemoveAllListenersBuildButton(BuildingButtonTypes.Third);

                    switch (CellUnitsDataContainer.UnitType(XySelectedCell))
                    {
                        case UnitTypes.None:
                            break;

                        case UnitTypes.King:
                            RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        case UnitTypes.Pawn:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.PawnSword:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.Rook:
                            RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        case UnitTypes.RookCrossbow:
                            RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        case UnitTypes.Bishop:
                            RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        case UnitTypes.BishopCrossbow:
                            RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        default:
                            throw new Exception();
                    }
                }

                else
                {
                    RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                }
            }

            else if (CellUnitsDataContainer.IsBot(XySelectedCell))
            {
                RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Building);
            }

            void PawnAndPawnSword()
            {
                RightUIViewContainer.SetActiveParentZone(true, UnitUIZoneTypes.Building);

                if (CellBuildDataContainer.HaveAnyBuilding(XySelectedCell))
                {
                    //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.First);
                    //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.Second);

                    if (CellUnitsDataContainer.HaveOwner(XySelectedCell))
                    {
                        if (CellUnitsDataContainer.IsMine(XySelectedCell))
                        {
                            if (CellBuildDataContainer.IsBuildingType(BuildingTypes.City, XySelectedCell))
                            {
                                RightUIViewContainer.SetActiveBuildingButton(false, BuildingButtonTypes.Third);
                            }
                            else
                            {
                                RightUIViewContainer.SetActiveBuildingButton(true, BuildingButtonTypes.Third);

                                RightUIViewContainer.AddListenerBuildButton(delegate { Destroy(); }, BuildingButtonTypes.Third);
                                RightUIViewContainer.SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                            }
                        }

                        else
                        {
                            RightUIViewContainer.SetActiveBuildingButton(true, BuildingButtonTypes.Third);

                            RightUIViewContainer.AddListenerBuildButton(delegate { Destroy(); }, BuildingButtonTypes.Third);
                            RightUIViewContainer.SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                        }
                    }

                    else if (CellBuildDataContainer.IsBot(XySelectedCell))
                    {
                        if (CellBuildDataContainer.IsBuildingType(BuildingTypes.City, XySelectedCell))
                        {
                            RightUIViewContainer.SetActiveBuildingButton(true, BuildingButtonTypes.Third);

                            RightUIViewContainer.AddListenerBuildButton(delegate { Destroy(); }, BuildingButtonTypes.Third);
                            RightUIViewContainer.SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                        }
                    }

                }

                else
                {
                    //if (!CellEnvirDataWorker.HaveEnvironments(XySelectedCell, new[] { EnvironmentTypes.AdultForest, EnvironmentTypes.YoungForest }))
                    //{
                    //    UIRightWorker.SetActiveBuildingButton(true, BuildingButtonTypes.First);
                    //}
                    //else
                    //{
                    //    UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.First);
                    //}


                    //if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, XySelectedCell))
                    //{
                    //    UIRightWorker.SetActiveBuildingButton(true, BuildingButtonTypes.Second);
                    //}

                    //else
                    //{
                    //    UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.Second);
                    //}


                    if (InfoBuidlingsDataContainer.IsSettedCity(PhotonNetwork.IsMasterClient))
                    {
                        RightUIViewContainer.SetActiveBuildingButton(false, BuildingButtonTypes.Third);
                    }
                    else
                    {
                        RightUIViewContainer.AddListenerBuildButton(delegate { Build(BuildingTypes.City); }, BuildingButtonTypes.Third);
                        RightUIViewContainer.SetTextBuildButton(BuildingButtonTypes.Third, "Build City");
                    }
                }
            }
        }

        else
        {
            RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Building);
        }
    }

    private void Build(BuildingTypes buildingType)
    {
        if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.BuildToMaster(XySelectedCell, buildingType);
    }
    private void Destroy()
    {
        if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.DestroyBuildingToMaster(XySelectedCell);
    }
}
