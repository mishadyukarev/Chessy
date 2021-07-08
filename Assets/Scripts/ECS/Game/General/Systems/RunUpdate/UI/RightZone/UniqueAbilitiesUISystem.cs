using Assets.Scripts;

internal sealed class UniqueAbilitiesUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    internal UniqueAbilitiesUISystem() { }

    public override void Run()
    {
        base.Run();


        if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit)
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
                            _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                            break;

                        case UnitTypes.Pawn:
                            Melee();
                            break;

                        case UnitTypes.PawnSword:
                            Melee();
                            break;

                        case UnitTypes.Rook:
                            _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                            break;

                        case UnitTypes.RookCrossbow:
                            _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                            break;

                        case UnitTypes.Bishop:
                            _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                            break;

                        case UnitTypes.BishopCrossbow:
                            _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
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
            else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).HaveBot)
            {
                _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
            }

            void Melee()
            {
                _eGM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(true);

                _eGM.UniqueAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(true);
                _eGM.Unique1AbilityEnt_ButtonCom.SetActive(true);
                _eGM.Unique1AbilityEnt_ButtonCom.RemoveAllListeners();
                _eGM.Unique1AbilityEnt_ButtonCom.AddListener(delegate { Fire(XySelectedCell, XySelectedCell); });

                if (_eGM.CellEffectEnt_CellEffectCom(XySelectedCell).HaveEffect(EffectTypes.Fire))
                {
                    _eGM.UniqueFirstAbilityEnt_TextMeshProGUICom.Text = "Put Out FIRE";
                }
                else
                {

                    _eGM.UniqueFirstAbilityEnt_TextMeshProGUICom.Text = "Fire forest";
                }

                _eGM.Unique2AbilityEnt_ButtonCom.SetActive(false);

                _eGM.Unique3AbilityEnt_ButtonCom.SetActive(true);
                _eGM.Unique3AbilityEnt_ButtonCom.RemoveAllListeners();
                _eGM.Unique3AbilityEnt_ButtonCom.AddListener(delegate { PawnUniqieAbility(UniqueAbilitiesPawnTypes.AbilityThree); });
                _eGM.Unique3AbilityEnt_TextMeshProGUICom.Text = "Seed Forest";
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

    private void Fire(int[] fromXy, int[] toXy)
    {
        _photonPunRPC.FireToMaster(fromXy, toXy);
    }
}
