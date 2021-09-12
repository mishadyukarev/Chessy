using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsGiveTWSys : IEcsRunSystem
    {
        private EcsFilter<CellsGiveTWComp> _cellsGiveFilter = default;

        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var cellsGiveTWCom = ref _cellsGiveFilter.Get1(0);

            foreach (var curIdxCell in _cellUnitFilter)
            {
                ref var curUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnerUnitCom = ref _cellUnitFilter.Get2(curIdxCell);


                if (curUnitDataCom.HaveUnit)
                {
                    if (curOwnerUnitCom.HaveOwner)
                    {
                        if (curUnitDataCom.IsUnitType(UnitTypes.Pawn))
                        {
                            //cellsGiveTWCom.Add(ToolWeaponTypes.Axe,)
                        }

                        else if (curUnitDataCom.IsUnitType(UnitTypes.Rook))
                        {

                        }
                    }
                }
            }
        }
    }
}
