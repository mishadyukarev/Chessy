using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
{
    internal sealed class TakePawnExtraToolMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForTakePawnExtraToolMastCom> _forTakePawnExtraToolFilter = default;

        private EcsFilter<InventorToolsComponent> _inventToolsFilter = default;

        private EcsFilter<CellUnitDataComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var infoMasCom = ref _infoFilter.Get1(0);
            ref var forTakePawnExtraToolCom = ref _forTakePawnExtraToolFilter.Get1(0);

            var sender = infoMasCom.FromInfo.Sender;

            ref var cellUnitDataComForTake = ref _cellUnitFilter.Get1(forTakePawnExtraToolCom.IdxCellForTakePawnExtraTool);


            if (cellUnitDataComForTake.IsUnitType(UnitTypes.Pawn_Axe))
            {
                if (cellUnitDataComForTake.HaveExtraPawnTool)
                {
                    if (cellUnitDataComForTake.HaveMaxAmountHealth)
                    {
                        if (cellUnitDataComForTake.HaveMaxAmountSteps)
                        {
                            _inventToolsFilter.Get1(0).AddAmountTools(cellUnitDataComForTake.ExtraPawnToolType);
                            cellUnitDataComForTake.ResetPawnExtraTool();
                            cellUnitDataComForTake.ResetAmountSteps();
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
                    RPCGameSystem.MistakeNeedToolInPawnToGeneral(sender);
                }
            }
        }
    }
}
