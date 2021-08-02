using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class UniqueAbilitiesUISystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public void Run()
    {
        if (CellUnitsDataContainer.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataContainer.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataContainer.IsMine(XySelectedCell))
                {
                    RightUIViewContainer.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.First);
                    RightUIViewContainer.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.Second);
                    RightUIViewContainer.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.Third);

                    RightUIViewContainer.SetActiveUniqueButton(false, UniqueAbilitiesTypes.Second);
                    RightUIViewContainer.SetActiveUniqueButton(false, UniqueAbilitiesTypes.Third);


                    if (CellUnitsDataContainer.IsUnitType(UnitTypes.King, XySelectedCell))
                    {
                        RightUIViewContainer.SetActiveParentZone(true, UnitUIZoneTypes.Unique);
                        RightUIViewContainer.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);

                        RightUIViewContainer.AddListenerUniqueButton(CircularAttackKing, UniqueAbilitiesTypes.First);
                    }
                    else
                    {
                        if (CellUnitsDataContainer.IsMelee(XySelectedCell))
                        {
                            RightUIViewContainer.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);

                            RightUIViewContainer.SetActiveParentZone(true, UnitUIZoneTypes.Unique);
                            RightUIViewContainer.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);

                            if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.AdultForest, XySelectedCell))
                            {
                                RightUIViewContainer.AddListenerUniqueButton(delegate { Fire(XySelectedCell, XySelectedCell); }, UniqueAbilitiesTypes.First);
                                if (CellFireDataContainer.HaveFire(XySelectedCell))
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
            else if (CellUnitsDataContainer.IsBot(XySelectedCell))
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
        if (SelectorWorker.SelectorType == SelectorTypes.PickFire)
        {
            SelectorWorker.SelectorType = SelectorTypes.Other;
        }
        else
        {
            SelectorWorker.SelectorType = SelectorTypes.PickFire;
        }
    }
}
