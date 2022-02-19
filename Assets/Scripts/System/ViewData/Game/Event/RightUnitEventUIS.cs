using System;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class RightUnitEventUIS : SystemUIAbstract
    {
        internal RightUnitEventUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
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
            if (Es.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (Es.UnitConditionTC(Es.SelectedIdxC.Idx).Is(condUnitType))
                {
                    Es.RpcE.ConditionUnitToMaster(Es.SelectedIdxC.Idx, ConditionUnitTypes.None);
                }
                else
                {
                    Es.RpcE.ConditionUnitToMaster(Es.SelectedIdxC.Idx, condUnitType);
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void Unique(in ButtonTypes uniqueButton)
        {
            if (Es.IsMyMove)
            {
                var idx_sel = Es.SelectedIdxC.Idx;

                var abil = Es.UnitEs(idx_sel).Ability(uniqueButton);

                if (!Es.UnitEs(idx_sel).CoolDownC(abil.Ability).HaveCooldown)
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
                            Es.SelectedAbilityTC.Set(AbilityTypes.FireArcher);
                            Es.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.CircularAttack:
                            Es.RpcE.CircularAttackKingToMaster(idx_sel);
                            TryOnHint(VideoClipTypes.CircularAttack);
                            break;

                        case AbilityTypes.StunElfemale:
                            {
                                Es.SelectedAbilityTC.Ability = AbilityTypes.StunElfemale;
                                Es.CellClickTC.Click = CellClickTypes.UniqueAbility;
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
                                Es.SelectedAbilityTC.Ability = AbilityTypes.ChangeDirectionWind;
                                Es.CellClickTC.Click = CellClickTypes.UniqueAbility;
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
                            Es.SelectedAbilityTC.Ability = AbilityTypes.DirectWave;
                            Es.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            break;


                        case AbilityTypes.Resurrect:
                            Es.SelectedAbilityTC.Ability = AbilityTypes.Resurrect;
                            Es.CellClickTC.Click = CellClickTypes.UniqueAbility;
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

