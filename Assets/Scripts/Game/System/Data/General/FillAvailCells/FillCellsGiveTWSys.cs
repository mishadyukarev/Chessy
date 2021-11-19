using Leopotam.Ecs;

namespace Game.Game
{
    internal sealed class FillCellsGiveTWSys : IEcsRunSystem
    {
        private EcsFilter<CellsGiveTWComp> _cellsGiveFilter = default;

        private EcsFilter<UnitC, OwnerC> _cellUnitFilter = default;

        public void Run()
        {
            ref var cellsGiveTWCom = ref _cellsGiveFilter.Get1(0);

            foreach (var curIdxCell in _cellUnitFilter)
            {
                ref var curUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnerUnitCom = ref _cellUnitFilter.Get2(curIdxCell);


                if (curUnitDataCom.HaveUnit)
                {
                    if (curUnitDataCom.Is(UnitTypes.Pawn))
                    {
                        //cellsGiveTWCom.Add(ToolWeaponTypes.Axe,)
                    }

                    else if (curUnitDataCom.Is(UnitTypes.Archer))
                    {

                    }
                }
            }
        }
    }
}
