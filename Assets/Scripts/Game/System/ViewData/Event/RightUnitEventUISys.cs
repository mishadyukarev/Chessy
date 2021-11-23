using Leopotam.Ecs;
using Game.Common;
using System;

namespace Game.Game
{
    public sealed class RightUnitEventUISys : IEcsInitSystem
    {
        private EcsFilter<ConditionUnitC> _effUnitF = default;
        private EcsFilter<UniqAbilC, CooldownUniqC> _uniqAbilF = default;

        public void Init()
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

                if (_effUnitF.Get1(SelIdx.Idx).Is(condUnitType))
                {
                    RpcSys.ConditionUnitToMaster(CondUnitTypes.None, SelIdx.Idx);
                }
                else
                {
                    RpcSys.ConditionUnitToMaster(condUnitType, SelIdx.Idx);
                }
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void UniqBut(UniqButTypes uniqBut)
        {
            if (WhoseMoveC.IsMyMove)
            {
                ref var uniq_sel = ref _uniqAbilF.Get1(SelIdx.Idx);
                ref var cdUniq_sel = ref _uniqAbilF.Get2(SelIdx.Idx);

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
                                    case UniqAbilTypes.None: throw new Exception();

                                    case UniqAbilTypes.FirePawn:
                                        RpcSys.FirePawnToMas(SelIdx.Idx);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.PutOutFirePawn:
                                        RpcSys.PutOutFirePawnToMas(SelIdx.Idx);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.Seed:
                                        RpcSys.SeedEnvToMaster(SelIdx.Idx, EnvTypes.YoungForest);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.FireArcher:
                                        CellClickC.Set(CellClickTypes.UniqAbil);
                                        SelUniqAbilC.UniqAbil = UniqAbilTypes.FireArcher;
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.CircularAttack:
                                        RpcSys.CircularAttackKingToMaster(SelIdx.Idx);
                                        TryOnHint(VideoClipTypes.CircularAttack);
                                        break;

                                    case UniqAbilTypes.GrowAdultForest:
                                        RpcSys.GrowAdultForest(SelIdx.Idx);
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
                                    case UniqAbilTypes.None: throw new Exception();

                                    case UniqAbilTypes.BonusNear:
                                        RpcSys.BonusNearUnits(SelIdx.Idx);
                                        TryOnHint(VideoClipTypes.BonusKing);
                                        break;

                                    case UniqAbilTypes.StunElfemale:
                                        {
                                            CellClickC.Set(CellClickTypes.UniqAbil);
                                            SelUniqAbilC.UniqAbil = UniqAbilTypes.StunElfemale;
                                            TryOnHint(VideoClipTypes.StunElfemale);
                                        }
                                        break;

                                    case UniqAbilTypes.ChangeCornerArcher:
                                        {
                                            RpcSys.ChangeCornerArchToMas(SelIdx.Idx);
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
                                    case UniqAbilTypes.None: throw new Exception();
                                    case UniqAbilTypes.ChangeDirWind:
                                        {
                                            TryOnHint(VideoClipTypes.PutOutElfemale);
                                            CellClickC.Set(CellClickTypes.UniqAbil);
                                            SelUniqAbilC.UniqAbil = UniqAbilTypes.ChangeDirWind;
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
                        RpcSys.BuildToMaster(SelIdx.Idx, BuildTypes.Farm);
                        TryOnHint(VideoClipTypes.BuldFarms);
                        break;

                    case BuildButtonTypes.Second:
                        RpcSys.BuildToMaster(SelIdx.Idx, BuildTypes.Mine);
                        TryOnHint(VideoClipTypes.BuildMine);
                        break;

                    case BuildButtonTypes.Third:
                        switch (BuildAbilC.AbilityType(buildBut))
                        {
                            case BuildAbilTypes.None: throw new Exception();
                            case BuildAbilTypes.FarmBuild: throw new Exception();
                            case BuildAbilTypes.MineBuild: throw new Exception();
                            case BuildAbilTypes.CityBuild:
                                RpcSys.BuildToMaster(SelIdx.Idx, BuildTypes.City);
                                break;

                            case BuildAbilTypes.Destroy:
                                RpcSys.DestroyBuildingToMaster(SelIdx.Idx);
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

