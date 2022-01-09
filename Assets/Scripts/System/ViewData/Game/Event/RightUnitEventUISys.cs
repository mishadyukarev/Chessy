﻿using System;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    sealed class RightUnitEventUISys
    {
        internal RightUnitEventUISys()
        {
            UniqButtonsUIC.AddListener(UniqButTypes.First, delegate { UniqBut(UniqButTypes.First); });
            UniqButtonsUIC.AddListener(UniqButTypes.Second, delegate { UniqBut(UniqButTypes.Second); });
            UniqButtonsUIC.AddListener(UniqButTypes.Third, delegate { UniqBut(UniqButTypes.Third); });

            BuildAbilitUIC.AddListener_Button(BuildButtonTypes.First, delegate { ExecuteBuild_Button(BuildButtonTypes.First); });
            BuildAbilitUIC.AddListener_Button(BuildButtonTypes.Second, delegate { ExecuteBuild_Button(BuildButtonTypes.Second); });
            BuildAbilitUIC.AddListener_Button(BuildButtonTypes.Third, delegate { ExecuteBuild_Button(BuildButtonTypes.Third); });

            ProtectUIC.AddListener(delegate { ConditionAbilityButton(CondUnitTypes.Protected); });
            RelaxUIC.AddListener(delegate { ConditionAbilityButton(CondUnitTypes.Relaxed); });
        }

        private void ConditionAbilityButton(CondUnitTypes condUnitType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (Unit<ConditionC>(SelIdx<IdxC>().Idx).Is(condUnitType))
                {
                    RpcS.ConditionUnitToMaster(CondUnitTypes.None, SelIdx<IdxC>().Idx);
                }
                else
                {
                    RpcS.ConditionUnitToMaster(condUnitType, SelIdx<IdxC>().Idx);
                }
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void UniqBut(UniqButTypes uniqBut)
        {
            if (WhoseMoveC.IsMyMove)
            {
                ref var uniq_sel = ref Unit<UniqAbilC>(SelIdx<IdxC>().Idx);
                ref var cdUniq_sel = ref Unit<CooldownUniqC>(SelIdx<IdxC>().Idx);

                var abil = uniq_sel.Ability(uniqBut);


                if (!cdUniq_sel.HaveCooldown(abil))
                {
                    switch (uniqBut)
                    {
                        case UniqButTypes.None: throw new Exception();

                        case UniqButTypes.First:
                            {
                                switch (abil)
                                {
                                    case UniqueAbilTypes.None: throw new Exception();

                                    case UniqueAbilTypes.FirePawn:
                                        RpcS.FirePawnToMas(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqueAbilTypes.PutOutFirePawn:
                                        RpcS.PutOutFirePawnToMas(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqueAbilTypes.Seed:
                                        RpcS.SeedEnvToMaster(SelIdx<IdxC>().Idx, EnvTypes.YoungForest);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqueAbilTypes.FireArcher:
                                        ClickerObject<CellClickC>().Set(CellClickTypes.UniqAbil);
                                        SelUniqAbilC.UniqAbil = UniqueAbilTypes.FireArcher;
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqueAbilTypes.CircularAttack:
                                        RpcS.CircularAttackKingToMaster(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.CircularAttack);
                                        break;

                                    case UniqueAbilTypes.GrowAdultForest:
                                        RpcS.GrowAdultForest(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                                        break;
                                    default: throw new Exception();
                                }
                            }
                            break;

                        case UniqButTypes.Second:
                            {
                                switch (abil)
                                {
                                    case UniqueAbilTypes.None: throw new Exception();

                                    case UniqueAbilTypes.BonusNear:
                                        RpcS.BonusNearUnits(SelIdx<IdxC>().Idx);
                                        TryOnHint(VideoClipTypes.BonusKing);
                                        break;

                                    case UniqueAbilTypes.StunElfemale:
                                        {
                                            ClickerObject<CellClickC>().Set(CellClickTypes.UniqAbil);
                                            SelUniqAbilC.UniqAbil = UniqueAbilTypes.StunElfemale;
                                            TryOnHint(VideoClipTypes.StunElfemale);
                                        }
                                        break;

                                    case UniqueAbilTypes.ChangeCornerArcher:
                                        {
                                            RpcS.ChangeCornerArchToMas(SelIdx<IdxC>().Idx);
                                        }
                                        break;

                                    default: throw new Exception();
                                }
                            }
                            break;

                        case UniqButTypes.Third:
                            {
                                switch (abil)
                                {
                                    case UniqueAbilTypes.None: throw new Exception();
                                    case UniqueAbilTypes.ChangeDirWind:
                                        {
                                            TryOnHint(VideoClipTypes.PutOutElfemale);
                                            ClickerObject<CellClickC>().Set(CellClickTypes.UniqAbil);
                                            SelUniqAbilC.UniqAbil = UniqueAbilTypes.ChangeDirWind;
                                        }
                                        break;
                                    default: throw new Exception();
                                }
                            }
                            break;
                        default: throw new Exception();
                    }
                }

                else SoundEffectVC.Play(ClipTypes.Mistake);
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void ExecuteBuild_Button(BuildButtonTypes buildBut)
        {
            if (WhoseMoveC.IsMyMove)
            {
                switch (buildBut)
                {
                    case BuildButtonTypes.None:
                        throw new Exception();

                    case BuildButtonTypes.First:
                        RpcS.BuildToMaster(SelIdx<IdxC>().Idx, BuildTypes.Farm);
                        TryOnHint(VideoClipTypes.BuldFarms);
                        break;

                    case BuildButtonTypes.Second:
                        RpcS.BuildToMaster(SelIdx<IdxC>().Idx, BuildTypes.Mine);
                        TryOnHint(VideoClipTypes.BuildMine);
                        break;

                    case BuildButtonTypes.Third:
                        switch (BuildAbilC.AbilityType(buildBut))
                        {
                            case BuildAbilTypes.None: throw new Exception();
                            case BuildAbilTypes.FarmBuild: throw new Exception();
                            case BuildAbilTypes.MineBuild: throw new Exception();
                            case BuildAbilTypes.CityBuild:
                                RpcS.BuildToMaster(SelIdx<IdxC>().Idx, BuildTypes.City);
                                break;

                            case BuildAbilTypes.Destroy:
                                RpcS.DestroyBuildingToMaster(SelIdx<IdxC>().Idx);
                                break;

                            default: throw new Exception();
                        }
                        break;

                    default: throw new Exception();
                }
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void TryOnHint(VideoClipTypes videoClip)
        {
            if (Common.HintC.IsOnHint)
            {
                if (!HintC.WasActived(videoClip))
                {
                    HintViewUIC.SetActiveHintZone(true);
                    HintViewUIC.SetVideoClip(videoClip);
                    HintC.SetWasActived(videoClip, true);
                }
            }
        }
    }
}

