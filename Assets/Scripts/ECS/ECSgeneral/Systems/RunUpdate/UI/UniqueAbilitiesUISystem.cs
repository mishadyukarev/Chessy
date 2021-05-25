using Leopotam.Ecs;

internal sealed class UniqueAbilitiesUISystem : RPCGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;

    internal UniqueAbilitiesUISystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();


        _eGM.Unique1AbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
        _eGM.Unique2AbilityEnt_ButtonCom.Button.gameObject.SetActive(false);
        _eGM.Unique3AbilityEnt_ButtonCom.Button.gameObject.SetActive(false);

        if (_eGM.CellEnt_CellUnitCom(XySelectedCell).IsMine)
        {

            switch (_eGM.CellEnt_CellUnitCom(XySelectedCell).UnitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:
                    break;

                case UnitTypes.Pawn:
                    _eGM.Unique1AbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                    _eGM.Unique1AbilityEnt_ButtonCom.Button.onClick.RemoveAllListeners();
                    _eGM.Unique1AbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { PawnUniqieAbility(UniqueAbilitiesPawnTypes.AbilityOne); });

                    if (_eGM.CellEnt_CellEffectCom(XySelectedCell).HaveFire)
                    {
                        _eGM.UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Put Out FIRE";
                    }
                    else
                    {

                        _eGM.UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Fire forest";
                    }


                    _eGM.Unique2AbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                    _eGM.Unique2AbilityEnt_ButtonCom.Button.onClick.RemoveAllListeners();
                    _eGM.Unique2AbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { PawnUniqieAbility(UniqueAbilitiesPawnTypes.AbilityTwo); });
                    _eGM.Unique2AbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Fertilize Field";

                    _eGM.Unique3AbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                    _eGM.Unique3AbilityEnt_ButtonCom.Button.onClick.RemoveAllListeners();
                    _eGM.Unique3AbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { PawnUniqieAbility(UniqueAbilitiesPawnTypes.AbilityThree); });
                    _eGM.Unique3AbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Seed Forest";
                    break;

                case UnitTypes.Rook:
                    break;

                case UnitTypes.Bishop:
                    break;

                default:
                    break;
            }

        }
    }

    private void PawnUniqieAbility(UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType)
    {
        _photonPunRPC.UniqueAbilityPawnToMaster(XySelectedCell, uniqueAbilitiesPawnType);
    }
}
