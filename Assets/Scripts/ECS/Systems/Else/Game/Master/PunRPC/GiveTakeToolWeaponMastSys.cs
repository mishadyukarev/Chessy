using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.Supports;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
{
    internal sealed class GiveTakeToolWeaponMastSys : IEcsRunSystem
    {
        private byte _woodCostForPick = 5;
        private byte _ironCostForSword = 1;

        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForGiveToolWeaponComp> _forGivePawnToolFilter = default;

        private EcsFilter<InventorResourcesComponent> _inventResFilter = default;
        private EcsFilter<InventorToolsComp> _inventToolsFilter = default;
        private EcsFilter<InventorWeaponsComp> _inventorWeaponsFilter = default;

        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var forGiveToolOrWeaponCom = ref _forGivePawnToolFilter.Get1(0);
            ref var inventToolsCom = ref _inventToolsFilter.Get1(0);
            ref var inventWeaponsComp = ref _inventorWeaponsFilter.Get1(0);
            ref var inventResCom = ref _inventResFilter.Get1(0);

            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var neededIdx = forGiveToolOrWeaponCom.IdxCell;
            var toolWeaponTypeForGive = forGiveToolOrWeaponCom.ToolAndWeaponType;

            ref var cellUnitDataComForGive = ref _cellUnitFilter.Get1(neededIdx);
            ref var ownerCellUnitComForGive = ref _cellUnitFilter.Get2(neededIdx);


            if (cellUnitDataComForGive.HaveExtraToolWeapon)
            {
                if (cellUnitDataComForGive.HaveMaxAmountHealth)
                {
                    if (cellUnitDataComForGive.HaveMaxAmountSteps)
                    {
                        if (cellUnitDataComForGive.IsUnitType(UnitTypes.Pawn))
                        {
                            if (cellUnitDataComForGive.ExtraToolWeaponType != ToolWeaponTypes.Axe)
                            {
                                if (cellUnitDataComForGive.ExtraToolWeaponType.IsTool())
                                {
                                    inventToolsCom.AddAmountTools(ownerCellUnitComForGive.IsMasterClient, cellUnitDataComForGive.ExtraToolWeaponType);
                                }
                                else
                                {
                                    inventWeaponsComp.AddAmountWeapons(ownerCellUnitComForGive.IsMasterClient, cellUnitDataComForGive.ExtraToolWeaponType);
                                }
                            }

                            cellUnitDataComForGive.ResetAmountSteps();
                            cellUnitDataComForGive.ExtraToolWeaponType = default;
                        }
                    }

                    else
                    {
                        RPCGameSystem.MistakeNeedMoreStepsToGeneral(sender);
                    }
                }
                else
                {
                    RPCGameSystem.MistakeNeedMoreHealthToGeneral(sender);
                }
            }

            else
            {
                if (cellUnitDataComForGive.HaveMaxAmountHealth)
                {
                    if (cellUnitDataComForGive.HaveMaxAmountSteps)
                    {
                        if (cellUnitDataComForGive.IsUnitType(UnitTypes.Pawn))
                        {
                            if (cellUnitDataComForGive.MainToolWeaponType != toolWeaponTypeForGive)
                            {
                                if (toolWeaponTypeForGive.IsTool())
                                {
                                    if (inventToolsCom.HaveTool(ownerCellUnitComForGive.IsMasterClient, toolWeaponTypeForGive))
                                    {
                                        inventToolsCom.TakeAmountTools(ownerCellUnitComForGive.IsMasterClient, toolWeaponTypeForGive);

                                        cellUnitDataComForGive.ExtraToolWeaponType = toolWeaponTypeForGive;
                                        cellUnitDataComForGive.ResetAmountSteps();
                                    }

                                    else if (toolWeaponTypeForGive == ToolWeaponTypes.Pick)
                                    {
                                        if (inventResCom.GetAmountResources(ResourceTypes.Wood, ownerCellUnitComForGive.IsMasterClient) >= _woodCostForPick)
                                        {
                                            inventResCom.TakeAmountResources(ResourceTypes.Wood, ownerCellUnitComForGive.IsMasterClient, _woodCostForPick);

                                            cellUnitDataComForGive.ExtraToolWeaponType = toolWeaponTypeForGive;
                                            cellUnitDataComForGive.ResetAmountSteps();
                                        }
                                        else
                                        {
                                            RPCGameSystem.MistakeEconomyToGeneral(sender, new[] { true, false, true, true, true });
                                        }
                                    }

                                    else if(toolWeaponTypeForGive == ToolWeaponTypes.Axe)
                                    {
                                        cellUnitDataComForGive.ExtraToolWeaponType = toolWeaponTypeForGive;
                                        cellUnitDataComForGive.ResetAmountSteps();
                                    }
                                }

                                else
                                {
                                    if(inventWeaponsComp.HaveWeapon(ownerCellUnitComForGive.IsMasterClient, toolWeaponTypeForGive))
                                    {
                                        inventWeaponsComp.TakeAmountWeapons(ownerCellUnitComForGive.IsMasterClient, toolWeaponTypeForGive);

                                        cellUnitDataComForGive.ExtraToolWeaponType = toolWeaponTypeForGive;
                                        cellUnitDataComForGive.ResetAmountSteps();
                                    }

                                    else if (toolWeaponTypeForGive == ToolWeaponTypes.Sword)
                                    {
                                        if (inventResCom.GetAmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient) >= _ironCostForSword)
                                        {
                                            inventResCom.TakeAmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient, _ironCostForSword);

                                            cellUnitDataComForGive.ExtraToolWeaponType = toolWeaponTypeForGive;
                                            cellUnitDataComForGive.ResetAmountSteps();
                                        }
                                        else
                                        {
                                            RPCGameSystem.MistakeEconomyToGeneral(sender, new[] { true, true, true, false, true });
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        RPCGameSystem.MistakeNeedMoreStepsToGeneral(sender);
                    }
                }

                else
                {
                    RPCGameSystem.MistakeNeedMoreHealthToGeneral(sender);
                }
            }
        }
    }
}
