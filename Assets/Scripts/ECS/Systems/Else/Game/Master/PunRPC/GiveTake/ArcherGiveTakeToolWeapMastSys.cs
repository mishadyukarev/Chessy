using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Supports;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
{
    internal sealed class ArcherGiveTakeToolWeapMastSys : IEcsRunSystem
    {
        private byte _ironCostForCrossbow = 1;

        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGivePawnToolFilter = default;

        private EcsFilter<InventorResourcesComponent> _inventResFilter = default;
        private EcsFilter<InventorToolsComp, InventorWeaponsComp> _inventToolWeapFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom> _cellUnitFilter = default;

        public void Run()
        {
            ref var forGiveToolOrWeaponCom = ref _forGivePawnToolFilter.Get1(0);
            ref var inventToolsCom = ref _inventToolWeapFilter.Get1(0);
            ref var invWeapsComp = ref _inventToolWeapFilter.Get2(0);
            ref var inventResCom = ref _inventResFilter.Get1(0);

            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var neededIdx = forGiveToolOrWeaponCom.IdxCell;
            var toolWeaponTypeForGive = forGiveToolOrWeaponCom.ToolWeapType;

            ref var unitDatComForGive = ref _cellUnitFilter.Get1(neededIdx);
            ref var onUnitComForGive = ref _cellUnitFilter.Get2(neededIdx);
            ref var offUnitComForGive = ref _cellUnitFilter.Get3(neededIdx);



            if (unitDatComForGive.Is(new[] { UnitTypes.Bishop, UnitTypes.Rook }))
            {
                if (unitDatComForGive.ArcherWeapType.Is(ToolWeaponTypes.Crossbow))
                {
                    if (unitDatComForGive.HaveMaxAmountHealth)
                    {
                        if (unitDatComForGive.HaveMaxAmountSteps)
                        {
                            if (unitDatComForGive.IsConditionType(new[] { CondUnitTypes.Protected, CondUnitTypes.Relaxed }))
                            {
                                unitDatComForGive.CondUnitType = default;
                            }

                            if (onUnitComForGive.HaveOwner)
                            {
                                invWeapsComp.AddAmountWeapons(onUnitComForGive.IsMasterClient, ToolWeaponTypes.Crossbow);
                            }
                            else
                            {
                                invWeapsComp.AddAmountWeapons(offUnitComForGive.IsMainMaster, ToolWeaponTypes.Crossbow);
                            }


                            
                            unitDatComForGive.ArcherWeapType = ToolWeaponTypes.Bow;

                            unitDatComForGive.ResetAmountSteps();

                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickArcher);
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
                    if (toolWeaponTypeForGive.IsForArcher())
                    {
                        if (unitDatComForGive.HaveMaxAmountHealth)
                        {
                            if (unitDatComForGive.HaveMaxAmountSteps)
                            {
                                if (unitDatComForGive.IsConditionType(new[] { CondUnitTypes.Protected, CondUnitTypes.Relaxed }))
                                {
                                    unitDatComForGive.CondUnitType = default;
                                }

                                var isMastMain = false;
                                if (onUnitComForGive.HaveOwner)
                                {
                                    isMastMain = onUnitComForGive.IsMasterClient;
                                }
                                else
                                {
                                    isMastMain = offUnitComForGive.IsMainMaster;
                                }



                                if (invWeapsComp.HaveWeapon(isMastMain, toolWeaponTypeForGive))
                                {
                                    invWeapsComp.TakeAmountWeapons(isMastMain, toolWeaponTypeForGive);

                                    unitDatComForGive.ArcherWeapType = toolWeaponTypeForGive;
                                    unitDatComForGive.ResetAmountSteps();
                                }

                                else if (toolWeaponTypeForGive == ToolWeaponTypes.Crossbow)
                                {
                                    if (inventResCom.AmountResources(ResourceTypes.Iron, isMastMain) >= _ironCostForCrossbow)
                                    {
                                        inventResCom.TakeAmountResources(ResourceTypes.Iron, isMastMain, _ironCostForCrossbow);

                                        unitDatComForGive.ArcherWeapType = toolWeaponTypeForGive;
                                        unitDatComForGive.ResetAmountSteps();

                                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickArcher);
                                    }
                                    else
                                    {
                                        RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                        RpcSys.MistakeEconomyToGeneral(sender, new[] { true, true, true, false, true });
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

