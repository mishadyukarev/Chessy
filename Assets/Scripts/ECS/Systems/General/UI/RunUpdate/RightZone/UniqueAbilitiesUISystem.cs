using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;
using static Assets.Scripts.Main;

internal sealed class UniqueAbilitiesUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    internal UniqueAbilitiesUISystem() { }

    public override void Run()
    {
        base.Run();


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

                _eGGUIM.Unique1AbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
                _eGGUIM.Unique3AbilityEnt_ButtonCom.Button.gameObject.SetActive(false);

                if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, XySelectedCell))
                {
                    _eGGUIM.Unique1AbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                    _eGGUIM.Unique1AbilityEnt_ButtonCom.Button.onClick.RemoveAllListeners();
                    UIRightWorker.AddListener(delegate { Fire(XySelectedCell, XySelectedCell); }, UniqueAbilitiesTypes.First);

                    if (CellFireDataWorker.HaveFire(XySelectedCell))
                    {
                        _eGGUIM.UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Put Out FIRE";
                    }
                    else
                    {

                        _eGGUIM.UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Fire forest";
                    }
                }

                else if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, XySelectedCell)
                    && !CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.YoungForest, XySelectedCell))
                {
                    _eGGUIM.Unique1AbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                    _eGGUIM.Unique1AbilityEnt_ButtonCom.Button.onClick.RemoveAllListeners();
                    UIRightWorker.AddListener(delegate { SeedEnvironment(EnvironmentTypes.YoungForest); }, UniqueAbilitiesTypes.First);
                    _eGGUIM.UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Seed Forest";
                }

                else
                {

                }


                _eGGUIM.Unique2AbilityEnt_ButtonCom.Button.gameObject.SetActive(false);








            }
        }

        else
        {
            _eGGUIM.UniquePareZoneEnt_ParentCom.ParentGO.SetActive(false);
        }
    }

    private void SeedEnvironment(EnvironmentTypes environmentType)
    {
        if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.SeedEnvironmentToMaster(XySelectedCell, environmentType);
    }

    private void Fire(int[] fromXy, int[] toXy)
    {
        if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.FireToMaster(fromXy, toXy);
    }
}
