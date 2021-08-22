using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class FireMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForFireMasCom> _fireFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

        private EcsFilter<AvailCellsForArcherArsonComp> _availCellsForArcherArsonFilter = default;


        public void Run()
        {
            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var fromIdx = _fireFilter.Get1(0).FromIdx;
            var toIdx = _fireFilter.Get1(0).ToIdx;

            ref var fromCellUnitDataCom = ref _cellUnitFilter.Get1(fromIdx);
            ref var fromOwnerCellUnitCom = ref _cellUnitFilter.Get2(fromIdx);

            ref var toCellUnitDataCom = ref _cellUnitFilter.Get1(toIdx);
            ref var toCellFireDataCom = ref _cellFireFilter.Get1(toIdx);
            ref var toCellEnvDataCom = ref _cellEnvFilter.Get1(toIdx);


            if (fromCellUnitDataCom.IsMelee)
            {
                if (fromCellUnitDataCom.HaveMinAmountSteps)
                {
                    if (toCellFireDataCom.HaveFire)
                    {
                        toCellFireDataCom.HaveFire = default;

                        toCellUnitDataCom.TakeAmountSteps();
                    }
                    else if (toCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        if (fromOwnerCellUnitCom.HaveOwner)
                        {
                            RpcGeneralSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                            toCellFireDataCom.HaveFire = true;
                            toCellUnitDataCom.TakeAmountSteps();
                        }

                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                else
                {
                    RpcGeneralSystem.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    RpcGeneralSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
            }

            else
            {
                if (fromCellUnitDataCom.HaveMaxAmountSteps)
                {
                    if(_availCellsForArcherArsonFilter.Get1(0).HaveIdxCell(sender.IsMasterClient, toIdx))
                    {
                        fromCellUnitDataCom.ResetAmountSteps();
                        toCellFireDataCom.HaveFire = true;
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
                    RpcGeneralSystem.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    RpcGeneralSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                }
            }
        }
    }
}
