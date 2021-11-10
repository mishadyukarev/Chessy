using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class FireArcherMS : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataC, UnitEffectsC, OwnerC> _cellUnitOthFilt = default;
        private EcsFilter<CellFireDataC> _cellFireFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;

        private EcsFilter<CellsArsonArcherComp> _cellsArcherArsonFilt = default;


        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToMC.Get(out var idx_from, out var idx_to);


            ref var unit_from = ref _cellUnitFilter.Get1(idx_from);
            ref var stepUnit_from = ref _cellUnitFilter.Get2(idx_from);
            ref var effUnit_from = ref _cellUnitOthFilt.Get2(idx_from);
            ref var ownUnit_from = ref _cellUnitOthFilt.Get3(idx_from);

            ref var toFireDatCom = ref _cellFireFilter.Get1(idx_to);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (stepUnit_from.HaveMaxSteps(unit_from.Unit, effUnit_from.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_from.Owner, unit_from.Unit)))
            {
                if (_cellsArcherArsonFilt.Get1(0).HaveIdxCell(whoseMove, idx_from, idx_to))
                {
                    RpcSys.SoundToGeneral(RpcTarget.All, UniqAbilTypes.FireArcher);

                    stepUnit_from.DefSteps();
                    toFireDatCom.HaveFire = true;
                }
            }

            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
