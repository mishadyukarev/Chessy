using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class UniqueAbilitiesUISystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorSystem.XySelectedCell;

    public void Run()
    {
        if (CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataSystem.IsMine(XySelectedCell))
                {
                    RightUIViewContainer.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.First);
                    RightUIViewContainer.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.Second);
                    RightUIViewContainer.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.Third);

                    RightUIViewContainer.SetActiveUniqueButton(false, UniqueAbilitiesTypes.Second);
                    RightUIViewContainer.SetActiveUniqueButton(false, UniqueAbilitiesTypes.Third);


                    if (CellUnitsDataSystem.IsUnitType(UnitTypes.King, XySelectedCell))
                    {
                        RightUIViewContainer.SetActiveParentZone(true, UnitUIZoneTypes.Unique);
                        RightUIViewContainer.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);

                        RightUIViewContainer.AddListenerUniqueButton(CircularAttackKing, UniqueAbilitiesTypes.First);
                    }
                    else
                    {
                        if (CellUnitsDataSystem.IsMelee(XySelectedCell))
                        {
                            RightUIViewContainer.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);

                            RightUIViewContainer.SetActiveParentZone(true, UnitUIZoneTypes.Unique);
                            RightUIViewContainer.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);

                            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, XySelectedCell))
                            {
                                RightUIViewContainer.AddListenerUniqueButton(delegate { Fire(XySelectedCell, XySelectedCell); }, UniqueAbilitiesTypes.First);
                                if (CellFireDataSystem.HaveFireCom(XySelectedCell).HaveFire)
                                {
                                    RightUIViewContainer.SetUniqueButtonText(UniqueAbilitiesTypes.First, "Put Out FIRE");
                                }
                                else
                                {
                                    RightUIViewContainer.SetUniqueButtonText(UniqueAbilitiesTypes.First, "Fire forest");
                                }

                            }
                            else
                            {
                                RightUIViewContainer.AddListenerUniqueButton(delegate { SeedEnvironment(EnvironmentTypes.YoungForest); }, UniqueAbilitiesTypes.First);
                                RightUIViewContainer.SetUniqueButtonText(UniqueAbilitiesTypes.First, "Seed Forest");
                            }
                        }

                        else
                        {
                            RightUIViewContainer.SetActiveParentZone(true, UnitUIZoneTypes.Unique);
                            RightUIViewContainer.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);
                            RightUIViewContainer.AddListenerUniqueButton(ActiveFireSelector, UniqueAbilitiesTypes.First);
                        }
                    }
                }

                else
                {
                    RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
                }
            }
            else if (CellUnitsDataSystem.IsBot(XySelectedCell))
            {
                RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
            }
        }

        else
        {
            RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
        }
    }

    private void SeedEnvironment(EnvironmentTypes environmentType)
    {
        if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.SeedEnvironmentToMaster(XySelectedCell, environmentType);
    }

    private void Fire(int[] fromXy, int[] toXy)
    {
        if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.FireToMaster(fromXy, toXy);
    }

    private void CircularAttackKing()
    {
        PhotonPunRPC.CircularAttackKingToMaster(XySelectedCell);
    }

    private void ActiveFireSelector()
    {
        if (SelectorSystem.SelectorType == SelectorTypes.PickFire)
        {
            SelectorSystem.SelectorType = SelectorTypes.StartClick;
        }
        else
        {
            SelectorSystem.SelectorType = SelectorTypes.PickFire;
        }
    }
}
