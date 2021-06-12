using static Main;

internal sealed class BuildingUISystem : RPCGeneralSystemReduction
{
    private int[] _xySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;


    public override void Init()
    {
        base.Init();

        _eGM.BuildingFirstAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Farm); });
        _eGM.BuildingSecondAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Woodcutter); });
        _eGM.BuildingThirdAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.Mine); });
        _eGM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Build(BuildingTypes.City); });
    }

    public override void Run()
    {
        base.Run();


        if (_eGM.SelectorEnt_SelectorCom.IsSelected && _eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit && _eGM.CellUnitEnt_CellOwnerCom(_xySelectedCell).IsMine)
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).UnitType)
            {
                case UnitTypes.King:

                    _eGM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(false);
                    _eGM.BuildingFirstAbilityEnt_ButtonCom.SetActive(false);
                    _eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
                    _eGM.BuildingThirdAbilityEnt_ButtonCom.SetActive(false);
                    _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);

                    break;


                case UnitTypes.Pawn:
                    _eGM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(true);
                    _eGM.BuildingFirstAbilityEnt_ButtonCom.SetActive(true);
                    _eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(true);
                    _eGM.BuildingThirdAbilityEnt_ButtonCom.SetActive(true);
                    _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(true);

                    _eGM.BuildingFourthAbilityEnt_ButtonCom.RemoveAllListeners();

                    if (_eGM.CellBuildEnt_BuilTypeCom(_xySelectedCell).HaveBuilding)
                    {
                        if (_eGM.CellBuildEnt_OwnerCom(_xySelectedCell).IsMine)
                        {
                            if (_eGM.CellBuildEnt_BuilTypeCom(_xySelectedCell).BuildingType == BuildingTypes.City)
                            {
                                _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
                            }
                            else
                            {
                                _eGM.BuildingFourthAbilityEnt_ButtonCom.AddListener(delegate { Destroy(); });
                                _eGM.BuildingFourthAbilityEnt_TextMeshProGUICom.Text = "Destroy";
                            }
                        }
                        else
                        {

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
                    break;
            }
        }

        else
        {
            _eGM.BuildingAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(false);
            _eGM.BuildingFirstAbilityEnt_ButtonCom.SetActive(false);
            _eGM.BuildingSecondAbilityEnt_ButtonCom.SetActive(false);
            _eGM.BuildingThirdAbilityEnt_ButtonCom.SetActive(false);
            _eGM.BuildingFourthAbilityEnt_ButtonCom.SetActive(false);
        }
    }

    private void Build(BuildingTypes buildingType) => _photonPunRPC.BuildToMaster(_xySelectedCell, buildingType);
    private void Destroy() => _photonPunRPC.DestroyBuildingToMaster(_xySelectedCell);
}
