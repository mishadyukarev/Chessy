using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class CircularAttackKingMastSys : IEcsRunSystem
    {
        private EcsFilter<ForCircularAttackMasCom> _forCircAttackFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var idxCurculAttack = _forCircAttackFilter.Get1(0).IdxUnitForCirculAttack;

            ref var starUnitCom = ref _cellUnitFilter.Get1(idxCurculAttack);
            ref var stepUnitC_0 = ref _cellUnitFilter.Get3(idxCurculAttack);

            ref var condUnitC_0 = ref _cellUnitOthFilt.Get2(idxCurculAttack);
            ref var effUnitC_0 = ref _cellUnitOthFilt.Get4(idxCurculAttack);
            ref var starOwnUnitCom = ref _cellUnitOthFilt.Get5(idxCurculAttack);


            if (stepUnitC_0.HaveMaxSteps(effUnitC_0, starUnitCom.UnitType))
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idxCurculAttack)))
                {
                    var idxCurDirect = _xyCellFilter.GetIdxCell(xy1);

                    ref var dirUnitC = ref _cellUnitFilter.Get1(idxCurDirect);
                    ref var dirHpUnitC = ref _cellUnitFilter.Get2(idxCurDirect);
                    ref var twUnitC_dir = ref _cellUnitOthFilt.Get3(idxCurDirect);
                    ref var effUnitC_1 = ref _cellUnitOthFilt.Get4(idxCurDirect);
                    ref var ownUnitComDir = ref _cellUnitOthFilt.Get5(idxCurDirect);

                    ref var envComDir = ref _cellEnvFilt.Get1(idxCurDirect);
                    ref var buildComDir = ref _cellBuildFilt.Get1(idxCurDirect);

                    if (dirUnitC.HaveUnit)
                    {
                        if (!ownUnitComDir.Is(starOwnUnitCom.PlayerType))
                        {
                            effUnitC_1.DefAllEffects();

                            if (twUnitC_dir.Is(ToolWeaponTypes.Shield))
                            {
                                twUnitC_dir.TakeShieldProtect();
                            }
                            else
                            {
                                if (dirUnitC.IsMelee)
                                {
                                    dirHpUnitC.TakeHp(25);
                                    if (dirHpUnitC.IsHpDeathAfterAttack || !dirHpUnitC.HaveHp)
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
                                else dirUnitC.DefUnitType();
                            }
                        }
                    }
                }

                stepUnitC_0.TakeSteps();

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.AttackMelee);


                if (condUnitC_0.Is(CondUnitTypes.Protected) || condUnitC_0.Is(CondUnitTypes.Relaxed))
                {
                    condUnitC_0.DefCondition();
                }
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
