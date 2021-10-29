using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class CircularAttackKingMastSys : IEcsRunSystem
    {
        private EcsFilter<ForCircularAttackMasCom> _forCircAttackFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, HpComponent, StepComponent, ToolWeaponC, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxCurculAttack = _forCircAttackFilter.Get1(0).IdxUnitForCirculAttack;

            ref var starUnitCom = ref _cellUnitFilter.Get1(idxCurculAttack);
            ref var stepUnitC_0 = ref _cellUnitFilter.Get3(idxCurculAttack);
            ref var starOwnUnitCom = ref _cellUnitFilter.Get5(idxCurculAttack);


            if (stepUnitC_0.HaveMaxAmountSteps(starUnitCom.UnitType))
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxCurculAttack)))
                {
                    var idxCurDirect = _xyCellFilter.GetIdxCell(xy1);

                    ref var dirUnitC = ref _cellUnitFilter.Get1(idxCurDirect);
                    ref var dirHpUnitC = ref _cellUnitFilter.Get2(idxCurDirect);
                    ref var twUnitC_dir = ref _cellUnitFilter.Get4(idxCurDirect);
                    ref var ownUnitComDir = ref _cellUnitFilter.Get5(idxCurDirect);

                    ref var envComDir = ref _cellEnvFilt.Get1(idxCurDirect);
                    ref var buildComDir = ref _cellBuildFilt.Get1(idxCurDirect);

                    if (dirUnitC.HaveUnit)
                    {
                        if (twUnitC_dir.HaveShield)
                        {
                            twUnitC_dir.TakeShieldProtect();
                        }
                        else
                        {
                            if (dirUnitC.IsMelee) dirHpUnitC.TakeAmountHealth(100, 0.25f);
                            else dirUnitC.DefUnitType();
                        }
                        

                        if (!dirHpUnitC.HaveAmountHealth)
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

                stepUnitC_0.TakeAmountSteps();

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
