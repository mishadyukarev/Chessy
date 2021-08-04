using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System;

internal sealed class BuildingUISystem : IEcsInitSystem, IEcsRunSystem
{
    private int[] XySelectedCell => SelectorSystem.XySelectedCell;
    public void Init()
    {
        RightUIViewContainer.AddListenerBuildButton(delegate { Build(BuildingTypes.Farm); }, BuildingButtonTypes.First);
        RightUIViewContainer.AddListenerBuildButton(delegate { Build(BuildingTypes.Mine); }, BuildingButtonTypes.Second);
        RightUIViewContainer.AddListenerBuildButton(delegate { Build(BuildingTypes.City); }, BuildingButtonTypes.Third);
    }

    public void Run()
    {
        if (SelectorSystem.IsSelectedCell && CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataSystem.IsMine(XySelectedCell))
                {
                    RightUIViewContainer.RemoveAllListenersBuildButton(BuildingButtonTypes.Third);

                    switch (CellUnitsDataSystem.UnitType(XySelectedCell))
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

            else if (CellUnitsDataSystem.IsBot(XySelectedCell))
            {
                RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Building);
            }

            void PawnAndPawnSword()
            {
                RightUIViewContainer.SetActiveParentZone(true, UnitUIZoneTypes.Building);

                if (CellBuildDataSystem.BuildTypeCom(XySelectedCell).HaveBuild)
                {
                    //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.First);
                    //UIRightWorker.SetActiveBuildingButton(false, BuildingButtonTypes.Second);

                    if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
                    {
                        if (CellUnitsDataSystem.IsMine(XySelectedCell))
                        {
                            if (CellBuildDataSystem.BuildTypeCom(XySelectedCell).Is(BuildingTypes.City))
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

                    else if (CellBuildDataSystem.OwnerBotCom(XySelectedCell).IsBot)
                    {
                        if (CellBuildDataSystem.BuildTypeCom(XySelectedCell).Is(BuildingTypes.City))
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


                    if (InitSystem.XyBuildingsCom.IsSettedCity(PhotonNetwork.IsMasterClient))
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
