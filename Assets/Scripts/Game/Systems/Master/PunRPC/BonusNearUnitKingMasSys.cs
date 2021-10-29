using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class BonusNearUnitKingMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _cellXyFilt = default;
        private EcsFilter<CellUnitDataCom, StepComponent, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            var idxFromStart = ForBonusNearUnitC.IdxCell;
            ref var unitC_0 = ref _cellUnitFilt.Get1(idxFromStart);
            ref var stepUnitC_0 =ref _cellUnitFilt.Get2(idxFromStart);
            ref var ownUnitC_0 = ref _cellUnitFilt.Get3(idxFromStart);

            var sender = InfoC.Sender(MasGenOthTypes.Master);


            if (stepUnitC_0.HaveMaxAmountSteps(unitC_0.UnitType))
            {
                var around = CellSpaceSupport.TryGetXyAround(_cellXyFilt.GetXyCell(idxFromStart));

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                stepUnitC_0.DefAmountSteps();
                unitC_0.DefCondType();

                foreach (var xy in around)
                {
                    var idxCell = _cellXyFilt.GetIdxCell(xy);

                    ref var curUnitC = ref _cellUnitFilt.Get1(idxCell);
                    ref var curOwnUnitC = ref _cellUnitFilt.Get3(idxCell);

                    if (curUnitC.HaveUnit)
                    {
                        if (curOwnUnitC.Is(ownUnitC_0.PlayerType))
                        {
                            curUnitC.Set(StatTypes.Health, true);
                            curUnitC.Set(StatTypes.Damage, true);
                            curUnitC.Set(StatTypes.Steps, true);
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