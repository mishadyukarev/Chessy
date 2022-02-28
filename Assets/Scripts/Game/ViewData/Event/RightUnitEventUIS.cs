using System;

namespace Chessy.Game
{
    sealed class RightUnitEventUIS : SystemUIAbstract
    {
        internal RightUnitEventUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            UIEs.RightEs.Unique(ButtonTypes.First).Button.AddListener(delegate { Unique(ButtonTypes.First); });
            UIEs.RightEs.Unique(ButtonTypes.Second).Button.AddListener(delegate { Unique(ButtonTypes.Second); });
            UIEs.RightEs.Unique(ButtonTypes.Third).Button.AddListener(delegate { Unique(ButtonTypes.Third); });
            UIEs.RightEs.Unique(ButtonTypes.Fourth).Button.AddListener(delegate { Unique(ButtonTypes.Fourth); });
            UIEs.RightEs.Unique(ButtonTypes.Fifth).Button.AddListener(delegate { Unique(ButtonTypes.Fifth); });

            UIEs.RightEs.ProtectE.ButtonUIC.AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            UIEs.RightEs.ProtectE.ButtonUIC.AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (E.UnitConditionTC(E.SelectedIdxC.Idx).Is(condUnitType))
                {
                    E.RpcPoolEs.ConditionUnitToMaster(E.SelectedIdxC.Idx, ConditionUnitTypes.None);
                }
                else
                {
                    E.RpcPoolEs.ConditionUnitToMaster(E.SelectedIdxC.Idx, condUnitType);
                }
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        void Unique(in ButtonTypes uniqueButton)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                var idx_sel = E.SelectedIdxC.Idx;

                var abil = E.UnitEs(idx_sel).Ability(uniqueButton);

                if (!E.UnitEs(idx_sel).CoolDownC(abil.Ability).HaveCooldown)
                {
                    switch (abil.Ability)
                    {
                        case AbilityTypes.FirePawn:
                            E.RpcPoolEs.FirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            E.RpcPoolEs.PutOutFirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.Seed:
                            E.RpcPoolEs.SeedEnvToMaster(idx_sel, EnvironmentTypes.YoungForest);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.FireArcher:
                            E.SelectedAbilityTC.Set(AbilityTypes.FireArcher);
                            E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.CircularAttack:
                            E.RpcPoolEs.CircularAttackKingToMaster(idx_sel);
                            TryOnHint(VideoClipTypes.CircularAttack);
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                E.SelectedAbilityTC.Ability = AbilityTypes.StunElfemale;
                                E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                                TryOnHint(VideoClipTypes.StunElfemale);
                            }
                            break;

                        case AbilityTypes.BonusNear:
                            E.RpcPoolEs.BonusNearUnits(idx_sel);
                            TryOnHint(VideoClipTypes.BonusKing);
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            {
                                E.RpcPoolEs.ChangeCornerArchToMas(idx_sel);
                            }
                            break;

                        case AbilityTypes.GrowAdultForest:
                            E.RpcPoolEs.GrowAdultForest(idx_sel);
                            TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                TryOnHint(VideoClipTypes.PutOutElfemale);
                                E.SelectedAbilityTC.Ability = AbilityTypes.ChangeDirectionWind;
                                E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            }
                            break;

                        case AbilityTypes.SetFarm:
                            {
                                E.RpcPoolEs.BuildFarmToMaster(idx_sel);
                                TryOnHint(VideoClipTypes.BuldFarms);
                            }
                            break;

                        case AbilityTypes.SetCity:
                            E.RpcPoolEs.BuildCityToMaster(idx_sel);
                            break;

                        case AbilityTypes.DestroyBuilding:
                            E.RpcPoolEs.DestroyBuildingToMaster(idx_sel);
                            break;


                        case AbilityTypes.IceWall:
                            E.RpcPoolEs.IceWallToMaster(idx_sel);
                            break;

                        case AbilityTypes.ActiveAroundBonusSnowy:
                            E.RpcPoolEs.ActiveSnowyAroundToMaster(idx_sel);
                            break;

                        case AbilityTypes.DirectWave:
                            E.SelectedAbilityTC.Ability = AbilityTypes.DirectWave;
                            E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            break;


                        case AbilityTypes.Resurrect:
                            E.SelectedAbilityTC.Ability = AbilityTypes.Resurrect;
                            E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            break;

                        case AbilityTypes.SetTeleport:
                            E.RpcPoolEs.SetTeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.Teleport:
                            E.RpcPoolEs.TeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.InvokeSkeletons:
                            E.RpcPoolEs.InvokeSkeletonsToMaster(idx_sel);
                            break;

                        default: throw new Exception();
                    }
                }

                else E.Sound(ClipTypes.Mistake).Action.Invoke();
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        void TryOnHint(VideoClipTypes videoClip)
        {
            if (Common.HintC.IsOnHint)
            {
                //if (!HintC.WasActived(videoClip))
                //{
                //    //EntityCenterHintUIPool.SetActiveHintZone(true);
                //    //EntityCenterHintUIPool.SetVideoClip(videoClip);
                //    HintC.SetWasActived(videoClip, true);
                //}
            }
        }
    }
}

