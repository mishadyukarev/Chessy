using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class BonusNearUnitKingMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _cellXyFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent, ConditionUnitC, UnitEffectsC, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            var idxFromStart = ForBonusNearUnitC.IdxCell;
            ref var unitC_0 = ref _cellUnitFilt.Get1(idxFromStart);
            ref var stepUnitC_0 =ref _cellUnitFilt.Get3(idxFromStart);
            ref var ownUnitC_0 = ref _cellUnitFilt.Get6(idxFromStart);

            var sender = InfoC.Sender(MasGenOthTypes.Master);


            if (stepUnitC_0.HaveMaxSteps(unitC_0.UnitType))
            {
                var around = CellSpaceSupport.TryGetXyAround(_cellXyFilt.GetXyCell(idxFromStart));

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                stepUnitC_0.DefSteps();
                _cellUnitFilt.Get4(idxFromStart).DefCondition();

                foreach (var xy in around)
                {
                    var idxCell = _cellXyFilt.GetIdxCell(xy);

                    ref var unitC_1 = ref _cellUnitFilt.Get1(idxCell);
                    ref var hpUnitC_1 = ref _cellUnitFilt.Get2(idxCell);
                    ref var stepUnitC_1 = ref _cellUnitFilt.Get3(idxCell);
                    ref var curOwnUnitC = ref _cellUnitFilt.Get6(idxCell);

                    if (unitC_1.HaveUnit)
                    {
                        if (curOwnUnitC.Is(ownUnitC_0.PlayerType))
                        {
                            if (!_cellUnitFilt.Get5(idxCell).Have(StatTypes.Health))
                            {
                                _cellUnitFilt.Get5(idxCell).Set(StatTypes.Health);
                                if(!hpUnitC_1.HaveCurMaxHpUnit(_cellUnitFilt.Get5(idxCell), unitC_1.UnitType))
                                {
                                    hpUnitC_1.AddBonusHp(unitC_1.UnitType);
                                }
                            }
                            if (!_cellUnitFilt.Get5(idxCell).Have(StatTypes.Damage))
                            {
                                _cellUnitFilt.Get5(idxCell).Set(StatTypes.Damage);
                            }

                            if (!_cellUnitFilt.Get5(idxCell).Have(StatTypes.Steps))
                            {
                                _cellUnitFilt.Get5(idxCell).Set(StatTypes.Steps);

                                if (!stepUnitC_1.HaveMaxSteps(_cellUnitFilt.Get5(idxCell), unitC_1.UnitType))
                                {
                                    stepUnitC_1.AddBonus();
                                }
                                
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