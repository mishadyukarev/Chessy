using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.Game.Master.PunRPC
{
    internal sealed class CircularAttackKingMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoMasCom> _infoMastFilter = default;
        private EcsFilter<ForCircularAttackMasCom> _forCircAttackFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

        private EcsFilter<EndGameDataUIComponent> _endGameDataUIFilter = default;

        public void Run()
        {
            var sender = _infoMastFilter.Get1(0).FromInfo.Sender;
            var idxCellCurculAttack = _forCircAttackFilter.Get1(0).IdxUnitForCirculAttack;

            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellCurculAttack);
            ref var curOwnerCellUnitCom = ref _cellUnitFilter.Get2(idxCellCurculAttack);


            if (curCellUnitDataCom.HaveMaxAmountSteps)
            {
                foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxCellCurculAttack)))
                {
                    var idxCellCurDirect = _xyCellFilter.GetIndexCell(xy1);

                    ref var cellUnitDataComDirect = ref _cellUnitFilter.Get1(idxCellCurDirect);

                    if (cellUnitDataComDirect.HaveUnit)
                    {
                        cellUnitDataComDirect.TakeAmountHealth(curCellUnitDataCom.SimplePowerDamage / 2);

                        if (!cellUnitDataComDirect.HaveAmountHealth)
                        {
                            //if ()
                            //{
                            //    _endGameDataUIFilter
                            //}

                            if (cellUnitDataComDirect.IsUnitType(UnitTypes.King))
                            {
                                //RpcSys.EndGameToMaster((byte)curOwnerCellUnitCom.ActorNumber);
                            }
                            cellUnitDataComDirect.ResetUnitType();
                        }

                        RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                    }
                }

                curCellUnitDataCom.TakeAmountSteps();

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.AttackMelee);


                if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Protected) || curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Relaxed))
                {
                    curCellUnitDataCom.ResetConditionType();
                }
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
            }
        }
    }
}
