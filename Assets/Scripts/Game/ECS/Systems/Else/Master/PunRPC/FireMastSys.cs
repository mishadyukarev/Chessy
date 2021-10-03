using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Scripts.Game
{
    internal sealed class FireMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<ForFireMasCom> _fireFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

        private EcsFilter<CellsArsonArcherComp> _cellsArcherArsonFilt = default;


        public void Run()
        {
            var sender = _infoFilter.Get1(0).FromInfo.Sender;
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
                    else if (toEnvDatCom.HaveEnvir(EnvirTypes.AdultForest))
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
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
            }

            else
            {
                if (fromUnitDatCom.HaveMaxAmountSteps)
                {
                    if (_cellsArcherArsonFilt.Get1(0).HaveIdxCell(sender.GetPlayerType(), fromIdx, toIdx))
                    {
                        RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                        fromUnitDatCom.ResetAmountSteps();
                        toFireDatCom.HaveFire = true;
                    }

                    //foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(fromIdx)))
                    //{
                    //    var curIdx = _xyCellFilter.GetIndexCell(xy1);

                    //    ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(curIdx);

                    //    if (curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                    //    {
                    //        if (curIdx == toIdx)
                    //        {
                    //            fromCellUnitDataCom.ResetAmountSteps();
                    //            toCellFireDataCom.HaveFire = true;
                    //        }
                    //    }
                    //}
                }

                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
            }
        }
    }
}
