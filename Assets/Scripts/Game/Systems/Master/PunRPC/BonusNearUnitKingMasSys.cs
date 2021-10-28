using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class BonusNearUnitKingMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _cellXyFilt = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            var idxFromStart = ForBonusNearUnitC.IdxCell;
            ref var startUnitC = ref _cellUnitFilt.Get1(idxFromStart);
            ref var startOwnUnitC = ref _cellUnitFilt.Get2(idxFromStart);

            var sender = InfoC.Sender(MasGenOthTypes.Master);


            if (startUnitC.HaveMaxAmountSteps)
            {
                var around = CellSpaceSupport.TryGetXyAround(_cellXyFilt.GetXyCell(idxFromStart));

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Building);

                startUnitC.DefAmountSteps();
                startUnitC.DefCondType();

                foreach (var xy in around)
                {
                    var idxCell = _cellXyFilt.GetIdxCell(xy);

                    ref var curUnitC = ref _cellUnitFilt.Get1(idxCell);
                    ref var curOwnUnitC = ref _cellUnitFilt.Get2(idxCell);

                    if (curUnitC.HaveUnit)
                    {
                        if (curOwnUnitC.Is(startOwnUnitC.PlayerType))
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