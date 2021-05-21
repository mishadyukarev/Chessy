using Leopotam.Ecs;

internal class UniqueAbilitiesUISystem : RPCGeneralReduction, IEcsRunSystem
{
    private int[] XySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;

    internal UniqueAbilitiesUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }

    public void Run()
    {
        _eGM.UniqueFirstAbilityEnt_ButtonCom.Button.gameObject.SetActive(false);

        switch (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                break;

            case UnitTypes.Pawn:
                _eGM.UniqueFirstAbilityEnt_ButtonCom.Button.gameObject.SetActive(true);
                _eGM.UniqueFirstAbilityEnt_ButtonCom.Button.onClick.RemoveAllListeners();
                _eGM.UniqueFirstAbilityEnt_ButtonCom.Button.onClick.AddListener(delegate { PawnUniqieAbility(UniqueAbilitiesPawnTypes.AbilityOne); });
                _eGM.UniqueFirstAbilityEnt_TextMeshProGUICom.TextMeshProUGUI.text = "Fire something";

                break;

            case UnitTypes.Rook:
                break;

            case UnitTypes.Bishop:
                break;

            default:
                break;
        }
    }

    private void PawnUniqieAbility(UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType)
    {
        _photonPunRPC.UniqueAbilityPawnToMaster(XySelectedCell, uniqueAbilitiesPawnType);
    }
}
