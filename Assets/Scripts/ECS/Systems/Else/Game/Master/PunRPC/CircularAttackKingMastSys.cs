using Assets.Scripts.Abstractions.Enums;
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
        private EcsFilter<CellUnitDataCom, OwnerOnlineComp> _cellUnitFilter = default;

        private EcsFilter<EndGameDataUIComponent> _endGameDataUIFilter = default;

        public void Run()
        {
            var sender = _infoMastFilter.Get1(0).FromInfo.Sender;
            var idxCurculAttack = _forCircAttackFilter.Get1(0).IdxUnitForCirculAttack;

            ref var starUnitDatCom = ref _cellUnitFilter.Get1(idxCurculAttack);
            ref var starOwnUnitCom = ref _cellUnitFilter.Get2(idxCurculAttack);


            if (starUnitDatCom.HaveMaxAmountSteps)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxCurculAttack)))
                {
                    var idxCurDirect = _xyCellFilter.GetIdxCell(xy1);

                    ref var unitDatComDirect = ref _cellUnitFilter.Get1(idxCurDirect);

                    if (unitDatComDirect.HaveUnit)
                    {
                        unitDatComDirect.TakeAmountHealth(starUnitDatCom.SimplePowerDamage / 3);

                        if (!unitDatComDirect.HaveAmountHealth)
                        {
                            if (unitDatComDirect.Is(UnitTypes.King))
                            {
                                _endGameDataUIFilter.Get1(0).IsEndGame = true;

                                if (_cellUnitFilter.Get2(idxCurDirect).HaveOwner)
                                {
                                    _endGameDataUIFilter.Get1(0).IsOwnerWinner = true;
                                    _endGameDataUIFilter.Get1(0).PlayerWinner = starOwnUnitCom.Owner;
                                }

                                else
                                {
                                    _endGameDataUIFilter.Get1(0).IsOwnerWinner = false;
                                    _endGameDataUIFilter.Get1(0).IsBotWinner = false;
                                }
                            }
                            unitDatComDirect.ResetUnitType();
                        }
                    }
                }

                starUnitDatCom.TakeAmountSteps();

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.AttackMelee);


                if (starUnitDatCom.IsConditionType(CondUnitTypes.Protected) || starUnitDatCom.IsConditionType(CondUnitTypes.Relaxed))
                {
                    starUnitDatCom.ResetConditionType();
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
