using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class FireArcherMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, StepC> _cellUnitFilter = default;
        private EcsFilter<UnitC, UnitEffectsC, OwnerC> _cellUnitOthFilt = default;
        private EcsFilter<FireC> _cellFireFilter = default;

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
                if (CellsArsonArcherComp.HaveIdxCell(whoseMove, idx_from, idx_to))
                {
                    RpcSys.SoundToGeneral(RpcTarget.All, UniqAbilTypes.FireArcher);

                    stepUnit_from.DefSteps();
                    toFireDatCom.Enable();
                }
            }

            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
