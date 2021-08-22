using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.Supports;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
{
    internal sealed class ArcherGiveTakeToolWeapMastSys : IEcsRunSystem
    {
        private byte _ironCostForCrossbow = 1;

        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGivePawnToolFilter = default;

        private EcsFilter<InventorResourcesComponent> _inventResFilter = default;
        private EcsFilter<InventorToolsComp, InventorWeaponsComp> _inventToolWeapFilter = default;

        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var forGiveToolOrWeaponCom = ref _forGivePawnToolFilter.Get1(0);
            ref var inventToolsCom = ref _inventToolWeapFilter.Get1(0);
            ref var inventWeaponsComp = ref _inventToolWeapFilter.Get2(0);
            ref var inventResCom = ref _inventResFilter.Get1(0);

            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var neededIdx = forGiveToolOrWeaponCom.IdxCell;
            var toolWeaponTypeForGive = forGiveToolOrWeaponCom.ToolWeapType;

            ref var cellUnitDataComForGive = ref _cellUnitFilter.Get1(neededIdx);
            ref var ownerCellUnitComForGive = ref _cellUnitFilter.Get2(neededIdx);



            if (cellUnitDataComForGive.IsUnitType(new[] { UnitTypes.Bishop, UnitTypes.Rook }))
            {
                if (cellUnitDataComForGive.MainToolWeaponType.Is(ToolWeaponTypes.Crossbow))
                {
                    if (cellUnitDataComForGive.HaveMaxAmountHealth)
                    {
                        if (cellUnitDataComForGive.HaveMaxAmountSteps)
                        {
                            if (cellUnitDataComForGive.IsConditionType(new[] { ConditionUnitTypes.Protected, ConditionUnitTypes.Relaxed }))
                            {
                                cellUnitDataComForGive.ConditionUnitType = default;
                            }

                            inventWeaponsComp.AddAmountWeapons(ownerCellUnitComForGive.IsMasterClient, ToolWeaponTypes.Crossbow);
                            cellUnitDataComForGive.MainToolWeaponType = ToolWeaponTypes.Bow;

                            cellUnitDataComForGive.ResetAmountSteps();
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
                    if (toolWeaponTypeForGive.IsForArcher())
                    {
                        if (cellUnitDataComForGive.HaveMaxAmountHealth)
                        {
                            if (cellUnitDataComForGive.HaveMaxAmountSteps)
                            {
                                if (cellUnitDataComForGive.IsConditionType(new[] { ConditionUnitTypes.Protected, ConditionUnitTypes.Relaxed }))
                                {
                                    cellUnitDataComForGive.ConditionUnitType = default;
                                }

                                if (inventWeaponsComp.HaveWeapon(ownerCellUnitComForGive.IsMasterClient, toolWeaponTypeForGive))
                                {
                                    inventWeaponsComp.TakeAmountWeapons(ownerCellUnitComForGive.IsMasterClient, toolWeaponTypeForGive);

                                    cellUnitDataComForGive.MainToolWeaponType = toolWeaponTypeForGive;
                                    cellUnitDataComForGive.ResetAmountSteps();
                                }

                                else if (toolWeaponTypeForGive == ToolWeaponTypes.Crossbow)
                                {
                                    if (inventResCom.GetAmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient) >= _ironCostForCrossbow)
                                    {
                                        inventResCom.TakeAmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient, _ironCostForCrossbow);

                                        cellUnitDataComForGive.MainToolWeaponType = toolWeaponTypeForGive;
                                        cellUnitDataComForGive.ResetAmountSteps();
                                    }
                                    else
                                    {
                                        RpcGeneralSystem.MistakeEconomyToGeneral(sender, new[] { true, true, true, false, true });
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

