using Leopotam.Ecs;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class RightUnitEventUISys : IEcsInitSystem
    {
        private EcsFilter<UnitC, ConditionUnitC> _cellUnitFilter = default;
        private EcsFilter<UnitC, UniqAbilC> _unitUniqFilt = default;

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

                if (_cellUnitFilter.Get2(SelectorC.IdxSelCell).Is(condUnitType))
                {
                    RpcSys.ConditionUnitToMaster(CondUnitTypes.None, SelectorC.IdxSelCell);
                }
                else
                {
                    RpcSys.ConditionUnitToMaster(condUnitType, SelectorC.IdxSelCell);
                }
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }

        private void UniqBut(UniqButtonTypes uniqBut)
        {
            if (WhoseMoveC.IsMyMove)
            {
                ref var uniq_sel = ref _unitUniqFilt.Get2(SelectorC.IdxSelCell);
                var abil = uniq_sel.Ability(uniqBut);


                if (!uniq_sel.HaveCooldown(abil))
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
                                        RpcSys.FirePawnToMas(SelectorC.IdxSelCell);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.PutOutFirePawn:
                                        RpcSys.PutOutFirePawnToMas(SelectorC.IdxSelCell);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.Seed:
                                        RpcSys.SeedEnvToMaster(SelectorC.IdxSelCell, EnvTypes.YoungForest);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.FireArcher:
                                        SelectorC.Set(CellClickTypes.PickFire);
                                        TryOnHint(VideoClipTypes.SeedFire);
                                        break;

                                    case UniqAbilTypes.CircularAttack:
                                        RpcSys.CircularAttackKingToMaster(SelectorC.IdxSelCell);
                                        TryOnHint(VideoClipTypes.CircularAttack);
                                        break;

                                    case UniqAbilTypes.GrowAdultForest:
                                        RpcSys.GrowAdultForest(SelectorC.IdxSelCell);
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
                                        RpcSys.BonusNearUnits(SelectorC.IdxSelCell);
                                        TryOnHint(VideoClipTypes.BonusKing);
                                        break;

                                    case UniqAbilTypes.StunElfemale:
                                        {
                                            SelectorC.Set(CellClickTypes.StunElfemale);
                                            TryOnHint(VideoClipTypes.StunElfemale);
                                        }
                                        break;

                                    case UniqAbilTypes.ChangeCornerArcher:
                                        {
                                            RpcSys.ChangeCornerArchToMas(SelectorC.IdxSelCell);
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
                                            SelectorC.Set(CellClickTypes.PutOutFireElfemale);
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
                        RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildTypes.Farm);
                        TryOnHint(VideoClipTypes.BuldFarms);
                        break;

                    case BuildButtonTypes.Second:
                        RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildTypes.Mine);
                        TryOnHint(VideoClipTypes.BuildMine);
                        break;

                    case BuildButtonTypes.Third:
                        switch (BuildAbilitDataUIC.AbilityType(buildBut))
                        {
                            case BuildAbilTypes.None: throw new Exception();
                            case BuildAbilTypes.FarmBuild: throw new Exception();
                            case BuildAbilTypes.MineBuild: throw new Exception();
                            case BuildAbilTypes.CityBuild:
                                RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildTypes.City);
                                break;

                            case BuildAbilTypes.Destroy:
                                RpcSys.DestroyBuildingToMaster(SelectorC.IdxSelCell);
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
                if (!HintDataUIC.WasActived(videoClip))
                {
                    HintViewUIC.SetActiveHintZone(true);
                    HintViewUIC.SetVideoClip(videoClip);
                    HintDataUIC.SetWasActived(videoClip, true);
                }
            }
        }
    }
}

