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
    internal UniqueAbilitiesUISystem() { }

    public void Run()
    {
        if (CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
            {
                if (CellUnitsDataWorker.IsMine(XySelectedCell))
                {
                    switch (CellUnitsDataWorker.UnitType(XySelectedCell))
                    {
                        case UnitTypes.None:
                            break;

                        case UnitTypes.King:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
                            break;

                        case UnitTypes.Pawn:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.PawnSword:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.Rook:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
                            break;

                        case UnitTypes.RookCrossbow:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
                            break;

                        case UnitTypes.Bishop:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
                            break;

                        case UnitTypes.BishopCrossbow:
                            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Unique);
                            break;

                        default:
                            break;
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

            void PawnAndPawnSword()
            {
                UIRightWorker.SetActiveParentZone(true, UnitUIZoneTypes.Unique);

                UIRightWorker.SetActiveUniqueButton(false, UniqueAbilitiesTypes.Second);
                UIRightWorker.SetActiveUniqueButton(false, UniqueAbilitiesTypes.Third);


                UIRightWorker.RemoveAllListenersUniqueButton(UniqueAbilitiesTypes.First);



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
}
