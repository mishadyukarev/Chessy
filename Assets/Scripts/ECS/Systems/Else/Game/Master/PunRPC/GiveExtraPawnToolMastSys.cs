using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
{
    internal sealed class GiveExtraPawnToolMastSys : IEcsRunSystem
    {
        private byte _woodCostForPick = 15;
        private byte _ironCostForSword = 1;

        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForGiveExtraPawnToolComp> _forGivePawnToolFilter = default;

        private EcsFilter<InventorResourcesComponent> _inventResFilter = default;
        private EcsFilter<InventorToolsComponent> _inventToolsFilter = default;

        private EcsFilter<CellUnitDataComponent, CellPawnDataComp, OwnerComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var forGivePawnToolCom = ref _forGivePawnToolFilter.Get1(0);
            ref var inventToolsCom = ref _inventToolsFilter.Get1(0);
            ref var inventResCom = ref _inventResFilter.Get1(0);

            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var neededIdx = forGivePawnToolCom.IdxForGivePawnTool;
            var toolTypeForGive = forGivePawnToolCom.PawnExtraToolType;

            ref var neededCellUnitDataCom = ref _cellUnitFilter.Get1(neededIdx);
            ref var cellPawnDataCompForGive = ref _cellUnitFilter.Get2(neededIdx);
            ref var neededOwnerCellUnitCom = ref _cellUnitFilter.Get3(neededIdx);


            if (neededCellUnitDataCom.IsUnitType(UnitTypes.Pawn))
            {
                if (!cellPawnDataCompForGive.HaveExtraTool)
                {
                    if (neededCellUnitDataCom.HaveMaxAmountHealth)
                    {
                        if (neededCellUnitDataCom.HaveMaxAmountSteps)
                        {
                            if (inventToolsCom.HaveTool(toolTypeForGive))
                            {
                                inventToolsCom.TakeAmountTools(toolTypeForGive);

                                cellPawnDataCompForGive.ExtraToolType = toolTypeForGive;
                                neededCellUnitDataCom.ResetAmountSteps();
                            }
                            else if (toolTypeForGive == PawnExtraToolTypes.Pick)
                            {
                                if (inventResCom.GetAmountResources(ResourceTypes.Wood, neededOwnerCellUnitCom.IsMasterClient) >= _woodCostForPick)
                                {
                                    inventResCom.TakeAmountResources(ResourceTypes.Wood, neededOwnerCellUnitCom.IsMasterClient, _woodCostForPick);

                                    cellPawnDataCompForGive.ExtraToolType = toolTypeForGive;
                                    neededCellUnitDataCom.ResetAmountSteps();
                                }
                                else
                                {
                                    RPCGameSystem.MistakeEconomyToGeneral(sender, new[] { true, false, true, true, true });
                                }
                            }
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
        }
    }
}
