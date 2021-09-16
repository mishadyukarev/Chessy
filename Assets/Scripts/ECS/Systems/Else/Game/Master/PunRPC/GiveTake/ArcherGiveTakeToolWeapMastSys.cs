using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
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

        private EcsFilter<CellUnitDataComponent, OwnerOnlineComp> _cellUnitFilter = default;

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
                if (cellUnitDataComForGive.ArcherWeaponType.Is(ToolWeaponTypes.Crossbow))
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
                            cellUnitDataComForGive.ArcherWeaponType = ToolWeaponTypes.Bow;

                            cellUnitDataComForGive.ResetAmountSteps();

                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickArcher);
                        }
                        else
                        {
                            RpcSys.SoundToGeneral(sender, SoundEffectTypes. Mistake);
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

                                    cellUnitDataComForGive.ArcherWeaponType = toolWeaponTypeForGive;
                                    cellUnitDataComForGive.ResetAmountSteps();
                                }

                                else if (toolWeaponTypeForGive == ToolWeaponTypes.Crossbow)
                                {
                                    if (inventResCom.AmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient) >= _ironCostForCrossbow)
                                    {
                                        inventResCom.TakeAmountResources(ResourceTypes.Iron, ownerCellUnitComForGive.IsMasterClient, _ironCostForCrossbow);

                                        cellUnitDataComForGive.ArcherWeaponType = toolWeaponTypeForGive;
                                        cellUnitDataComForGive.ResetAmountSteps();

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

