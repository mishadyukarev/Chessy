﻿using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Scripts.Game
{
    internal sealed class FireMastSys : IEcsRunSystem
    {
        private EcsFilter<ForFireMasCom> _fireFilter = default;

        private EcsFilter<CellUnitDataCom, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        private EcsFilter<CellsArsonArcherComp> _cellsArcherArsonFilt = default;


        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var fromIdx = _fireFilter.Get1(0).FromIdx;
            var toIdx = _fireFilter.Get1(0).ToIdx;

            ref var unitDatC_from = ref _cellUnitFilter.Get1(fromIdx);
            ref var stepUnitC_from = ref _cellUnitFilter.Get2(fromIdx);
            ref var effUnitC_from = ref _cellUnitOthFilt.Get2(fromIdx);
            ref var fromOnUnitCom = ref _cellUnitOthFilt.Get3(fromIdx);

            ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdx);
            ref var stepUnitC_to = ref _cellUnitFilter.Get2(toIdx);

            ref var toFireDatCom = ref _cellFireFilter.Get1(toIdx);
            ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdx);


            if (unitDatC_from.IsMelee)
            {
                if (stepUnitC_from.HaveMinSteps)
                {
                    if (toFireDatCom.HaveFire)
                    {
                        toFireDatCom.HaveFire = default;

                        stepUnitC_to.TakeSteps();
                    }
                    else if (toEnvDatCom.Have(EnvirTypes.AdultForest))
                    {
                        RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

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
                if (stepUnitC_from.HaveMaxSteps(effUnitC_from, unitDatC_from.UnitType))
                {
                    if (_cellsArcherArsonFilt.Get1(0).HaveIdxCell(sender.GetPlayerType(), fromIdx, toIdx))
                    {
                        RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                        stepUnitC_from.ZeroSteps();
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
