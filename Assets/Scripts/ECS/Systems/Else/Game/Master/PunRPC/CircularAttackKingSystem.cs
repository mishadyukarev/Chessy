using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.Game.Master.PunRPC
{
    internal sealed class CircularAttackKingSystem : IEcsRunSystem
    {
        private EcsFilter<InfoMasCom> _infoMastFilter;
        private EcsFilter<ForCircularAttackMasCom> _forCircAttackFilter;
        private EcsFilter<IdxUnitsComponent> _idxUnitsFilter;
        private EcsFilter<IdxUnitsInConditionCom> _idxUnitsInCondFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

        public void Run()
        {
            var sender = _infoMastFilter.Get1(0).FromInfo.Sender;
            var idxCellCurculAttack = _forCircAttackFilter.Get1(0).IdxUnitForCirculAttack;
            ref var idxUnitsCom = ref _idxUnitsFilter.Get1(0);
            ref var idxUnitsInCondCom = ref _idxUnitsInCondFilter.Get1(0);

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
                            if (cellUnitDataComDirect.IsUnitType(UnitTypes.King))
                            {
                                //RPCGameSystem.EndGameToMaster((byte)curOwnerCellUnitCom.ActorNumber);
                            }
                            idxUnitsCom.RemoveAmountUnitsInGame(cellUnitDataComDirect.UnitType, curOwnerCellUnitCom.IsMasterClient, idxCellCurDirect);
                            cellUnitDataComDirect.ResetUnitType();
                        }

                        //RPCGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                    }
                }

                curCellUnitDataCom.TakeAmountSteps();

                //RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.AttackMelee);


                if (curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Protected) || curCellUnitDataCom.IsConditionType(ConditionUnitTypes.Relaxed))
                {

                    idxUnitsInCondCom.ReplaceCondition(curCellUnitDataCom.ConditionType, ConditionUnitTypes.None, UnitTypes.King, sender.IsMasterClient, idxCellCurculAttack);
                    curCellUnitDataCom.ResetConditionType();
                }
            }
            else
            {
                //RPCGameSystem.MistakeStepsUnitToGeneral(sender);
                //RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
            }
        }
    }
}
