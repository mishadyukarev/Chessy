using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class CircularAttackKingMastSys : IEcsRunSystem
    {
        private EcsFilter<ForCircularAttackMasCom> _forCircAttackFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxCurculAttack = _forCircAttackFilter.Get1(0).IdxUnitForCirculAttack;

            ref var starUnitCom = ref _cellUnitFilter.Get1(idxCurculAttack);
            ref var starOwnUnitCom = ref _cellUnitFilter.Get2(idxCurculAttack);


            if (starUnitCom.HaveMaxAmountSteps)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxCurculAttack)))
                {
                    var idxCurDirect = _xyCellFilter.GetIdxCell(xy1);

                    ref var dirUnitC = ref _cellUnitFilter.Get1(idxCurDirect);
                    ref var ownUnitComDir = ref _cellUnitFilter.Get2(idxCurDirect);
                    ref var envComDir = ref _cellEnvFilt.Get1(idxCurDirect);
                    ref var buildComDir = ref _cellBuildFilt.Get1(idxCurDirect);

                    if (dirUnitC.HaveUnit)
                    {
                        if (dirUnitC.HaveShield)
                        {
                            dirUnitC.TakeShieldProtect();
                        }
                        else
                        {
                            if (dirUnitC.IsMelee) dirUnitC.TakeAmountHealth(100, 0.25f);
                            else dirUnitC.DefUnitType();
                        }
                        

                        if (!dirUnitC.HaveAmountHealth)
                        {
                            if (dirUnitC.Is(UnitTypes.King))
                            {
                                EndGameDataUIC.PlayerWinner = starOwnUnitCom.PlayerType;
                            }
                            else if (dirUnitC.Is(UnitTypes.Scout))
                            {
                                InventorUnitsC.AddUnitsInInventor(ownUnitComDir.PlayerType, UnitTypes.Scout, LevelUnitTypes.Wood);
                            }
                            dirUnitC.DefUnitType();
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
