using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;
using static Assets.Scripts.Main;

internal sealed class BuildingUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    public override void Init()
    {
        base.Init();

        _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { Build(BuildingTypes.Farm); });
        //_eGM.BuildingSecondAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Woodcutter); });
        _eGGUIM.BuildingSecondAbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { Build(BuildingTypes.Mine); });
        _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { Build(BuildingTypes.City); });
    }

    public override void Run()
    {
        base.Run();


        if (SelectorWorker.IsSelectedCell && CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
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
                            Activate(false);
                            break;

                        case UnitTypes.Pawn:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.PawnSword:
                            PawnAndPawnSword();
                            break;

                        case UnitTypes.Rook:
                            Activate(false);
                            break;

                        case UnitTypes.RookCrossbow:
                            Activate(false);
                            break;

                        case UnitTypes.Bishop:
                            Activate(false);
                            break;

                        case UnitTypes.BishopCrossbow:
                            Activate(false);
                            break;

                        default:
                            break;
                    }
                }

                else
                {
                    _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.TextMeshProUGUI.gameObject.SetActive(false);
                    _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
                    //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
                    _eGGUIM.BuildingSecondAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
                    _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
                }
            }

            else if (CellUnitsDataWorker.IsBot(XySelectedCell))
            {
                _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.TextMeshProUGUI.gameObject.SetActive(false);
                _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
                //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
                _eGGUIM.BuildingSecondAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
                _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
            }

            void PawnAndPawnSword()
            {
                _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.TextMeshProUGUI.gameObject.SetActive(true);
                _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(true);
                _eGGUIM.BuildingSecondAbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.gameObject.SetActive(true);

                _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.onClick.RemoveAllListeners();

                if (CellBuildingsDataWorker.HaveAnyBuilding(XySelectedCell))
                {
                    if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
                    {
                        if (CellUnitsDataWorker.IsMine(XySelectedCell))
                        {
                            if (CellBuildingsDataWorker.IsBuildingType(BuildingTypes.City, XySelectedCell))
                            {
                                _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
                            }
                            else
                            {
                                _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { Destroy(); });
                                _eGGUIM.BuildingThirdAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Destroy";
                            }
                        }

                        else
                        {
                            _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.gameObject.SetActive(true);

                            _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { Destroy(); });
                            _eGGUIM.BuildingThirdAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Destroy";
                        }



                        //if (_eGM.CellBuildEnt_OwnerCom(XySelectedCell).IsMine)
                        //{
                        //    if (_eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).BuildingType == BuildingTypes.City)
                        //    {
                        //        _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
                        //    }
                        //    else
                        //    {
                        //        _eGM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Destroy(); });
                        //        _eGM.BuildingFourthAbilityEnt_TextMeshProGUICom.Text = "Destroy";
                        //    }
                        //}
                        //else
                        //{
                        //    if (_eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).BuildingType == BuildingTypes.City)
                        //    {
                        //        _eGM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Destroy(); });
                        //        _eGM.BuildingFourthAbilityEnt_TextMeshProGUICom.Text = "Destroy";
                        //    }
                        //}
                    }

                    else if (CellBuildingsDataWorker.IsBot(XySelectedCell))
                    {
                        if (CellBuildingsDataWorker.IsBuildingType(BuildingTypes.City, XySelectedCell))
                        {
                            _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { Destroy(); });
                            _eGGUIM.BuildingThirdAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Destroy";
                        }
                    }

                }
                else
                {
                    if (InfoBuidlingsWorker.IsSettedCity(Instance.IsMasterClient))
                    {
                        _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
                    }
                    else
                    {
                        _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { Build(BuildingTypes.City); });
                        _eGGUIM.BuildingThirdAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Build City";
                    }
                }
            }
        }

        else
        {
            _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.TextMeshProUGUI.gameObject.SetActive(false);
            _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
            //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
            _eGGUIM.BuildingSecondAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
            _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
        }
    }

    private void Activate(bool isActivated)
    {
        _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.TextMeshProUGUI.gameObject.SetActive(isActivated);
        _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.Button.gameObject.SetActive(isActivated);
        //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(isActivated);
        _eGGUIM.BuildingSecondAbilityEnt_ButtonCom.Button.gameObject.SetActive(isActivated);
        _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.Button.gameObject.SetActive(isActivated);
    }

    private void Build(BuildingTypes buildingType)
    {
        if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.BuildToMaster(XySelectedCell, buildingType);
    }
    private void Destroy()
    {
        if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient)) PhotonPunRPC.DestroyBuildingToMaster(XySelectedCell);
    }
}
