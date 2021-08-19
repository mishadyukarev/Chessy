﻿using Assets.Scripts.Abstractions.Enums;
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

        private EcsFilter<UnitsInConditionInGameCom> _unitsInCondInGameFilter = default;

        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var forGiveToolOrWeaponCom = ref _forGivePawnToolFilter.Get1(0);
            ref var inventToolsCom = ref _inventToolWeapFilter.Get1(0);
            ref var inventWeaponsComp = ref _inventToolWeapFilter.Get2(0);
            ref var inventResCom = ref _inventResFilter.Get1(0);
            ref var unitsInCondInGameCom = ref _unitsInCondInGameFilter.Get1(0);

            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var neededIdx = forGiveToolOrWeaponCom.IdxCell;
            var toolWeaponTypeForGive = forGiveToolOrWeaponCom.ToolWeapType;

            ref var cellUnitDataComForGive = ref _cellUnitFilter.Get1(neededIdx);
            ref var ownerCellUnitComForGive = ref _cellUnitFilter.Get2(neededIdx);



            if (cellUnitDataComForGive.IsUnitType(new[] { UnitTypes.Bishop, UnitTypes.Rook }))
            {
                if (toolWeaponTypeForGive.IsForArcher())
                {
                    if (cellUnitDataComForGive.HaveMaxAmountHealth)
                    {
                        if (cellUnitDataComForGive.HaveMaxAmountSteps)
                        {
                            if (cellUnitDataComForGive.IsConditionType(new[] { ConditionUnitTypes.Protected, ConditionUnitTypes.Relaxed }))
                            {
                                unitsInCondInGameCom.ReplaceCondition(cellUnitDataComForGive.ConditionUnitType, default,
                                    cellUnitDataComForGive.UnitType, ownerCellUnitComForGive.IsMasterClient, neededIdx);

                                cellUnitDataComForGive.ConditionUnitType = default;
                            }

                            if (cellUnitDataComForGive.MainToolWeaponType.Is(ToolWeaponTypes.Crossbow))
                            {
                                inventWeaponsComp.AddAmountWeapons(ownerCellUnitComForGive.IsMasterClient, ToolWeaponTypes.Crossbow);
                                cellUnitDataComForGive.MainToolWeaponType = ToolWeaponTypes.Bow;
                            }

                            else if (!cellUnitDataComForGive.MainToolWeaponType.Is(toolWeaponTypeForGive))
                            {
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
                                        RpcGameSystem.MistakeEconomyToGeneral(sender, new[] { true, true, true, false, true });
                                    }
                                }
                            }
                        }

                        else
                        {
                            RpcGameSystem.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        }
                    }

                    else
                    {
                        RpcGameSystem.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
                    }
                }

                else
                {
                    RpcGameSystem.SimpleMistakeToGeneral(MistakeTypes.ThisIsForOtherUnit, sender);
                }
            }     
        }
    }
}
