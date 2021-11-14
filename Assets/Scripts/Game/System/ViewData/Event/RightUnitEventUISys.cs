using Leopotam.Ecs;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class RightUnitEventUISys : IEcsInitSystem
    {
        private EcsFilter<ConditionUnitC> _effUnitF = default;
        private EcsFilter<UniqAbilC, CooldownUniqC> _uniqAbilF = default;

        public void Init()
        {
            UniqButtonsViewC.AddListener(UniqButtonTypes.First, delegate { UniqBut(UniqButtonTypes.First); });
            UniqButtonsViewC.AddListener(UniqButtonTypes.Second, delegate { UniqBut(UniqButtonTypes.Second); });
            UniqButtonsViewC.AddListener(UniqButtonTypes.Third, delegate { UniqBut(UniqButtonTypes.Third); });

            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.First, delegate { ExecuteBuild_Button(BuildButtonTypes.First); });
            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.Second, delegate { ExecuteBuild_Button(BuildButtonTypes.Second); });
            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.Third, delegate { ExecuteBuild_Button(BuildButtonTypes.Third); });

            ProtectUIC.AddListener(delegate { ConditionAbilityButton(CondUnitTypes.Protected); });
            RelaxUIC.AddListener(delegate { ConditionAbilityButton(CondUnitTypes.Relaxed); });
        }

        private void ConditionAbilityButton(CondUnitTypes condUnitType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                TryOnHint(VideoClipTypes.ProtRelax);

                if (_effUnitF.Get1(IdxSel.Idx).Is(condUnitType))
                {
                    RpcSys.ConditionUnitToMaster(CondUnitTypes.None, IdxSel.Idx);
                }
                else
                {
                    RpcSys.ConditionUnitToMaster(condUnitType, IdxSel.Idx);
                }
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }

        private void UniqBut(UniqButtonTypes uniqBut)
        {
            if (WhoseMoveC.IsMyMove)
            {
                ref var uniq_sel = ref _uniqAbilF.Get1(IdxSel.Idx);
                ref var cdUniq_sel = ref _uniqAbilF.Get2(IdxSel.Idx);

                var abil = uniq_sel.Ability(uniqBut);


                if (!cdUniq_sel.HaveCooldown(abil))
                {
                    switch (uniqBut)
                    {
                        case UniqButtonTypes.None: throw new Exception();

                        case UniqButtonTypes.First:
                            {
                                switch (abil)
                                {
                                    case UniqAbilTypes.None: throw new Exception();

                                    case UniqAbilTypes.FirePawn:
                                        RpcSys.FirePawnToMas(IdxSel.Idx);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.PutOutFirePawn:
                                        RpcSys.PutOutFirePawnToMas(IdxSel.Idx);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.Seed:
                                        RpcSys.SeedEnvToMaster(IdxSel.Idx, EnvTypes.YoungForest);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.FireArcher:
                                        CellClickC.Set(CellClickTypes.PickFire);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.CircularAttack:
                                        RpcSys.CircularAttackKingToMaster(IdxSel.Idx);
                                        TryOnHint(VideoClipTypes.CircularAttack);
                                        break;

                                    case UniqAbilTypes.GrowAdultForest:
                                        RpcSys.GrowAdultForest(IdxSel.Idx);
                                        TryOnHint(VideoClipTypes.GrowingAdForesElfemale);
                                        break;
                                    default: throw new Exception();
                                }
                            }
                            break;

                        case UniqButtonTypes.Second:
                            {
                                switch (abil)
                                {
                                    case UniqAbilTypes.None: throw new Exception();

                                    case UniqAbilTypes.BonusNear:
                                        RpcSys.BonusNearUnits(IdxSel.Idx);
                                        TryOnHint(VideoClipTypes.BonusKing);
                                        break;

                                    case UniqAbilTypes.StunElfemale:
                                        {
                                            CellClickC.Set(CellClickTypes.StunElfemale);
                                            TryOnHint(VideoClipTypes.StunElfemale);
                                        }
                                        break;

                                    case UniqAbilTypes.ChangeCornerArcher:
                                        {
                                            RpcSys.ChangeCornerArchToMas(IdxSel.Idx);
                                        }
                                        break;

                                    default: throw new Exception();
                                }
                            }
                            break;

                        case UniqButtonTypes.Third:
                            {
                                switch (abil)
                                {
                                    case UniqAbilTypes.None: throw new Exception();
                                    case UniqAbilTypes.PutOutFireElfemale:
                                        {
                                            TryOnHint(VideoClipTypes.PutOutElfemale);
                                            CellClickC.Set(CellClickTypes.PutOutFireElfemale);
                                        }
                                        break;
                                    default: throw new Exception();
                                }
                            }
                            break;
                        default: throw new Exception();
                    }
                }

                else SoundEffectC.Play(ClipGameTypes.Mistake);
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
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
                        RpcSys.BuildToMaster(IdxSel.Idx, BuildTypes.Farm);
                        TryOnHint(VideoClipTypes.BuldFarms);
                        break;

                    case BuildButtonTypes.Second:
                        RpcSys.BuildToMaster(IdxSel.Idx, BuildTypes.Mine);
                        TryOnHint(VideoClipTypes.BuildMine);
                        break;

                    case BuildButtonTypes.Third:
                        switch (BuildAbilC.AbilityType(buildBut))
                        {
                            case BuildAbilTypes.None: throw new Exception();
                            case BuildAbilTypes.FarmBuild: throw new Exception();
                            case BuildAbilTypes.MineBuild: throw new Exception();
                            case BuildAbilTypes.CityBuild:
                                RpcSys.BuildToMaster(IdxSel.Idx, BuildTypes.City);
                                break;

                            case BuildAbilTypes.Destroy:
                                RpcSys.DestroyBuildingToMaster(IdxSel.Idx);
                                break;

                            default: throw new Exception();
                        }
                        break;

                    default: throw new Exception();
                }
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }

        private void TryOnHint(VideoClipTypes videoClip)
        {
            if (HintComC.IsOnHint)
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

