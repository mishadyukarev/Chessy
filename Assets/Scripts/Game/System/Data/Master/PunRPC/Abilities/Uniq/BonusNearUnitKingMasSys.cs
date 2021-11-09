using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class BonusNearUnitKingMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _cellXyFilt = default;
        private EcsFilter<CellUnitDataC, HpUnitC, StepComponent> _cellUnitFilt = default;
        private EcsFilter<CellUnitDataC, ConditionUnitC, UnitEffectsC, OwnerC> _cellUnitOthFilt = default;
        private EcsFilter<CellUnitDataC, UniqAbilC> _unitUniqFilt = default;

        public void Run()
        {
            var idx_0 = ForBonusNearUnitC.IdxCell;
            ref var unit_0 = ref _cellUnitFilt.Get1(idx_0);
            ref var stepUnit_0 =ref _cellUnitFilt.Get3(idx_0);
            ref var condUnit_0 = ref _cellUnitOthFilt.Get2(idx_0);

            ref var uniq_0 = ref _unitUniqFilt.Get2(idx_0);

            ref var effUnit_0 = ref _cellUnitOthFilt.Get3(idx_0);
            ref var ownUnit_0 = ref _cellUnitOthFilt.Get4(idx_0);

            var sender = InfoC.Sender(MGOTypes.Master);

            if (!uniq_0.HaveCooldown(UniqAbilTypes.BonusNear))
            {
                if (stepUnit_0.HaveMinSteps)
                {
                    var around = CellSpaceSupport.GetXyAround(_cellXyFilt.Get1(idx_0).XyCell);

                    RpcSys.SoundToGeneral(sender, ClipGameTypes.Building);


                    uniq_0.SetCooldown(UniqAbilTypes.BonusNear, 3);

                    stepUnit_0.TakeSteps();
                    if (condUnit_0.HaveCondition) condUnit_0.Def();

                    RpcSys.SoundToGeneral(sender, ClipGameTypes.BonusKing);

                    if (!effUnit_0.Have(UnitStatTypes.Damage)) effUnit_0.Set(UnitStatTypes.Damage);
                    if (!effUnit_0.Have(UnitStatTypes.Steps)) effUnit_0.Set(UnitStatTypes.Steps);

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
                                if (!effUnitC_1.Have(UnitStatTypes.Damage))
                                {
                                    effUnitC_1.Set(UnitStatTypes.Damage);
                                }
                                if (!effUnitC_1.Have(UnitStatTypes.Steps))
                                {
                                    effUnitC_1.Set(UnitStatTypes.Steps);
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

            else RpcSys.SoundToGeneral(sender, ClipGameTypes.Mistake);
        }
    }
}