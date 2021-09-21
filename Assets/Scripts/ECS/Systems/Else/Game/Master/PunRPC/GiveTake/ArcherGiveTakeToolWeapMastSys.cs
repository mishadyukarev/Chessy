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

        private EcsFilter<InventResourCom> _inventResFilter = default;
        private EcsFilter<InventorTWCom> _inventTWFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            ref var forGiveToolOrWeaponCom = ref _forGivePawnToolFilter.Get1(0);
            ref var inventTWCom = ref _inventTWFilt.Get1(0);
            ref var inventResCom = ref _inventResFilter.Get1(0);

            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var neededIdx = forGiveToolOrWeaponCom.IdxCell;
            var toolWeaponTypeForGive = forGiveToolOrWeaponCom.ToolWeapType;

            ref var unitDatComForGive = ref _cellUnitFilter.Get1(neededIdx);
            ref var onUnitComForGive = ref _cellUnitFilter.Get2(neededIdx);



            if (unitDatComForGive.Is(new[] { UnitTypes.Bishop, UnitTypes.Rook }))
            {
                if (unitDatComForGive.ArcherWeapType.Is(ToolWeaponTypes.Crossbow))
                {
                    if (unitDatComForGive.HaveMaxAmountHealth)
                    {
                        if (unitDatComForGive.HaveMinAmountSteps)
                        {
                            unitDatComForGive.CondUnitType = default;


                            inventTWCom.AddAmountTools(onUnitComForGive.PlayerType, ToolWeaponTypes.Crossbow);
                            

                            unitDatComForGive.ArcherWeapType = ToolWeaponTypes.Bow;

                            unitDatComForGive.AmountSteps -= 1;

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
                            if (unitDatComForGive.HaveMinAmountSteps)
                            {
                                unitDatComForGive.CondUnitType = default;

                                if (inventTWCom.HaveTool(onUnitComForGive.PlayerType, toolWeaponTypeForGive))
                                {
                                    inventTWCom.TakeAmountTools(onUnitComForGive.PlayerType, toolWeaponTypeForGive);

                                    unitDatComForGive.ArcherWeapType = toolWeaponTypeForGive;
                                    unitDatComForGive.AmountSteps -= 1;
                                }

                                else if (toolWeaponTypeForGive == ToolWeaponTypes.Crossbow)
                                {
                                    if (inventResCom.AmountResources(onUnitComForGive.PlayerType, ResourceTypes.Iron) >= _ironCostForCrossbow)
                                    {
                                        inventResCom.TakeAmountResources(onUnitComForGive.PlayerType, ResourceTypes.Iron, _ironCostForCrossbow);

                                        unitDatComForGive.ArcherWeapType = toolWeaponTypeForGive;
                                        unitDatComForGive.AmountSteps -= 1;

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

