using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    public sealed class RightUnitEventUISys : IEcsInitSystem
    {
        private EcsFilter<CellUnitDataCom, ConditionUnitC> _cellUnitFilter = default;

        public void Init()
        {
            RightUniqueViewUIC.AddListener_Button(UniqueButtonTypes.First, delegate { ExecuteUnique_Button(UniqueButtonTypes.First); });
            RightUniqueViewUIC.AddListener_Button(UniqueButtonTypes.Second, delegate { ExecuteUnique_Button(UniqueButtonTypes.Second); });

            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.First, delegate { ExecuteBuild_Button(BuildButtonTypes.First); });
            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.Second, delegate { ExecuteBuild_Button(BuildButtonTypes.Second); });
            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.Third, delegate { ExecuteBuild_Button(BuildButtonTypes.Third); });

            CondUnitUIC.AddListener(CondUnitTypes.Protected, delegate { ConditionAbilityButton(CondUnitTypes.Protected); });
            CondUnitUIC.AddListener(CondUnitTypes.Relaxed, delegate { ConditionAbilityButton(CondUnitTypes.Relaxed); });
        }

        private void ConditionAbilityButton(CondUnitTypes condUnitType)
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

            if (WhoseMoveC.IsMyMove)
            {
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

        private void ExecuteUnique_Button(UniqueButtonTypes uniqueButtonType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                switch (uniqueButtonType)
                {
                    case UniqueButtonTypes.None: throw new Exception();
                    case UniqueButtonTypes.First:
                        {
                            var abil = RightUniqueDataUIC.AbilityType(UniqueButtonTypes.First);
                            switch (abil)
                            {
                                case UniqueAbilTypes.None: throw new Exception();
                                case UniqueAbilTypes.FirePawn:
                                    RpcSys.FireToMaster(SelectorC.IdxSelCell, SelectorC.IdxSelCell);
                                    break;
                                case UniqueAbilTypes.NoneFirePawn:
                                    RpcSys.FireToMaster(SelectorC.IdxSelCell, SelectorC.IdxSelCell);
                                    break;
                                case UniqueAbilTypes.FireArcher:
                                    SelectorC.CellClickType = CellClickTypes.PickFire;
                                    break;
                                case UniqueAbilTypes.Seed:
                                    RpcSys.SeedEnvironmentToMaster(SelectorC.IdxSelCell, EnvTypes.YoungForest);

                                    break;
                                case UniqueAbilTypes.CircularAttack:
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
                                default: throw new Exception();
                            }

                            if (HintComC.IsOnHint)
                            {
                                if (abil == UniqueAbilTypes.FireArcher
                                || abil == UniqueAbilTypes.Seed
                                || abil == UniqueAbilTypes.FirePawn
                                || abil == UniqueAbilTypes.None)
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
                    case UniqueButtonTypes.Second:
                        {
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
                        }
                        break;
                    case UniqueButtonTypes.Third:
                        break;
                    default: throw new Exception();
                }
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

