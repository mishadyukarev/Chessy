using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class CircularAttackKingMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoMastFilter = default;
        private EcsFilter<ForCircularAttackMasCom> _forCircAttackFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

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
                        unitDatComDirect.TakeAmountHealth(starUnitDatCom.SimplePowerDamage / 4);
                        unitDatComDirect.TakeAmountHealth(2);


                        if (!unitDatComDirect.HaveAmountHealth)
                        {
                            if (unitDatComDirect.Is(UnitTypes.King))
                            {
                                _endGameDataUIFilter.Get1(0).PlayerWinner = starOwnUnitCom.PlayerType;
                            }
                            unitDatComDirect.DefUnitType();
                        }
                    }
                }

                starUnitDatCom.TakeAmountSteps();

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.AttackMelee);


                if (starUnitDatCom.Is(CondUnitTypes.Protected) || starUnitDatCom.Is(CondUnitTypes.Relaxed))
                {
                    starUnitDatCom.ResetCondType();
                }
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
