using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using static Assets.Scripts.Main;

internal sealed class BuildingUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    private bool IsActivatedDoner => _eGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient);

    public override void Init()
    {
        base.Init();

        _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Farm); });
        //_eGM.BuildingSecondAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Woodcutter); });
        _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Mine); });
        _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.City); });
    }

    public override void Run()
    {
        base.Run();


        if (_eGM.SelectorEnt_SelectorCom.IsSelected && CellUnitWorker.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitWorker.HaveOwner(XySelectedCell))
            {
                if (CellUnitWorker.IsMine(XySelectedCell))
                {
                    switch (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType)
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
                    _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(false);
                    _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.SetActive(false);
                    //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
                    _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.SetActive(false);
                    _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
                }
            }

            else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).IsBot)
            {
                _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(false);
                _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.SetActive(false);
                //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
                _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.SetActive(false);
                _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
            }

            void PawnAndPawnSword()
            {
                _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(true);
                _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.SetActive(true);
                //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(true);
                _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.SetActive(true);
                _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.SetActive(true);

                _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.RemoveAllListeners();

                if (CellBuildingWorker.HaveBuilding(XySelectedCell))
                {
                    if (CellUnitWorker.HaveOwner(XySelectedCell))
                    {
                        if (CellUnitWorker.IsMine(XySelectedCell))
                        {
                            if (_eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).BuildingType == BuildingTypes.City)
                            {
                                _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
                            }
                            else
                            {
                                _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Destroy(); });
                                _eGGUIM.BuildingFourthAbilityEnt_TextMeshProGUICom.SetText("Destroy");
                            }
                        }

                        else
                        {
                            _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.SetActive(true);

                            _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Destroy(); });
                            _eGGUIM.BuildingFourthAbilityEnt_TextMeshProGUICom.SetText("Destroy");
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

                    else if (_eGM.CellBuildEnt_OwnerBotCom(XySelectedCell).IsBot)
                    {
                        if (_eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).BuildingType == BuildingTypes.City)
                        {
                            _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Destroy(); });
                            _eGGUIM.BuildingFourthAbilityEnt_TextMeshProGUICom.SetText("Destroy");
                        }
                    }

                }
                else
                {
                    if (InfoBuidlingsWorker.IsSettedCity(Instance.IsMasterClient))
                    {
                        _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
                    }
                    else
                    {
                        _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.City); });
                        _eGGUIM.BuildingFourthAbilityEnt_TextMeshProGUICom.SetText("Build City");
                    }
                }
            }
        }

        else
        {
            _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(false);
            _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.SetActive(false);
            //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
            _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.SetActive(false);
            _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
        }
    }

    private void Activate(bool isActivated)
    {
        _eGGUIM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(isActivated);
        _eGGUIM.BuildingFirstAbilityEnt_ButtonCom.SetActive(isActivated);
        //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(isActivated);
        _eGGUIM.BuildingThirdAbilityEnt_ButtonCom.SetActive(isActivated);
        _eGGUIM.BuildingFourthAbilityEnt_ButtonCom.SetActive(isActivated);
    }

    private void Build(BuildingTypes buildingType)
    {
        if (!IsActivatedDoner) PhotonPunRPC.BuildToMaster(XySelectedCell, buildingType);
    }
    private void Destroy()
    {
        if (!IsActivatedDoner) PhotonPunRPC.DestroyBuildingToMaster(XySelectedCell);
    }
}
