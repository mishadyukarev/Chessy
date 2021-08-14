using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
{
    internal sealed class GiveExtraPawnToolMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForGivePawnToolComponent> _forGivePawnToolFilter = default;

        private EcsFilter<CellUnitDataComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var forGivePawnToolCom = ref _forGivePawnToolFilter.Get1(0);

            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var neededIdx = forGivePawnToolCom.IdxForGivePawnTool;

            ref var neededCellUnitDataCom = ref _cellUnitFilter.Get1(neededIdx);


            if (neededCellUnitDataCom.IsUnitType(UnitTypes.Pawn_Axe))
            {
                if (!neededCellUnitDataCom.HaveExtraPawnTool)
                {
                    if (neededCellUnitDataCom.HaveMaxAmountHealth)
                    {
                        if (neededCellUnitDataCom.HaveMaxAmountSteps)
                        {
                            neededCellUnitDataCom.ExtraPawnToolType = forGivePawnToolCom.PawnToolType;
                            neededCellUnitDataCom.ResetAmountSteps();
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
