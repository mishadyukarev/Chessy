using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Scripts.Game
{
    internal sealed class FireMastSys : IEcsRunSystem
    {
        private EcsFilter<ForFireMasCom> _fireFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        private EcsFilter<CellsArsonArcherComp> _cellsArcherArsonFilt = default;


        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var fromIdx = _fireFilter.Get1(0).FromIdx;
            var toIdx = _fireFilter.Get1(0).ToIdx;

            ref var fromUnitDatCom = ref _cellUnitFilter.Get1(fromIdx);
            ref var fromOnUnitCom = ref _cellUnitFilter.Get2(fromIdx);

            ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdx);
            ref var toFireDatCom = ref _cellFireFilter.Get1(toIdx);
            ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdx);


            if (fromUnitDatCom.IsMelee)
            {
                if (fromUnitDatCom.HaveMinAmountSteps)
                {
                    if (toFireDatCom.HaveFire)
                    {
                        toFireDatCom.HaveFire = default;

                        toUnitDatCom.TakeAmountSteps();
                    }
                    else if (toEnvDatCom.Have(EnvirTypes.AdultForest))
                    {
                        RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                        toFireDatCom.EnabFire();
                        toUnitDatCom.TakeAmountSteps();
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
                if (fromUnitDatCom.HaveMaxAmountSteps)
                {
                    if (_cellsArcherArsonFilt.Get1(0).HaveIdxCell(sender.GetPlayerType(), fromIdx, toIdx))
                    {
                        RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                        fromUnitDatCom.DefAmountSteps();
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
