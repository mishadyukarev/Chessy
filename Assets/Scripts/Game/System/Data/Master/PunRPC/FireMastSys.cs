using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class FireMastSys : IEcsRunSystem
    {
        private EcsFilter<ForFireMasCom> _fireFilter = default;

        private EcsFilter<CellUnitDataC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataC, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;
        private EcsFilter<CellFireDataC> _cellFireFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;

        private EcsFilter<CellsArsonArcherComp> _cellsArcherArsonFilt = default;


        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var fromIdx = _fireFilter.Get1(0).FromIdx;
            var toIdx = _fireFilter.Get1(0).ToIdx;

            ref var unit_from = ref _cellUnitFilter.Get1(fromIdx);
            ref var stepUnitC_from = ref _cellUnitFilter.Get2(fromIdx);
            ref var effUnit_from = ref _cellUnitOthFilt.Get2(fromIdx);
            ref var ownUnit_from = ref _cellUnitOthFilt.Get3(fromIdx);

            ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdx);
            ref var stepUnitC_to = ref _cellUnitFilter.Get2(toIdx);

            ref var toFireDatCom = ref _cellFireFilter.Get1(toIdx);
            ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdx);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (unit_from.IsMelee)
            {
                if (stepUnitC_from.HaveMinSteps)
                {
                    if (toFireDatCom.HaveFire)
                    {
                        toFireDatCom.HaveFire = default;

                        stepUnitC_to.TakeSteps();
                    }
                    else if (toEnvDatCom.Have(EnvTypes.AdultForest))
                    {
                        RpcSys.SoundToGeneral(RpcTarget.All, ClipGameTypes.Fire);

                        toFireDatCom.EnabFire();
                        stepUnitC_to.TakeSteps();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else
            {
                if (stepUnitC_from.HaveMaxSteps(unit_from.Unit, effUnit_from.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_from.Owner, unit_from.Unit)))
                {
                    if (_cellsArcherArsonFilt.Get1(0).HaveIdxCell(whoseMove, fromIdx, toIdx))
                    {
                        RpcSys.SoundToGeneral(RpcTarget.All, ClipGameTypes.Fire);

                        stepUnitC_from.DefSteps();
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
}
