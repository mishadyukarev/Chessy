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
            ref var unitC_0 = ref _cellUnitFilt.Get1(idx_0);
            ref var stepUnitC_0 =ref _cellUnitFilt.Get3(idx_0);

            ref var effUnitC_0 = ref _cellUnitOthFilt.Get3(idx_0);
            ref var ownUnitC_0 = ref _cellUnitOthFilt.Get4(idx_0);

            var sender = InfoC.Sender(MasGenOthTypes.Master);


            if (stepUnitC_0.HaveMaxSteps(effUnitC_0, unitC_0.UnitType))
            {
                var around = CellSpaceSupport.TryGetXyAround(_cellXyFilt.GetXyCell(idx_0));

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                stepUnitC_0.ZeroSteps();
                _cellUnitOthFilt.Get2(idx_0).DefCondition();

                foreach (var xy in around)
                {
                    var idxCell = _cellXyFilt.GetIdxCell(xy);

                    ref var unitC_1 = ref _cellUnitFilt.Get1(idxCell);
                    ref var hpUnitC_1 = ref _cellUnitFilt.Get2(idxCell);
                    ref var stepUnitC_1 = ref _cellUnitFilt.Get3(idxCell);

                    ref var effUnitC_1 = ref _cellUnitOthFilt.Get3(idxCell);
                    ref var curOwnUnitC = ref _cellUnitOthFilt.Get4(idxCell);

                    if (unitC_1.HaveUnit)
                    {
                        if (curOwnUnitC.Is(ownUnitC_0.Owner))
                        {
                            if (!effUnitC_1.Have(StatTypes.Health))
                            {
                                effUnitC_1.Set(StatTypes.Health);
                                hpUnitC_1.TryAddBonusHp(effUnitC_1, unitC_1.UnitType);
                            }
                            if (!effUnitC_1.Have(StatTypes.Damage))
                            {
                                effUnitC_1.Set(StatTypes.Damage);
                            }

                            if (!effUnitC_1.Have(StatTypes.Steps))
                            {
                                effUnitC_1.Set(StatTypes.Steps);
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