using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class CircularAttackKingMastSys : IEcsRunSystem
    {
        private EcsFilter<ForCircularAttackMasCom> _forCircAttackFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC> _cellUnitOthFilt = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataC> _cellBuildFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idxCurculAttack = _forCircAttackFilter.Get1(0).IdxUnitForCirculAttack;

            ref var unit_0 = ref _cellUnitFilter.Get1(idxCurculAttack);

            ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idxCurculAttack);
            ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idxCurculAttack);

            ref var stepUnitC_0 = ref _cellUnitFilter.Get3(idxCurculAttack);

            ref var condUnit_0 = ref _cellUnitOthFilt.Get2(idxCurculAttack);
            ref var effUnit_0 = ref _cellUnitOthFilt.Get4(idxCurculAttack);


            if (stepUnitC_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit)))
            {
                RpcSys.SoundToGeneral(RpcTarget.All, ClipGameTypes.AttackMelee);

                foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.Get1(idxCurculAttack).XyCell))
                {
                    var idx_1 = _xyCellFilter.GetIdxCell(xy1);

                    ref var unitC_1 = ref _cellUnitFilter.Get1(idx_1);

                    ref var levUnit_1 = ref _cellUnitMainFilt.Get2(idx_1);
                    ref var ownUnit_1 = ref _cellUnitMainFilt.Get3(idx_1);

                    ref var hpUnitC_1 = ref _cellUnitFilter.Get2(idx_1);
                    ref var twUnitC_1 = ref _cellUnitOthFilt.Get3(idx_1);
                    ref var effUnitC_1 = ref _cellUnitOthFilt.Get4(idx_1);

                    ref var envC_1 = ref _cellEnvFilt.Get1(idx_1);
                    ref var buildC_1 = ref _cellBuildFilt.Get1(idx_1);

                    if (unitC_1.HaveUnit)
                    {
                        if (!ownUnit_1.Is(ownUnit_0.Owner))
                        {
                            effUnitC_1.DefAllEffects();

                            if (twUnitC_1.Is(ToolWeaponTypes.Shield))
                            {
                                twUnitC_1.TakeShieldProtect();
                            }
                            else
                            {
                                if (unitC_1.IsMelee)
                                {
                                    hpUnitC_1.TakeHp(25);
                                    if (hpUnitC_1.IsHpDeathAfterAttack || !hpUnitC_1.HaveHp)
                                    {
                                        if (unitC_1.Is(UnitTypes.King))
                                        {
                                            EndGameDataUIC.PlayerWinner = ownUnit_0.Owner;
                                        }
                                        else if (unitC_1.Is(UnitTypes.Scout))
                                        {
                                            InvUnitsC.AddUnit(ownUnit_1.Owner, UnitTypes.Scout, LevelUnitTypes.Wood);
                                        }

                                        WhereUnitsC.Remove(ownUnit_1.Owner, unitC_1.Unit, levUnit_1.Level, idx_1);
                                        unitC_1.NoneUnit();
                                    }
                                }
                                else
                                {
                                    WhereUnitsC.Remove(ownUnit_1.Owner, unitC_1.Unit, levUnit_1.Level, idx_1);
                                    unitC_1.NoneUnit();
                                }
                            }
                        }
                    }
                }

                stepUnitC_0.DefSteps();

                RpcSys.SoundToGeneral(sender, ClipGameTypes.AttackMelee);


                if (condUnit_0.HaveCondition) condUnit_0.Def();
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
