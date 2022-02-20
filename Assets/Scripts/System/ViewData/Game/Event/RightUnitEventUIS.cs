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
            if (E.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (E.UnitConditionTC(E.SelectedIdxC.Idx).Is(condUnitType))
                {
                    E.RpcE.ConditionUnitToMaster(E.SelectedIdxC.Idx, ConditionUnitTypes.None);
                }
                else
                {
                    E.RpcE.ConditionUnitToMaster(E.SelectedIdxC.Idx, condUnitType);
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void Unique(in ButtonTypes uniqueButton)
        {
            if (E.IsMyMove)
            {
                var idx_sel = E.SelectedIdxC.Idx;

                var abil = E.UnitEs(idx_sel).Ability(uniqueButton);

                if (!E.UnitEs(idx_sel).CoolDownC(abil.Ability).HaveCooldown)
                {
                    switch (abil.Ability)
                    {
                        case AbilityTypes.FirePawn:
                            E.RpcE.FirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.PutOutFirePawn:
                            E.RpcE.PutOutFirePawnToMas(idx_sel);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.Seed:
                            E.RpcE.SeedEnvToMaster(idx_sel, EnvironmentTypes.YoungForest);
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.FireArcher:
                            E.SelectedAbilityTC.Set(AbilityTypes.FireArcher);
                            E.CellClickTC.Click = CellClickTypes.UniqueAbility;
                            TryOnHint(VideoClipTypes.SeedFire);
                            break;

                        case AbilityTypes.CircularAttack:
                            E.RpcE.CircularAttackKingToMaster(idx_sel);
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
                            E.RpcE.BonusNearUnits(idx_sel);
                            TryOnHint(VideoClipTypes.BonusKing);
                            break;

                        case AbilityTypes.ChangeCornerArcher:
                            {
                                E.RpcE.ChangeCornerArchToMas(idx_sel);
                            }
                            break;

                        case AbilityTypes.GrowAdultForest:
                            E.RpcE.GrowAdultForest(idx_sel);
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
                                E.RpcE.BuildFarmToMaster(idx_sel);
                                TryOnHint(VideoClipTypes.BuldFarms);
                            }
                            break;

                        case AbilityTypes.SetCity:
                            E.RpcE.BuildCityToMaster(idx_sel);
                            break;

                        case AbilityTypes.DestroyBuilding:
                            E.RpcE.DestroyBuildingToMaster(idx_sel);
                            break;


                        case AbilityTypes.IceWall:
                            E.RpcE.IceWallToMaster(idx_sel);
                            break;

                        case AbilityTypes.ActiveAroundBonusSnowy:
                            E.RpcE.ActiveSnowyAroundToMaster(idx_sel);
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
                            E.RpcE.SetTeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.Teleport:
                            E.RpcE.TeleportToMaster(idx_sel);
                            break;

                        case AbilityTypes.InvokeSkeletons:
                            E.RpcE.InvokeSkeletonsToMaster(idx_sel);
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

