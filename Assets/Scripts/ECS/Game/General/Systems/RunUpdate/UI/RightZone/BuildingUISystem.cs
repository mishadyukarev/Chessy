using Assets.Scripts;
using static Assets.Scripts.Main;

internal sealed class BuildingUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;


    public override void Init()
    {
        base.Init();

        _eGM.BuildingFirstAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Farm); });
        //_eGM.BuildingSecondAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Woodcutter); });
        _eGM.BuildingThirdAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Mine); });
        _eGM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.City); });
    }

    public override void Run()
    {
        base.Run();


        if (_eGM.SelectorEnt_SelectorCom.IsSelected && _eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit)
        {
            if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).HaveOwner)
            {
                if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).IsMine)
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
                    _eGM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(false);
                    _eGM.BuildingFirstAbilityEnt_ButtonCom.SetActive(false);
                    //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
                    _eGM.BuildingThirdAbilityEnt_ButtonCom.SetActive(false);
                    _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
                }
            }

            else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).HaveBot)
            {
                _eGM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(false);
                _eGM.BuildingFirstAbilityEnt_ButtonCom.SetActive(false);
                //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
                _eGM.BuildingThirdAbilityEnt_ButtonCom.SetActive(false);
                _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
            }

            void PawnAndPawnSword()
            {
                _eGM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(true);
                _eGM.BuildingFirstAbilityEnt_ButtonCom.SetActive(true);
                //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(true);
                _eGM.BuildingThirdAbilityEnt_ButtonCom.SetActive(true);
                _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(true);

                _eGM.BuildingFourthAbilityEnt_ButtonCom.RemoveAllListeners();

                if (_eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).HaveBuilding)
                {
                    if (_eGM.CellBuildEnt_OwnerCom(XySelectedCell).HaveOwner)
                    {
                        if (_eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).BuildingType == BuildingTypes.City)
                        {
                            _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
                        }
                        else
                        {
                            _eGM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Destroy(); });
                            _eGM.BuildingFourthAbilityEnt_TextMeshProGUICom.Text = "Destroy";
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

                    else if (_eGM.CellBuildEnt_CellOwnerBotCom(XySelectedCell).HaveBot)
                    {
                        if (_eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).BuildingType == BuildingTypes.City)
                        {
                            _eGM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Destroy(); });
                            _eGM.BuildingFourthAbilityEnt_TextMeshProGUICom.Text = "Destroy";
                        }
                    }

                }
                else
                {
                    if (_eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Instance.IsMasterClient])
                    {
                        _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
                    }
                    else
                    {
                        _eGM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.City); });
                        _eGM.BuildingFourthAbilityEnt_TextMeshProGUICom.Text = "Build City";
                    }
                }
            }
        }

        else
        {
            _eGM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(false);
            _eGM.BuildingFirstAbilityEnt_ButtonCom.SetActive(false);
            //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
            _eGM.BuildingThirdAbilityEnt_ButtonCom.SetActive(false);
            _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
        }
    }

    private void Activate(bool isActivated)
    {
        _eGM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(isActivated);
        _eGM.BuildingFirstAbilityEnt_ButtonCom.SetActive(isActivated);
        //_eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(isActivated);
        _eGM.BuildingThirdAbilityEnt_ButtonCom.SetActive(isActivated);
        _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(isActivated);
    }

    private void Build(BuildingTypes buildingType) => _photonPunRPC.BuildToMaster(XySelectedCell, buildingType);
    private void Destroy() => _photonPunRPC.DestroyBuildingToMaster(XySelectedCell);
}
