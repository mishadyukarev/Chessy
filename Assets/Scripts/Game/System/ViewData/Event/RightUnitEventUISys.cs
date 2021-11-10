using Leopotam.Ecs;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class RightUnitEventUISys : IEcsInitSystem
    {
        private EcsFilter<CellUnitDataC, ConditionUnitC> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataC, UniqAbilC> _unitUniqFilt = default;

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
                if (HintComC.IsOnHint)
                {
                    if (!HintDataUIC.IsActive(VideoClipTypes.ProtRelax))
                    {
                        HintViewUIC.SetActiveHintZone(true);
                        HintViewUIC.SetVideoClip(VideoClipTypes.ProtRelax);
                        HintDataUIC.SetActive(VideoClipTypes.ProtRelax, true);
                    }
                }

                if (_cellUnitFilter.Get2(SelectorC.IdxSelCell).Is(condUnitType))
                {
                    RpcSys.ConditionUnitToMaster(CondUnitTypes.None, SelectorC.IdxSelCell);
                }
                else
                {
                    RpcSys.ConditionUnitToMaster(condUnitType, SelectorC.IdxSelCell);
                }
            }
        }

        private void UniqBut(UniqButtonTypes uniqBut)
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
                                    break;

                                case UniqAbilTypes.PutOutFirePawn:
                                    RpcSys.PutOutFirePawnToMas(SelectorC.IdxSelCell);
                                    break;

                                case UniqAbilTypes.Seed:
                                    RpcSys.SeedEnvToMaster(SelectorC.IdxSelCell, EnvTypes.YoungForest);
                                    break;

                                case UniqAbilTypes.FireArcher:
                                    SelectorC.Set(CellClickTypes.PickFire);
                                    break;

                                case UniqAbilTypes.CircularAttack:
                                    RpcSys.CircularAttackKingToMaster(SelectorC.IdxSelCell);
                                    if (HintComC.IsOnHint)
                                    {
                                        if (!HintDataUIC.IsActive(VideoClipTypes.CircularAttack))
                                        {
                                            HintViewUIC.SetActiveHintZone(true);
                                            HintViewUIC.SetVideoClip(VideoClipTypes.CircularAttack);
                                            HintDataUIC.SetActive(VideoClipTypes.CircularAttack, true);
                                        }
                                    }
                                    break;

                                case UniqAbilTypes.BonusNear: throw new Exception();

                                case UniqAbilTypes.GrowAdultForest:
                                    RpcSys.GrowAdultForest(SelectorC.IdxSelCell);
                                    break;
                                default: throw new Exception();
                            }

                            if (HintComC.IsOnHint)
                            {
                                if (abil == UniqAbilTypes.FireArcher
                                || abil == UniqAbilTypes.Seed
                                || abil == UniqAbilTypes.FirePawn
                                || abil == UniqAbilTypes.PutOutFirePawn)
                                {
                                    if (!HintDataUIC.IsActive(VideoClipTypes.SeedFire))
                                    {
                                        HintViewUIC.SetActiveHintZone(true);
                                        HintViewUIC.SetVideoClip(VideoClipTypes.SeedFire);
                                        HintDataUIC.SetActive(VideoClipTypes.SeedFire, true);
                                    }
                                }
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
                                    if (HintComC.IsOnHint)
                                    {
                                        if (!HintDataUIC.IsActive(VideoClipTypes.BonusKing))
                                        {
                                            HintViewUIC.SetActiveHintZone(true);
                                            HintViewUIC.SetVideoClip(VideoClipTypes.BonusKing);
                                            HintDataUIC.SetActive(VideoClipTypes.BonusKing, true);
                                        }
                                    }
                                    break;

                                case UniqAbilTypes.StunElfemale:
                                    {
                                        SelectorC.Set(CellClickTypes.StunElfemale);
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
                        if (HintComC.IsOnHint)
                        {
                            if (!HintDataUIC.IsActive(VideoClipTypes.BuldFarms))
                            {
                                HintViewUIC.SetActiveHintZone(true);
                                HintViewUIC.SetVideoClip(VideoClipTypes.BuldFarms);
                                HintDataUIC.SetActive(VideoClipTypes.BuldFarms, true);
                            }
                        }
                        break;

                    case BuildButtonTypes.Second:
                        RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildTypes.Mine);
                        if (HintComC.IsOnHint)
                        {
                            if (!HintDataUIC.IsActive(VideoClipTypes.BuildMine))
                            {
                                HintViewUIC.SetActiveHintZone(true);
                                HintViewUIC.SetVideoClip(VideoClipTypes.BuildMine);
                                HintDataUIC.SetActive(VideoClipTypes.BuildMine, true);
                            }
                        }
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
        }
    }
}

