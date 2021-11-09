using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class CircularAttackKingMastSys : IEcsRunSystem
    {
        private EcsFilter<ForCircularAttackMasCom> _forCircAttackFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataC, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataC, ConditionUnitC, ToolWeaponC, UnitEffectsC> _cellUnitOthFilt = default;
        private EcsFilter<CellUnitDataC, UniqAbilC> _unitUniqFilt = default;

        private EcsFilter<CellEnvDataC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataC> _cellBuildFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = _forCircAttackFilter.Get1(0).IdxUnitForCirculAttack;

            ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);

            ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
            ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);

            ref var uniqUnit_0 = ref _unitUniqFilt.Get2(idx_0);

            ref var stepUnit_0 = ref _cellUnitFilter.Get3(idx_0);

            ref var condUnit_0 = ref _cellUnitOthFilt.Get2(idx_0);
            ref var effUnit_0 = ref _cellUnitOthFilt.Get4(idx_0);


            if (!uniqUnit_0.HaveCooldown(UniqAbilTypes.CircularAttack))
            {
                if (stepUnit_0.HaveMinSteps)
                {
                    RpcSys.SoundToGeneral(RpcTarget.All, ClipGameTypes.AttackMelee);

                    uniqUnit_0.SetCooldown(UniqAbilTypes.CircularAttack, 3);

                    foreach (var xy1 in CellSpaceSupport.GetXyAround(_xyCellFilter.Get1(idx_0).XyCell))
                    {
                        var idx_1 = _xyCellFilter.GetIdxCell(xy1);

                        ref var unit_1 = ref _cellUnitFilter.Get1(idx_1);

                        ref var levUnit_1 = ref _cellUnitMainFilt.Get2(idx_1);
                        ref var ownUnit_1 = ref _cellUnitMainFilt.Get3(idx_1);

                        ref var hpUnitC_1 = ref _cellUnitFilter.Get2(idx_1);
                        ref var twUnitC_1 = ref _cellUnitOthFilt.Get3(idx_1);
                        ref var effUnitC_1 = ref _cellUnitOthFilt.Get4(idx_1);

                        ref var envC_1 = ref _cellEnvFilt.Get1(idx_1);
                        ref var buildC_1 = ref _cellBuildFilt.Get1(idx_1);


                        if (unit_1.HaveUnit)
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
                                    if (unit_1.IsMelee)
                                    {
                                        hpUnitC_1.TakeHp(25);
                                        if (hpUnitC_1.IsHpDeathAfterAttack || !hpUnitC_1.HaveHp)
                                        {
                                            if (unit_1.Is(UnitTypes.King))
                                            {
                                                EndGameDataUIC.PlayerWinner = ownUnit_0.Owner;
                                            }
                                            else if (unit_1.Is(UnitTypes.Scout))
                                            {
                                                InvUnitsC.AddUnit(ownUnit_1.Owner, UnitTypes.Scout, LevelUnitTypes.First);
                                            }

                                            WhereUnitsC.Remove(ownUnit_1.Owner, unit_1.Unit, levUnit_1.Level, idx_1);
                                            unit_1.DefUnit();
                                        }
                                    }
                                    else
                                    {
                                        WhereUnitsC.Remove(ownUnit_1.Owner, unit_1.Unit, levUnit_1.Level, idx_1);
                                        unit_1.DefUnit();
                                    }
                                }
                            }
                        }
                    }

                    stepUnit_0.TakeSteps();
                    effUnit_0.DefAllEffects();

                    RpcSys.SoundToGeneral(sender, ClipGameTypes.AttackMelee);


                    if (condUnit_0.HaveCondition) condUnit_0.Def();
                }
                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else RpcSys.SoundToGeneral(sender, ClipGameTypes.Mistake);
        }
    }
}
