using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class FromToNewUnitMS : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyFilt = default;
        private EcsFilter<CellUnitDataC, LevelUnitC, OwnerC> _unitMainFilt = default;

        public void Run()
        {
            ForFromToNewUnitC.Get(out var unit, out var idx_from, out var idx_to);
            var whoseMove = WhoseMoveC.WhoseMove;

            ref var unit_from = ref _unitMainFilt.Get1(idx_from);
            ref var levUnit_from = ref _unitMainFilt.Get2(idx_from);
            ref var ownUnit_from = ref _unitMainFilt.Get3(idx_from);

            ref var unit_to = ref _unitMainFilt.Get1(idx_to);
            ref var levUnit_to = ref _unitMainFilt.Get2(idx_to);
            ref var ownUnit_to = ref _unitMainFilt.Get3(idx_to);


            if (unit_from.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
            {
                if (unit_to.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                {
                    if (ownUnit_from.Is(whoseMove) && ownUnit_to.Is(whoseMove))
                    {
                        var xy_from = _xyFilt.Get1(idx_from).XyCell;

                        var list_around = CellSpaceSupport.GetXyAround(xy_from);

                        foreach (var xy_1 in list_around)
                        {
                            var idx_1 = _xyFilt.GetIdxCell(xy_1);

                            if (idx_1 == idx_to)
                            {
                                WhereUnitsC.Remove(ownUnit_from.Owner, unit_from.Unit, levUnit_from.Level, idx_from);
                                unit_from.DefUnit();

                                WhereUnitsC.Remove(ownUnit_to.Owner, unit_to.Unit, levUnit_to.Level, idx_to);
                                unit_to.DefUnit();
                                

                                unit_to.SetUnit(unit);
                                WhereUnitsC.Add(ownUnit_to.Owner, unit_to.Unit, levUnit_to.Level, idx_to);

                                InvUnitsC.TakeUnit(ownUnit_to.Owner, unit_to.Unit, levUnit_to.Level, idx_to);

                                return;
                            }
                        }
                    }
                }
            }

        }
    }
}