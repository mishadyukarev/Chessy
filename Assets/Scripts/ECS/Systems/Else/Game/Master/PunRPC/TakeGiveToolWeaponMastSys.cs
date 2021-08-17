using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
{
    internal sealed class TakeGiveToolWeaponMastSys : IEcsRunSystem
    {
        private byte _woodCostForPick = 15;
        private byte _ironCostForSword = 1;

        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForGiveExtraPawnToolComp> _forGivePawnToolFilter = default;

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


            if (cellUnitDataComForGive.IsUnitType(UnitTypes.Pawn))
            {
                if (!cellUnitDataComForGive.HaveExtraThing)
                {
                    if (cellUnitDataComForGive.HaveMaxAmountHealth)
                    {
                        if (cellUnitDataComForGive.HaveMaxAmountSteps)
                        {
                            if (inventToolsCom.HaveTool(ownerCellUnitComForGive.IsMasterClient, toolWeaponTypeForGive))
                            {
                                inventToolsCom.TakeAmountTools(ownerCellUnitComForGive.IsMasterClient, toolWeaponTypeForGive);

                                cellUnitDataComForGive.ExtraToolAndWeaponType = toolWeaponTypeForGive;
                                cellUnitDataComForGive.ResetAmountSteps();
                            }
                            //else if (toolAndWeaponTypeForGive == ToolAndWeaponTypes.Pick)
                            //{
                            //    if (inventResCom.GetAmountResources(ResourceTypes.Wood, ownerCellUnitComForGive.IsMasterClient) >= _woodCostForPick)
                            //    {
                            //        inventResCom.TakeAmountResources(ResourceTypes.Wood, ownerCellUnitComForGive.IsMasterClient, _woodCostForPick);

                            //        cellPawnDataCompForGive.ExtraThingType = extraPawnThingTypeForGive;
                            //        cellUnitDataComForGive.ResetAmountSteps();
                            //    }
                            //    else
                            //    {
                            //        RPCGameSystem.MistakeEconomyToGeneral(sender, new[] { true, false, true, true, true });
                            //    }
                            //}
                            //else if (toolTypeForGive == PawnExtra)
                            //{
                            //    if (inventResCom.GetAmountResources(ResourceTypes.Iron, neededOwnerCellUnitCom.IsMasterClient) >= _ironCostForSword)
                            //    {
                            //        inventResCom.TakeAmountResources(ResourceTypes.Iron, neededOwnerCellUnitCom.IsMasterClient, _ironCostForSword);

                            //        cellPawnDataCompForGive.ExtraWeaponType = toolTypeForGive;
                            //        neededCellUnitDataCom.ResetAmountSteps();
                            //    }
                            //    else
                            //    {
                            //        RPCGameSystem.MistakeEconomyToGeneral(sender, new[] { true, true, true, false, true });
                            //    }
                            //}
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
                    RPCGameSystem.MistakePawnHaveToolToGeneral(sender);
                }
            }


            //if (cellUnitDataComForTake.IsUnitType(UnitTypes.Pawn))
            //{
            //    if (cellPawnDataCompForTake.HaveExtraThing)
            //    {
            //        if (cellUnitDataComForTake.HaveMaxAmountHealth)
            //        {
            //            if (cellUnitDataComForTake.HaveMaxAmountSteps)
            //            {
            //                _inventToolsFilter.Get1(0).AddAmountTools(ownerCellUnitCompForTake.IsMasterClient, cellPawnDataCompForTake.ExtraThingType);
            //                cellPawnDataCompForTake.ResetExtraTool();
            //                cellUnitDataComForTake.ResetAmountSteps();
            //            }

            //            else
            //            {
            //                RPCGameSystem.MistakeNeedMoreStepsToGeneral(sender);
            //            }
            //        }

            //        else
            //        {
            //            RPCGameSystem.MistakeNeedMoreHealthToGeneral(sender);
            //        }
            //    }

            //    else
            //    {
            //        RPCGameSystem.MistakeNeedToolInPawnToGeneral(sender);
            //    }
            //}
        }
    }
}
