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
        UIRightWorker.AddListenerBuildButton(delegate { Build(BuildingTypes.Farm); }, BuildingButtonTypes.First);
        UIRightWorker.AddListenerBuildButton(delegate { Build(BuildingTypes.Mine); }, BuildingButtonTypes.Second);
        UIRightWorker.AddListenerBuildButton(delegate { Build(BuildingTypes.City); }, BuildingButtonTypes.Third);
    }

    public void Run()
    {
        if (SelectorWorker.IsSelectedCell && CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataWorker.IsMine(XySelectedCell))
                {
                    UIRightWorker.RemoveAllListenersBuildButton(BuildingButtonTypes.Third);

                    switch (CellUnitsDataWorker.UnitType(XySelectedCell))
                    {
                        case UnitTypes.None:
                            break;

                        case UnitTypes.King:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        case UnitTypes.Pawn:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.PawnSword:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.Rook:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        case UnitTypes.RookCrossbow:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        case UnitTypes.Bishop:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        case UnitTypes.BishopCrossbow:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                            break;

                        default:
                            throw new Exception();
                    }
                }

                else
                {
                    UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Building);
                }
            }

            else if (CellUnitsDataWorker.IsBot(XySelectedCell))
            {
                UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Building);
            }

            void PawnAndPawnSword()
            {
                UIRightWorker.SetActiveParentZone(true, UnitUIZoneTypes.Building);

                if (CellBuildingsDataWorker.HaveAnyBuilding(XySelectedCell))
                {
                    //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.First);
                    //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.Second);

                    if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
                    {
                        if (CellUnitsDataWorker.IsMine(XySelectedCell))
                        {
                            if (CellBuildingsDataWorker.IsBuildingType(BuildingTypes.City, XySelectedCell))
                            {
                                UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.Third);
                            }
                            else
                            {
                                UIRightWorker.SetActiveBuildingButton(true, BuildingButtonTypes.Third);

                                UIRightWorker.AddListenerBuildButton(delegate { Destroy(); }, BuildingButtonTypes.Third);
                                UIRightWorker.SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                            }
                        }

                        else
                        {
                            UIRightWorker.SetActiveBuildingButton(true, BuildingButtonTypes.Third);

                            UIRightWorker.AddListenerBuildButton(delegate { Destroy(); }, BuildingButtonTypes.Third);
                            UIRightWorker.SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
                        }
                    }

                    else if (CellBuildingsDataWorker.IsBot(XySelectedCell))
                    {
                        if (CellBuildingsDataWorker.IsBuildingType(BuildingTypes.City, XySelectedCell))
                        {
                            UIRightWorker.SetActiveBuildingButton(true, BuildingButtonTypes.Third);

                            UIRightWorker.AddListenerBuildButton(delegate { Destroy(); }, BuildingButtonTypes.Third);
                            UIRightWorker.SetTextBuildButton(BuildingButtonTypes.Third, "Destroy");
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


                    if (InfoBuidlingsWorker.IsSettedCity(PhotonNetwork.IsMasterClient))
                    {
                        UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.Third);
                    }
                    else
                    {
                        UIRightWorker.AddListenerBuildButton(delegate { Build(BuildingTypes.City); }, BuildingButtonTypes.Third);
                        UIRightWorker.SetTextBuildButton(BuildingButtonTypes.Third, "Build City");
                    }
                }
            }
        }

        else
        {
            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Building);
        }
    }

    private void Build(BuildingTypes buildingType)
    {
        if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.BuildToMaster(XySelectedCell, buildingType);
    }
    private void Destroy()
    {
        if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.DestroyBuildingToMaster(XySelectedCell);
    }
}
