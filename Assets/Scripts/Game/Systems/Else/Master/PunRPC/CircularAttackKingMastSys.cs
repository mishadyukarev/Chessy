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
        private EcsFilter<InventorUnitsCom> _invUnitsFilt = default;

        public void Run()
        {
            var sender = _infoMastFilter.Get1(0).FromInfo.Sender;
            var idxCurculAttack = _forCircAttackFilter.Get1(0).IdxUnitForCirculAttack;

            ref var starUnitCom = ref _cellUnitFilter.Get1(idxCurculAttack);
            ref var starOwnUnitCom = ref _cellUnitFilter.Get2(idxCurculAttack);


            if (starUnitCom.HaveMaxAmountSteps)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxCurculAttack)))
                {
                    var idxCurDirect = _xyCellFilter.GetIdxCell(xy1);

                    ref var unitComDirect = ref _cellUnitFilter.Get1(idxCurDirect);
                    ref var ownUnitComDir = ref _cellUnitFilter.Get2(idxCurDirect);

                    if (unitComDirect.HaveUnit)
                    {
                        unitComDirect.TakeAmountHealth(starUnitCom.PowerDamageWithTW / 4);
                        unitComDirect.TakeAmountHealth(2);


                        if (!unitComDirect.HaveAmountHealth)
                        {
                            if (unitComDirect.Is(UnitTypes.King))
                            {
                                _endGameDataUIFilter.Get1(0).PlayerWinner = starOwnUnitCom.PlayerType;
                            }
                            else if (unitComDirect.Is(UnitTypes.Scout))
                            {
                                _invUnitsFilt.Get1(0).AddUnitsInInventor(ownUnitComDir.PlayerType, UnitTypes.Scout, LevelUnitTypes.Wood);
                            }
                            unitComDirect.DefUnitType();
                        }
                    }
                }

                starUnitCom.TakeAmountSteps();

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.AttackMelee);


                if (starUnitCom.Is(CondUnitTypes.Protected) || starUnitCom.Is(CondUnitTypes.Relaxed))
                {
                    starUnitCom.DefCondType();
                }
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
