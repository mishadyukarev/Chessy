using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Fire;
using static Assets.Scripts.Main;

internal sealed class UniqueAbilitiesUISystem : RPCGeneralSystemReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    private bool IsActivatedDoner => _eGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient);

    internal UniqueAbilitiesUISystem() { }

    public override void Run()
    {
        base.Run();


        if (CellUnitWorker.HaveAnyUnit(XySelectedCell))
        {
            if (CellUnitWorker.HaveOwner(XySelectedCell))
            {
                if (CellUnitWorker.IsMine(XySelectedCell))
                {
                    switch (CellUnitWorker.UnitType(XySelectedCell))
                    {
                        case UnitTypes.None:
                            break;

                        case UnitTypes.King:
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                            break;

                        case UnitTypes.Pawn:
                            Melee();
                            break;

                        case UnitTypes.PawnSword:
                            Melee();
                            break;

                        case UnitTypes.Rook:
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                            break;

                        case UnitTypes.RookCrossbow:
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                            break;

                        case UnitTypes.Bishop:
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                            break;

                        case UnitTypes.BishopCrossbow:
                            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                            break;

                        default:
                            break;
                    }
                }

                else
                {
                    _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
                }
            }
            else if (CellUnitWorker.IsBot(XySelectedCell))
            {
                _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
            }

            void Melee()
            {
                _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(true);

                _eGGUIM.UniqueAbilitiesZoneEnt_TextMeshProUGUICom.SetActive(true);
                _eGGUIM.Unique1AbilityEnt_ButtonCom.SetActive(true);
                _eGGUIM.Unique1AbilityEnt_ButtonCom.RemoveAllListeners();
                _eGGUIM.Unique1AbilityEnt_ButtonCom.AddListener(delegate { Fire(XySelectedCell, XySelectedCell); });

                if (CellFireWorker.HaveEffect(EffectTypes.Fire, XySelectedCell))
                {
                    _eGGUIM.UniqueFirstAbilityEnt_TextMeshProGUICom.SetText("Put Out FIRE");
                }
                else
                {

                    _eGGUIM.UniqueFirstAbilityEnt_TextMeshProGUICom.SetText("Fire forest");
                }

                _eGGUIM.Unique2AbilityEnt_ButtonCom.SetActive(false);

                _eGGUIM.Unique3AbilityEnt_ButtonCom.SetActive(true);
                _eGGUIM.Unique3AbilityEnt_ButtonCom.RemoveAllListeners();
                _eGGUIM.Unique3AbilityEnt_ButtonCom.AddListener(delegate { SeedEnvironment(EnvironmentTypes.YoungForest); });
                _eGGUIM.Unique3AbilityEnt_TextMeshProGUICom.SetText("Seed Forest");
            }
        }

        else
        {
            _eGGUIM.UniqueAbilitiesZoneEnt_ParentCom.SetActive(false);
        }
    }

    private void SeedEnvironment(EnvironmentTypes environmentType)
    {
        if (!IsActivatedDoner) PhotonPunRPC.SeedEnvironmentToMaster(XySelectedCell, environmentType);
    }

    private void Fire(int[] fromXy, int[] toXy)
    {
        if (!IsActivatedDoner) PhotonPunRPC.FireToMaster(fromXy, toXy);
    }
}
