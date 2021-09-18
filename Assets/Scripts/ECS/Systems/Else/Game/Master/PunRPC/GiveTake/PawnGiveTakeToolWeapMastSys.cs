using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
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

        private EcsFilter<CellUnitDataCom, OwnerOnlineComp> _cellUnitFilter = default;

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

                if (cellUnitDataComForGive.IsUnit(UnitTypes.Pawn))
                {
                    if (cellUnitDataComForGive.HaveExtraToolWeaponPawn)
                    {
                        if (cellUnitDataComForGive.HaveMaxAmountHealth)
                        {
                            if (cellUnitDataComForGive.HaveMaxAmountSteps)
                            {
                                if (cellUnitDataComForGive.IsConditionType(new[] { CondUnitTypes.Protected, CondUnitTypes.Relaxed }))
                                {
                                    cellUnitDataComForGive.CondUnitType = default;
                                }

                                if (cellUnitDataComForGive.ExtraTWPawnType.IsTool())
                                {
                                    inventToolsCom.AddAmountTools(ownerCellUnitComForGive.IsMasterClient, cellUnitDataComForGive.ExtraTWPawnType);
                                }
                                else
                                {
                                    inventWeaponsComp.AddAmountWeapons(ownerCellUnitComForGive.IsMasterClient, cellUnitDataComForGive.ExtraTWPawnType);
                                }

                                cellUnitDataComForGive.ResetAmountSteps();
                                cellUnitDataComForGive.ExtraTWPawnType = default;

                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                            }

                            else
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }

                        else
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
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
                                    if (cellUnitDataComForGive.ArcherWeapType != toolWeapTypeForGive)
                                    {
                                        if (toolWeapTypeForGive.IsTool())
                                        {
                                            if (inventToolsCom.HaveTool(ownerCellUnitComForGive.IsMasterClient, toolWeapTypeForGive))
                                            {
                                                inventToolsCom.TakeAmountTools(ownerCellUnitComForGive.IsMasterClient, toolWeapTypeForGive);

                                                cellUnitDataComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                cellUnitDataComForGive.ResetAmountSteps();

                                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                            }

                                            else if (toolWeapTypeForGive == ToolWeaponTypes.Pick)
                                            {
                                                if (inventResCom.AmountResources(ResourceTypes.Wood, ownerCellUnitComForGive.IsMasterClient) >= _woodCostForPick)
                                                {
                                                    inventResCom.TakeAmountResources(ResourceTypes.Wood, ownerCellUnitComForGive.IsMasterClient, _woodCostForPick);

                                                    cellUnitDataComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                    cellUnitDataComForGive.ResetAmountSteps();

                                                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                                }
                                                else
                                                {
                                                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                                    RpcSys.MistakeEconomyToGeneral(sender, new[] { true, false, true, true, true });
                                                }
                                            }

                                            else if (toolWeapTypeForGive == ToolWeaponTypes.Axe)
                                            {
                                                cellUnitDataComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                cellUnitDataComForGive.ResetAmountSteps();
                                            }
                                        }

                                        else
                                        {
                                            if (inventWeaponsComp.HaveWeapon(ownerCellUnitComForGive.IsMasterClient, toolWeapTypeForGive))
                                            {
                                                inventWeaponsComp.TakeAmountWeapons(ownerCellUnitComForGive.IsMasterClient, toolWeapTypeForGive);

                                                cellUnitDataComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                cellUnitDataComForGive.ResetAmountSteps();

                                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                            }

                                            else if (toolWeapTypeForGive == ToolWeaponTypes.Sword)
                                            {
                                                if (inventResCom.AmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient) >= _ironCostForSword)
                                                {
                                                    inventResCom.TakeAmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient, _ironCostForSword);

                                                    cellUnitDataComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                    cellUnitDataComForGive.ResetAmountSteps();

                                                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                                }
                                                else
                                                {
                                                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                                    RpcSys.MistakeEconomyToGeneral(sender, new[] { true, true, true, false, true });
                                                }
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }

                            else
                            {
                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
                            }
                        }

                        else
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                            RpcSys.SimpleMistakeToGeneral(MistakeTypes.ThatIsForOtherUnit, sender);
                        }
                    }
                }
            }
        }
    }
}
