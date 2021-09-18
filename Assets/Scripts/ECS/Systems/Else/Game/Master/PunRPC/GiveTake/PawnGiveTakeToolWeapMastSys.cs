using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
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

        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom> _cellUnitFilter = default;

        public void Run()
        {
            var neededIdxCell = _forGiveTakeToolWeapFilter.Get1(0).IdxCell;

            if (neededIdxCell != default)
            {
                var toolWeapTypeForGive = _forGiveTakeToolWeapFilter.Get1(0).ToolWeapType;

                ref var inventToolsCom = ref _inventToolWeapFilter.Get1(0);
                ref var inventWeapsCom = ref _inventToolWeapFilter.Get2(0);
                ref var invResCom = ref _inventResFilter.Get1(0);

                var sender = _infoFilter.Get1(0).FromInfo.Sender;

                ref var unitDatComForGive = ref _cellUnitFilter.Get1(neededIdxCell);
                ref var onUnitComForGive = ref _cellUnitFilter.Get2(neededIdxCell);




                var isMaster = false;
                if (onUnitComForGive.HaveOwner)
                {
                    isMaster = onUnitComForGive.IsMasterClient;
                }
                else
                {
                    isMaster = _cellUnitFilter.Get3(neededIdxCell).IsMainMaster;
                }





                if (unitDatComForGive.IsUnit(UnitTypes.Pawn))
                {
                    if (unitDatComForGive.HaveExtraToolWeaponPawn)
                    {
                        if (unitDatComForGive.HaveMaxAmountHealth)
                        {
                            if (unitDatComForGive.HaveMaxAmountSteps)
                            {
                                if (unitDatComForGive.IsConditionType(new[] { CondUnitTypes.Protected, CondUnitTypes.Relaxed }))
                                {
                                    unitDatComForGive.CondUnitType = default;
                                }

                                if (unitDatComForGive.ExtraTWPawnType.IsTool())
                                {
                                    inventToolsCom.AddAmountTools(isMaster , unitDatComForGive.ExtraTWPawnType);
                                }
                                else
                                {
                                    inventWeapsCom.AddAmountWeapons(isMaster, unitDatComForGive.ExtraTWPawnType);
                                }

                                unitDatComForGive.ResetAmountSteps();
                                unitDatComForGive.ExtraTWPawnType = default;

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
                            if (unitDatComForGive.HaveMaxAmountHealth)
                            {
                                if (unitDatComForGive.HaveMaxAmountSteps)
                                {
                                    if (unitDatComForGive.ArcherWeapType != toolWeapTypeForGive)
                                    {
                                        if (toolWeapTypeForGive.IsTool())
                                        {
                                            if (inventToolsCom.HaveTool(isMaster, toolWeapTypeForGive))
                                            {
                                                inventToolsCom.TakeAmountTools(isMaster, toolWeapTypeForGive);

                                                unitDatComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                unitDatComForGive.ResetAmountSteps();

                                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                            }

                                            else if (toolWeapTypeForGive == ToolWeaponTypes.Pick)
                                            {
                                                if (invResCom.AmountResources(ResourceTypes.Wood, isMaster) >= _woodCostForPick)
                                                {
                                                    invResCom.TakeAmountResources(ResourceTypes.Wood, isMaster, _woodCostForPick);

                                                    unitDatComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                    unitDatComForGive.ResetAmountSteps();

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
                                                unitDatComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                unitDatComForGive.ResetAmountSteps();
                                            }
                                        }

                                        else
                                        {
                                            if (inventWeapsCom.HaveWeapon(isMaster, toolWeapTypeForGive))
                                            {
                                                inventWeapsCom.TakeAmountWeapons(isMaster, toolWeapTypeForGive);

                                                unitDatComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                unitDatComForGive.ResetAmountSteps();

                                                RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                            }

                                            else if (toolWeapTypeForGive == ToolWeaponTypes.Sword)
                                            {
                                                if (invResCom.AmountResources(ResourceTypes.Iron, isMaster) >= _ironCostForSword)
                                                {
                                                    invResCom.TakeAmountResources(ResourceTypes.Iron, isMaster, _ironCostForSword);

                                                    unitDatComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                    unitDatComForGive.ResetAmountSteps();

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
