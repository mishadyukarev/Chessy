using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    internal sealed class RightUnitEventUISys : IEcsInitSystem
    {
        private EcsFilter<CellUnitDataCom, ConditionUnitC> _cellUnitFilter = default;

        public void Init()
        {
            RightUniqueViewUIC.AddListener_Button(UniqueButtonTypes.First, delegate { ExecuteUniqueButton(UniqueButtonTypes.First); });
            RightUniqueViewUIC.AddListener_Button(UniqueButtonTypes.Second, delegate { ExecuteUniqueButton(UniqueButtonTypes.Second); });

            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.First, delegate { ExecuteButton(BuildButtonTypes.First); });
            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.Second, delegate { ExecuteButton(BuildButtonTypes.Second); });
            BuildAbilitViewUIC.AddListener_Button(BuildButtonTypes.Third, delegate { ExecuteButton(BuildButtonTypes.Third); });

            CondUnitUIC.AddListener(CondUnitTypes.Protected, delegate { ConditionAbilityButton(CondUnitTypes.Protected); });
            CondUnitUIC.AddListener(CondUnitTypes.Relaxed, delegate { ConditionAbilityButton(CondUnitTypes.Relaxed); });
        }

        private void ConditionAbilityButton(CondUnitTypes condUnitType)
        {
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

        private void ExecuteUniqueButton(UniqueButtonTypes uniqueButtonType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                switch (uniqueButtonType)
                {
                    case UniqueButtonTypes.None: throw new Exception();
                    case UniqueButtonTypes.First:
                        {
                            switch (RightUniqueDataUIC.AbilityType(UniqueButtonTypes.First))
                            {
                                case AbilityTypes.None: throw new Exception();
                                case AbilityTypes.FirePawn: 
                                    RpcSys.FireToMaster(SelectorC.IdxSelCell, SelectorC.IdxSelCell); 
                                    break;
                                case AbilityTypes.NoneFirePawn: 
                                    RpcSys.FireToMaster(SelectorC.IdxSelCell, SelectorC.IdxSelCell); 
                                    break;
                                case AbilityTypes.FireArcher: 
                                    SelectorC.CellClickType = CellClickTypes.PickFire; 
                                    break;
                                case AbilityTypes.Seed: 
                                    RpcSys.SeedEnvironmentToMaster(SelectorC.IdxSelCell, EnvirTypes.YoungForest); 
                                    break;
                                case AbilityTypes.CircularAttack: 
                                    RpcSys.CircularAttackKingToMaster(SelectorC.IdxSelCell); 
                                    break;
                                case AbilityTypes.FarmBuild: throw new Exception();
                                case AbilityTypes.MineBuild: throw new Exception();
                                case AbilityTypes.CityBuild: throw new Exception();
                                case AbilityTypes.Destroy: throw new Exception();
                                default: throw new Exception();
                            }
                        }
                        break;
                    case UniqueButtonTypes.Second:
                        {
                            RpcSys.BonusNearUnits(SelectorC.IdxSelCell);
                            //switch (RightUniqueDataUIC.AbilityType(UniqueButtonTypes.Second))
                            //{
                            //    case AbilityTypes.None: throw new Exception();
                            //    case AbilityTypes.FirePawn:
                            //        RpcSys.FireToMaster(SelectorC.IdxSelCell, SelectorC.IdxSelCell);
                            //        break;
                            //    case AbilityTypes.NoneFirePawn:
                            //        RpcSys.FireToMaster(SelectorC.IdxSelCell, SelectorC.IdxSelCell);
                            //        break;
                            //    case AbilityTypes.FireArcher:
                            //        SelectorC.CellClickType = CellClickTypes.PickFire;
                            //        break;
                            //    case AbilityTypes.Seed:
                            //        RpcSys.SeedEnvironmentToMaster(SelectorC.IdxSelCell, EnvirTypes.YoungForest);
                            //        break;
                            //    case AbilityTypes.CircularAttack:
                            //        RpcSys.CircularAttackKingToMaster(SelectorC.IdxSelCell);
                            //        break;
                            //    case AbilityTypes.FarmBuild: throw new Exception();
                            //    case AbilityTypes.MineBuild: throw new Exception();
                            //    case AbilityTypes.CityBuild: throw new Exception();
                            //    case AbilityTypes.Destroy: throw new Exception();
                            //    default: throw new Exception();
                            //}
                        }
                        break;
                    case UniqueButtonTypes.Third:
                        break;
                    default: throw new Exception();
                }
            }
        }

        private void ExecuteButton(BuildButtonTypes buildButtonType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                switch (buildButtonType)
                {
                    case BuildButtonTypes.None:
                        throw new Exception();

                    case BuildButtonTypes.First:
                        RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildingTypes.Farm);
                        break;

                    case BuildButtonTypes.Second:
                        RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildingTypes.Mine);
                        break;

                    case BuildButtonTypes.Third:
                        switch (BuildAbilitDataUIC.AbilityType(buildButtonType))
                        {
                            case AbilityTypes.CityBuild:
                                {
                                    RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildingTypes.City); 
                                }
                                break;
                            case AbilityTypes.Destroy:
                                {
                                    RpcSys.DestroyBuildingToMaster(SelectorC.IdxSelCell);
                                }
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

