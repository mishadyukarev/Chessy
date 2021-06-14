internal sealed class UniqueAbilitiesUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    internal UniqueAbilitiesUISystem() { }

    public override void Run()
    {
        base.Run();



        if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit && _eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).IsMine)
        {

            switch (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:
                    _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                    break;

                case UnitTypes.Pawn:
                    _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(true);

                    _eGM.UniqueAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(true);
                    _eGM.Unique1AbilityEnt_ButtonCom.SetActive(true);
                    _eGM.Unique1AbilityEnt_ButtonCom.RemoveAllListeners();
                    _eGM.Unique1AbilityEnt_ButtonCom.AddListener(delegate { PawnUniqieAbility(UniqueAbilitiesPawnTypes.AbilityOne); });

                    if (_eGM.CellEffectEnt_CellEffectCom(XySelectedCell).HaveFire)
                    {
                        _eGM.UniqueFirstAbilityEnt_TextMeshProGUICom.Text = "Put Out FIRE";
                    }
                    else
                    {

                        _eGM.UniqueFirstAbilityEnt_TextMeshProGUICom.Text = "Fire forest";
                    }

                    _eGM.Unique2AbilityEnt_ButtonCom.SetActive(true);
                    _eGM.Unique2AbilityEnt_ButtonCom.RemoveAllListeners();
                    _eGM.Unique2AbilityEnt_ButtonCom.AddListener(delegate { PawnUniqieAbility(UniqueAbilitiesPawnTypes.AbilityTwo); });
                    _eGM.Unique2AbilityEnt_TextMeshProGUICom.Text = "Fertilize Field";

                    _eGM.Unique3AbilityEnt_ButtonCom.SetActive(true);
                    _eGM.Unique3AbilityEnt_ButtonCom.RemoveAllListeners();
                    _eGM.Unique3AbilityEnt_ButtonCom.AddListener(delegate { PawnUniqieAbility(UniqueAbilitiesPawnTypes.AbilityThree); });
                    _eGM.Unique3AbilityEnt_TextMeshProGUICom.Text = "Seed Forest";
                    break;

                case UnitTypes.Rook:
                    break;

                case UnitTypes.Bishop:
                    break;

                default:
                    break;
            }

        }

        else
        {
            _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
        }
    }

    private void PawnUniqieAbility(UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType)
    {
        _photonPunRPC.UniqueAbilityPawnToMaster(XySelectedCell, uniqueAbilitiesPawnType);
    }
}
