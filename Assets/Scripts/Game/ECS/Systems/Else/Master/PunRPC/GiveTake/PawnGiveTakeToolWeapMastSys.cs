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

        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ForGiveTakeToolWeaponComp> _forGiveTakeToolWeapFilter = default;

        private EcsFilter<InventResourCom> _inventResFilter = default;
        private EcsFilter<InventorTWCom> _inventTWFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            var neededIdxCell = _forGiveTakeToolWeapFilter.Get1(0).IdxCell;

            if (neededIdxCell != default)
            {
                var toolWeapTypeForGive = _forGiveTakeToolWeapFilter.Get1(0).ToolWeapType;

                ref var inventTWCom = ref _inventTWFilt.Get1(0);
                ref var invResCom = ref _inventResFilter.Get1(0);

                var sender = _infoFilter.Get1(0).FromInfo.Sender;

                ref var unitDatComForGive = ref _cellUnitFilter.Get1(neededIdxCell);
                ref var ownUnitComForGive = ref _cellUnitFilter.Get2(neededIdxCell);


                if (unitDatComForGive.Is(UnitTypes.Pawn))
                {
                    if (unitDatComForGive.HaveExtraToolWeaponPawn)
                    {
                        if (unitDatComForGive.HaveMaxAmountHealth)
                        {
                            if (unitDatComForGive.HaveMinAmountSteps)
                            {
                                unitDatComForGive.CondUnitType = default;

                                inventTWCom.AddAmountTools(ownUnitComForGive.PlayerType, unitDatComForGive.ExtraTWPawnType);

                                unitDatComForGive.AmountSteps -= 1;
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
                                if (unitDatComForGive.HaveMinAmountSteps)
                                {
                                    if (unitDatComForGive.ArcherWeapType != toolWeapTypeForGive)
                                    {
                                        unitDatComForGive.CondUnitType = default;


                                        if (inventTWCom.HaveTool(ownUnitComForGive.PlayerType, toolWeapTypeForGive))
                                        {
                                            inventTWCom.TakeAmountTools(ownUnitComForGive.PlayerType, toolWeapTypeForGive);

                                            unitDatComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                            unitDatComForGive.AmountSteps -= 1;

                                            RpcSys.SoundToGeneral(sender, SoundEffectTypes.PickMelee);
                                        }

                                        else if (toolWeapTypeForGive == ToolWeaponTypes.Pick)
                                        {
                                            if (invResCom.AmountResources(ownUnitComForGive.PlayerType, ResourceTypes.Wood) >= _woodCostForPick)
                                            {
                                                invResCom.TakeAmountResources(ownUnitComForGive.PlayerType, ResourceTypes.Wood, _woodCostForPick);

                                                unitDatComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                unitDatComForGive.AmountSteps -= 1;

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
                                            unitDatComForGive.AmountSteps -= 1;
                                        }

                                        else if (toolWeapTypeForGive == ToolWeaponTypes.Sword)
                                        {
                                            if (invResCom.AmountResources(ownUnitComForGive.PlayerType, ResourceTypes.Iron) >= _ironCostForSword)
                                            {
                                                invResCom.TakeAmountResources(ownUnitComForGive.PlayerType, ResourceTypes.Iron, _ironCostForSword);

                                                unitDatComForGive.ExtraTWPawnType = toolWeapTypeForGive;
                                                unitDatComForGive.AmountSteps -= 1;

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
