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
        if (CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataWorker.IsMine(XySelectedCell))
                {
                    UIRightWorker.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.First);
                    UIRightWorker.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.Second);
                    UIRightWorker.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.Third);

                    UIRightWorker.SetActiveUniqueButton(false, UniqueAbilitiesTypes.Second);
                    UIRightWorker.SetActiveUniqueButton(false, UniqueAbilitiesTypes.Third);


                    if (CellUnitsDataWorker.IsUnitType(UnitTypes.King, XySelectedCell))
                    {
                        UIRightWorker.SetActiveParentZone(true, UnitUIZoneTypes.Unique);
                        UIRightWorker.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);

                        UIRightWorker.AddListenerUniqueButton(CircularAttackKing, UniqueAbilitiesTypes.First);
                    }
                    else
                    {
                        if (CellUnitsDataWorker.IsMelee(XySelectedCell))
                        {
                            UIRightWorker.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);

                            UIRightWorker.SetActiveParentZone(true, UnitUIZoneTypes.Unique);
                            UIRightWorker.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);

                            if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, XySelectedCell))
                            {
                                UIRightWorker.AddListenerUniqueButton(delegate { Fire(XySelectedCell, XySelectedCell); }, UniqueAbilitiesTypes.First);
                                if (CellFireDataWorker.HaveFire(XySelectedCell))
                                {
                                    UIRightWorker.SetUniqueButtonText(UniqueAbilitiesTypes.First, "Put Out FIRE");
                                }
                                else
                                {
                                    UIRightWorker.SetUniqueButtonText(UniqueAbilitiesTypes.First, "Fire forest");
                                }

                            }
                            else
                            {
                                UIRightWorker.AddListenerUniqueButton(delegate { SeedEnvironment(EnvironmentTypes.YoungForest); }, UniqueAbilitiesTypes.First);
                                UIRightWorker.SetUniqueButtonText(UniqueAbilitiesTypes.First, "Seed Forest");
                            }
                        }

                        else
                        {
                            UIRightWorker.SetActiveParentZone(true, UnitUIZoneTypes.Unique);
                            UIRightWorker.SetActiveUniqueButton(true, UniqueAbilitiesTypes.First);
                            UIRightWorker.AddListenerUniqueButton(ActiveFireSelector, UniqueAbilitiesTypes.First);
                        }
                    }
                }

                else
                {
                    UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
                }
            }
            else if (CellUnitsDataWorker.IsBot(XySelectedCell))
            {
                UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
            }
        }

        else
        {
            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
        }
    }

    private void SeedEnvironment(EnvironmentTypes environmentType)
    {
        if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.SeedEnvironmentToMaster(XySelectedCell, environmentType);
    }

    private void Fire(int[] fromXy, int[] toXy)
    {
        if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.FireToMaster(fromXy, toXy);
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
