using System;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class RightUnitEventUIS : SystemUIAbstract
    {
        internal RightUnitEventUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
            UIEs.RightEs.Unique(ButtonTypes.First).Button.AddListener(delegate { Unique(ButtonTypes.First); });
            UIEs.RightEs.Unique(ButtonTypes.Second).Button.AddListener(delegate { Unique(ButtonTypes.Second); });
            UIEs.RightEs.Unique(ButtonTypes.Third).Button.AddListener(delegate { Unique(ButtonTypes.Third); });
            UIEs.RightEs.Unique(ButtonTypes.Fourth).Button.AddListener(delegate { Unique(ButtonTypes.Fourth); });
            UIEs.RightEs.Unique(ButtonTypes.Fifth).Button.AddListener(delegate { Unique(ButtonTypes.Fifth); });

            RightProtectUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            RightRelaxUIE.Button<ButtonUIC>().AddListener(delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });
        }

        void ConditionAbilityButton(ConditionUnitTypes condUnitType)
        {
            if (Es.WhoseMoveE.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (UnitEs(Es.SelectedIdxE.IdxC.Idx).ConditionE.ConditionTC.Is(condUnitType))
                {
                    Es.RpcE.ConditionUnitToMaster(Es.SelectedIdxE.IdxC.Idx, ConditionUnitTypes.None);
                }
                else
                {
                    Es.RpcE.ConditionUnitToMaster(Es.SelectedIdxE.IdxC.Idx, condUnitType);
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void Unique(in ButtonTypes uniqueButton)
        {
            if (Es.WhoseMoveE.IsMyMove)
            {
                var idx_sel = Es.SelectedIdxE.IdxC.Idx;

                var abil = UnitEs(idx_sel).AbilityButton(uniqueButton).AbilityC;

                if (!UnitEs(idx_sel).Ability(abil.Ability).HaveCooldown)
                {
                    switch (abil.Ability)
                    {
                        case AbilityTypes.FirePawn:
                            Es.RpcE.FirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            Es.RpcE.PutOutFirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.Seed:
                            Es.RpcE.SeedEnvToMaster(idx_sel, EnvironmentTypes.YoungForest);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.FireArcher:
                            Es.SelectedAbilityE.SetAbility(AbilityTypes.FireArcher, Es.ClickerObjectE);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.CircularAttack:
                            Es.RpcE.CircularAttackKingToMaster(idx_sel);
                            TryOnHint(VideoClipTypes.CircularAttack);
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                Es.SelectedAbilityE.SetAbility(AbilityTypes.StunElfemale, Es.ClickerObjectE);
                                TryOnHint(VideoClipTypes.StunElfemale);
                            }
                            break;

                        case AbilityTypes.BonusNear:
                            Es.RpcE.BonusNearUnits(idx_sel);
                            TryOnHint(VideoClipTypes.BonusKing);
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            {
                                Es.RpcE.ChangeCornerArchToMas(idx_sel);
                            }
                            break;

                        case AbilityTypes.GrowAdultForest:
                            Es.RpcE.GrowAdultForest(idx_sel);
                            TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                            break;

                        case AbilityTypes.ChangeDirectionWind:
                            {
                                TryOnHint(VideoClipTypes.PutOutElfemale);
                                Es.SelectedAbilityE.SetAbility(AbilityTypes.ChangeDirectionWind, Es.ClickerObjectE);
                            }
                            break;

                        case AbilityTypes.SetFarm:
                            {
                                Es.RpcE.BuildFarmToMaster(idx_sel);
                                TryOnHint(VideoClipTypes.BuldFarms);
                            }
                            break;

                        case AbilityTypes.SetCity:
                            Es.RpcE.BuildCityToMaster(idx_sel);
                            break;

                        case AbilityTypes.DestroyBuilding:
                            Es.RpcE.DestroyBuildingToMaster(idx_sel);
                            break;


                        case AbilityTypes.IceWall:
                            Es.RpcE.IceWallToMaster(idx_sel);
                            break;

                        case AbilityTypes.ActiveAroundBonusSnowy:
                            Es.RpcE.ActiveSnowyAroundToMaster(idx_sel);
                            break;

                        case AbilityTypes.DirectWave:
                            Es.SelectedAbilityE.SetAbility(AbilityTypes.DirectWave, Es.ClickerObjectE);
                            break;


                        case AbilityTypes.Resurrect:
                            Es.SelectedAbilityE.SetAbility(AbilityTypes.Resurrect, Es.ClickerObjectE);
                            break;

                        case AbilityTypes.SetTeleport:
                            Es.RpcE.SetTeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.Teleport:
                            Es.RpcE.TeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.InvokeSkeletons:
                            Es.RpcE.InvokeSkeletonsToMaster(idx_sel);
                            break;

                        default: throw new Exception();
                    }
                }

                else SoundV(ClipTypes.Mistake).Play();
            }
            else SoundV(ClipTypes.Mistake).Play();
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

