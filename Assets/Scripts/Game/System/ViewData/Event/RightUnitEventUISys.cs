using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    public sealed class RightUnitEventUISys : IEcsInitSystem
    {
        private EcsFilter<CellUnitDataC, ConditionUnitC> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataC, Uniq1C, Uniq2C> _unitUniqFilt = default;

        public void Init()
        {
            UniqButtonsViewC.AddListener(UniqButtonTypes.First, UniqFirstBut);
            UniqButtonsViewC.AddListener(UniqButtonTypes.Second, UniqSecBut);

            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.First, delegate { ExecuteBuild_Button(BuildButtonTypes.First); });
            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.Second, delegate { ExecuteBuild_Button(BuildButtonTypes.Second); });
            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.Third, delegate { ExecuteBuild_Button(BuildButtonTypes.Third); });

            CondUnitUIC.AddListener(CondUnitTypes.Protected, delegate { ConditionAbilityButton(CondUnitTypes.Protected); });
            CondUnitUIC.AddListener(CondUnitTypes.Relaxed, delegate { ConditionAbilityButton(CondUnitTypes.Relaxed); });
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

        private void UniqFirstBut()
        {
            ref var firstUniq_sel = ref _unitUniqFilt.Get2(SelectorC.IdxSelCell);

            var abil = firstUniq_sel.Ability;

            switch (firstUniq_sel.Ability)
            {
                case UniqAbilTypes.None: throw new Exception();

                case UniqAbilTypes.FirePawn:
                    RpcSys.FireToMaster(SelectorC.IdxSelCell, SelectorC.IdxSelCell);
                    break;

                case UniqAbilTypes.NoneFirePawn:
                    RpcSys.FireToMaster(SelectorC.IdxSelCell, SelectorC.IdxSelCell);
                    break;

                case UniqAbilTypes.Seed:
                    RpcSys.SeedEnvironmentToMaster(SelectorC.IdxSelCell, EnvTypes.YoungForest);
                    break;

                case UniqAbilTypes.FireArcher:
                    SelectorC.CellClickType = CellClickTypes.PickFire;
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
                default: throw new Exception();
            }

            if (HintComC.IsOnHint)
            {
                if (abil == UniqAbilTypes.FireArcher
                || abil == UniqAbilTypes.Seed
                || abil == UniqAbilTypes.FirePawn
                || abil == UniqAbilTypes.NoneFirePawn)
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

        private void UniqSecBut()
        {
            ref var secUniq_sel = ref _unitUniqFilt.Get3(SelectorC.IdxSelCell);

            var abil = secUniq_sel.Ability;

            switch (abil)
            {
                case UniqAbilTypes.None: throw new Exception();
                case UniqAbilTypes.FirePawn: throw new Exception();
                case UniqAbilTypes.NoneFirePawn: throw new Exception();
                case UniqAbilTypes.Seed: throw new Exception();
                case UniqAbilTypes.FireArcher: throw new Exception();
                case UniqAbilTypes.CircularAttack: throw new Exception();

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

                default: throw new Exception();
            }
        }

        private void ExecuteBuild_Button(BuildButtonTypes buildButtonType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                switch (buildButtonType)
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
                        switch (BuildAbilitDataUIC.AbilityType(buildButtonType))
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

