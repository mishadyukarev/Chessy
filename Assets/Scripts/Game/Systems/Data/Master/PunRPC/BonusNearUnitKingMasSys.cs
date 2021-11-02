using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class BonusNearUnitKingMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _cellXyFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilt = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, UnitEffectsC, OwnerCom> _cellUnitOthFilt = default;

        public void Run()
        {
            var idx_0 = ForBonusNearUnitC.IdxCell;
            ref var unit_0 = ref _cellUnitFilt.Get1(idx_0);
            ref var stepUnitC_0 =ref _cellUnitFilt.Get3(idx_0);

            ref var effUnitC_0 = ref _cellUnitOthFilt.Get3(idx_0);
            ref var ownUnit_0 = ref _cellUnitOthFilt.Get4(idx_0);

            var sender = InfoC.Sender(MGOTypes.Master);


            if (stepUnitC_0.HaveMaxSteps(effUnitC_0, unit_0.Unit, UnitsUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit)))
            {
                var around = CellSpaceSupport.TryGetXyAround(_cellXyFilt.Get1(idx_0).XyCell);

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                stepUnitC_0.ZeroSteps();
                _cellUnitOthFilt.Get2(idx_0).DefCondition();

                foreach (var xy in around)
                {
                    var idxCell = _cellXyFilt.GetIdxCell(xy);

                    ref var unit_1 = ref _cellUnitFilt.Get1(idxCell);
                    ref var hpUnitC_1 = ref _cellUnitFilt.Get2(idxCell);
                    ref var stepUnitC_1 = ref _cellUnitFilt.Get3(idxCell);

                    ref var effUnitC_1 = ref _cellUnitOthFilt.Get3(idxCell);
                    ref var ownUnit_1 = ref _cellUnitOthFilt.Get4(idxCell);

                    if (unit_1.HaveUnit)
                    {
                        if (ownUnit_1.Is(ownUnit_0.Owner))
                        {
                            if (!effUnitC_1.Have(UnitStatTypes.Hp))
                            {
                                effUnitC_1.Set(UnitStatTypes.Hp);
                                hpUnitC_1.TryAddBonusHp(unit_1.Unit, effUnitC_1.Have(UnitStatTypes.Hp), UnitsUpgC.UpgPercent(ownUnit_1.Owner, unit_1.Unit, UnitStatTypes.Hp));
                            }
                            if (!effUnitC_1.Have(UnitStatTypes.Damage))
                            {
                                effUnitC_1.Set(UnitStatTypes.Damage);
                            }

                            if (!effUnitC_1.Have(UnitStatTypes.Steps))
                            {
                                effUnitC_1.Set(UnitStatTypes.Steps);
                                //if (!stepUnitC_1.HaveMaxSteps(effUnitC_1, unitC_1.UnitType))
                                //{
                                //    stepUnitC_1.AddBonus();
                                //}
                            }
                        }
                    }
                }
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}