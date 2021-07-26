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
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.ParentGO.SetActive(false);
                            break;

                        case UnitTypes.Pawn:
                            Melee();
                            break;

                        case UnitTypes.PawnSword:
                            Melee();
                            break;

                        case UnitTypes.Rook:
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.ParentGO.SetActive(false);
                            break;

                        case UnitTypes.RookCrossbow:
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.ParentGO.SetActive(false);
                            break;

                        case UnitTypes.Bishop:
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.ParentGO.SetActive(false);
                            break;

                        case UnitTypes.BishopCrossbow:
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.ParentGO.SetActive(false);
                            break;

                        default:
                            break;
                    }
                }

                else
                {
                    _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.ParentGO.SetActive(false);
                }
            }
            else if (CellUnitsDataWorker.IsBot(XySelectedCell))
            {
                _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.ParentGO.SetActive(false);
            }

            void Melee()
            {
                _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.ParentGO.SetActive(true);

                _eGGUIM.UniqueAbilitiesZoneEnt_TextMeshProUGUICom.TextMeshProUGUI.gameObject.SetActive(true);
                _eGGUIM.Unique1AbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                _eGGUIM.Unique1AbilityEnt_ButtonCom.Button.onClick.RemoveAllListeners();
                _eGGUIM.Unique1AbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { Fire(XySelectedCell, XySelectedCell); });

                if (CellFireDataWorker.HaveFire(XySelectedCell))
                {
                    _eGGUIM.UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Put Out FIRE";
                }
                else
                {

                    _eGGUIM.UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Fire forest";
                }

                _eGGUIM.Unique2AbilityEnt_ButtonCom.Button.gameObject.SetActive(false);

                _eGGUIM.Unique3AbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                _eGGUIM.Unique3AbilityEnt_ButtonCom.Button.onClick.RemoveAllListeners();
                _eGGUIM.Unique3AbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { SeedEnvironment(EnvironmentTypes.YoungForest); });
                _eGGUIM.Unique3AbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Seed Forest";
            }
        }

        else
        {
            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.ParentGO.SetActive(false);
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
