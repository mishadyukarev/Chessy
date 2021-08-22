using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.Supports;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC.GiveTake
{
    internal sealed class PawnGiveTakeToolWeapMastSys : IEcsRunSystem
    {
        private byte _woodCostForPick = 5;
        private byte _ironCostForSword = 1;

        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGiveTakeToolWeapFilter = default;

        private EcsFilter<InventorResourcesComponent> _inventResFilter = default;
        private EcsFilter<InventorToolsComp, InventorWeaponsComp> _inventToolWeapFilter = default;

        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

        public void Run()
        {
            var neededIdxCell = _forGiveTakeToolWeapFilter.Get1(0).IdxCell;

            if (neededIdxCell != default)
            {
                var toolWeapTypeForGive = _forGiveTakeToolWeapFilter.Get1(0).ToolWeapType;

                ref var inventToolsCom = ref _inventToolWeapFilter.Get1(0);
                ref var inventWeaponsComp = ref _inventToolWeapFilter.Get2(0);
                ref var inventResCom = ref _inventResFilter.Get1(0);

                var sender = _infoFilter.Get1(0).FromInfo.Sender;

                ref var cellUnitDataComForGive = ref _cellUnitFilter.Get1(neededIdxCell);
                ref var ownerCellUnitComForGive = ref _cellUnitFilter.Get2(neededIdxCell);

                if (cellUnitDataComForGive.IsUnitType(UnitTypes.Pawn))
                {
                    if (cellUnitDataComForGive.HaveExtraToolWeapon)
                    {
                        if (cellUnitDataComForGive.HaveMaxAmountHealth)
                        {
                            if (cellUnitDataComForGive.HaveMaxAmountSteps)
                            {
                                if (cellUnitDataComForGive.IsConditionType(new[] { ConditionUnitTypes.Protected, ConditionUnitTypes.Relaxed }))
                                {
                                    cellUnitDataComForGive.ConditionUnitType = default;
                                }

                                if (cellUnitDataComForGive.ExtraToolWeaponType.IsTool())
                                {
                                    inventToolsCom.AddAmountTools(ownerCellUnitComForGive.IsMasterClient, cellUnitDataComForGive.ExtraToolWeaponType);
                                }
                                else
                                {
                                    inventWeaponsComp.AddAmountWeapons(ownerCellUnitComForGive.IsMasterClient, cellUnitDataComForGive.ExtraToolWeaponType);
                                }

                                cellUnitDataComForGive.ResetAmountSteps();
                                cellUnitDataComForGive.ExtraToolWeaponType = default;
                            }

                            else
                            {
                                RpcGeneralSystem.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }

                        else
                        {
                            RpcGeneralSystem.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
                        }

                    }

                    else
                    {
                        if (toolWeapTypeForGive.IsForPawn())
                        {
                            if (cellUnitDataComForGive.HaveMaxAmountHealth)
                            {
                                if (cellUnitDataComForGive.HaveMaxAmountSteps)
                                {
                                    if (cellUnitDataComForGive.MainToolWeaponType != toolWeapTypeForGive)
                                    {
                                        if (toolWeapTypeForGive.IsTool())
                                        {
                                            if (inventToolsCom.HaveTool(ownerCellUnitComForGive.IsMasterClient, toolWeapTypeForGive))
                                            {
                                                inventToolsCom.TakeAmountTools(ownerCellUnitComForGive.IsMasterClient, toolWeapTypeForGive);

                                                cellUnitDataComForGive.ExtraToolWeaponType = toolWeapTypeForGive;
                                                cellUnitDataComForGive.ResetAmountSteps();
                                            }

                                            else if (toolWeapTypeForGive == ToolWeaponTypes.Pick)
                                            {
                                                if (inventResCom.GetAmountResources(ResourceTypes.Wood, ownerCellUnitComForGive.IsMasterClient) >= _woodCostForPick)
                                                {
                                                    inventResCom.TakeAmountResources(ResourceTypes.Wood, ownerCellUnitComForGive.IsMasterClient, _woodCostForPick);

                                                    cellUnitDataComForGive.ExtraToolWeaponType = toolWeapTypeForGive;
                                                    cellUnitDataComForGive.ResetAmountSteps();
                                                }
                                                else
                                                {
                                                    RpcGeneralSystem.MistakeEconomyToGeneral(sender, new[] { true, false, true, true, true });
                                                }
                                            }

                                            else if (toolWeapTypeForGive == ToolWeaponTypes.Axe)
                                            {
                                                cellUnitDataComForGive.ExtraToolWeaponType = toolWeapTypeForGive;
                                                cellUnitDataComForGive.ResetAmountSteps();
                                            }
                                        }

                                        else
                                        {
                                            if (inventWeaponsComp.HaveWeapon(ownerCellUnitComForGive.IsMasterClient, toolWeapTypeForGive))
                                            {
                                                inventWeaponsComp.TakeAmountWeapons(ownerCellUnitComForGive.IsMasterClient, toolWeapTypeForGive);

                                                cellUnitDataComForGive.ExtraToolWeaponType = toolWeapTypeForGive;
                                                cellUnitDataComForGive.ResetAmountSteps();
                                            }

                                            else if (toolWeapTypeForGive == ToolWeaponTypes.Sword)
                                            {
                                                if (inventResCom.GetAmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient) >= _ironCostForSword)
                                                {
                                                    inventResCom.TakeAmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient, _ironCostForSword);

                                                    cellUnitDataComForGive.ExtraToolWeaponType = toolWeapTypeForGive;
                                                    cellUnitDataComForGive.ResetAmountSteps();
                                                }
                                                else
                                                {
                                                    RpcGeneralSystem.MistakeEconomyToGeneral(sender, new[] { true, true, true, false, true });
                                                }
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    RpcGeneralSystem.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }

                            else
                            {
                                RpcGeneralSystem.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
                            }
                        }

                        else
                        {
                            RpcGeneralSystem.SimpleMistakeToGeneral(MistakeTypes.ThisIsForOtherUnit, sender);
                        }
                    }
                }
            }
        }
    }
}
